using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class CryptoUtils
    {
        private static char[] charSet                   = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private static Random rGen                      = new Random(); //Must share, because the clock seed only has Ticks (~10ms) resolution, yet lock has only 20-50ns delay.
        private static int byteSize                     = 256; //Labelling convenience
        private static int biasZone                     = byteSize - (byteSize % charSet.Length);
        private static bool SlightlyMoreSecurityNeeded  = true; //Configuration - needs to be true, if more security is desired and if charSet.Length is not divisible by 2^X.


        public static string GetPasswordWithSalt(string email, string password)
        {
            return String.Format("{0}-{1}", password, email);
        }
       

        public static RijndaelManaged BuildRigndaelCommon(out byte[] rgbIV, out byte[] key)
        {
            rgbIV = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x5, 0x6, 0x7, 0x8, 0xA, 0xB, 0xC, 0xD, 0xF, 0x10, 0x11, 0x12 };
            key = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x5, 0x6, 0x7, 0x8, 0xA, 0xB, 0xC, 0xD, 0xF, 0x10, 0x11, 0x12 };

            //Specify the algorithms key & IV
            RijndaelManaged rijndael = new RijndaelManaged { BlockSize = 128, IV = rgbIV, KeySize = 128, Key = key, Padding = PaddingMode.PKCS7 };
            
            return rijndael;
        }

        public static string HashPassword(string passwordWithSalt)
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(passwordWithSalt);
            string plaintextstring="";
            foreach(var str in plaintextBytes)
            {
                plaintextstring += str + ",";
            }
           
            byte[] result;

            using (SHA256 shaM = new SHA256Managed())
            {
                result = shaM.ComputeHash(plaintextBytes);
            }
            string resultstring = "";
            foreach (var str in result)
            {
                resultstring += str + ",";
            }
            return Convert.ToBase64String(result);
        }

        public static string ConvertToBase64(string password)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(passwordBytes);
        }

        public static string Encrypt(string clearText)
        {
            byte[] rgbIV;
            byte[] key;

            RijndaelManaged rijndael = CryptoUtils.BuildRigndaelCommon(out rgbIV, out key);

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] cipherTextBytes = null;

            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                //Call SymmetricAlgorithm.CreateEncryptor to create the Encryptor obj
                var transform = rijndael.CreateEncryptor();

                //Chaining mode
                symmetricKey.Mode = CipherMode.CFB;
                //create encryptor from the key and the IV value
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(key, rgbIV);

                //define memory stream to hold encrypted data
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    //encrypt contents of cryptostream
                    cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                    cs.Flush();
                    cs.FlushFinalBlock();

                    //convert encrypted data from a memory stream into a byte array
                    ms.Position = 0;
                    cipherTextBytes = ms.ToArray();

                    ms.Close();
                    cs.Close();
                }
            }

            return BitConverter.ToString(cipherTextBytes).Replace("-", "");
        }

        public static string Decrypt(string encryptedData)
        {
            byte[] disguishedtextBytes = FromHexString(encryptedData);

            byte[] rgbIV;
            byte[] key;

            CryptoUtils.BuildRigndaelCommon(out rgbIV, out key);

            string visiabletext = "";
            
            //create uninitialized Rijndael encryption obj
            using (var symmetricKey = new RijndaelManaged())
            {
                //Call SymmetricAlgorithm.CreateEncryptor to create the Encryptor obj
                symmetricKey.Mode = CipherMode.CFB;
                symmetricKey.BlockSize = 128;

                //create encryptor from the key and the IV value

                // ICryptoTransform encryptor = symmetricKey.CreateEncryptor(key, rgbIV);
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(key, rgbIV);

                //define memory stream to hold encrypted data
                using (MemoryStream ms = new MemoryStream(disguishedtextBytes))
                {
                    //define cryptographic stream - contains the transformation to be used and the mode
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] decryptedData = new byte[disguishedtextBytes.Length];
                        int stringSize = cs.Read(decryptedData, 0, disguishedtextBytes.Length);
                        cs.Close();

                        //Trim the excess empty elements from the array and convert back to a string
                        byte[] trimmedData = new byte[stringSize];
                        Array.Copy(decryptedData, trimmedData, stringSize);
                        visiabletext = Encoding.UTF8.GetString(trimmedData);
                    }
                }
            }

            return visiabletext;
        }

      

        public static byte[] FromHexString(string hexString)
        {
            if (hexString == null)
            {
                return new byte[0];
            }

            var numberChars = hexString.Length;
            var bytes = new byte[numberChars / 2];

            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GenerateRandomString(int Length) //Configurable output string length
        {
            byte[] rBytes = new byte[Length]; //Do as much before and after lock as possible
            char[] rName = new char[Length];

            lock (rGen) //~20-50ns
            {
                rGen.NextBytes(rBytes);

                for (int i = 0; i < Length; i++)
                {
                    while (SlightlyMoreSecurityNeeded && rBytes[i] >= biasZone) //Secure against 1/5 increased bias of index[0-7] values against others. Note: Must exclude where it == biasZone (that is >=), otherwise there's still a bias on index 0.
                    {
                        rGen.NextBytes(rBytes);
                    }

                    rName[i] = charSet[rBytes[i] % charSet.Length];
                }
            }

            return new string(rName);
        }

        public static string generateToken(int authType, string emailid)
        {
            string token = string.Empty;
            switch (authType)
            {
                case 1:
                    token = "custom:" + emailid + ":" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH-mm-ss");
                    break;
                case 2:
                    token = "facebook:" + emailid + ":" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH-mm-ss");
                    break;
                case 3:
                    token = "google:" + emailid + ":" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH-mm-ss");
                    break;
                default:
                    token = "";
                    break;
            }
            return token;
        }

    }
}
