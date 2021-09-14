using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class MiscUtils
    {
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

        public static string EncodeStringToBase64(string value)
        {
            //try
            //{
            //    byte[] encData = new byte[value.Length];
            //    encData = System.Text.Encoding.UTF8.GetBytes(value);
            //    string encodedData = Convert.ToBase64String(encData);
            //    return encodedData;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error in base64Encode" + ex.Message);
            //}
            byte[] rgbIV;
            byte[] key;

            RijndaelManaged rijndael = CryptoUtils.BuildRigndaelCommon(out rgbIV, out key);

            //convert plaintext into a byte array
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(value);

            byte[] cipherTextBytes = null;

            //create uninitialized Rijndael encryption obj
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

            //store result as a hex value
            return BitConverter.ToString(cipherTextBytes).Replace("-", "");
        }
        
        public static string DecodeFrom64(string encodedData)
        {
            byte[] disguishedtextBytes = FromHexString(encodedData);

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

        public static string ToFixed(this double number, uint decimals)
        {
            return number.ToString("N" + decimals);
        }

        public static List<string> GetPrepostionFreeText (List<string> keywords)
        {
           //var myRegex = new Regex("at|as|if|of|the|to|a|an|it|is|by|its|and",
           //     RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var myRegex = new Regex("and,|as,|if,|of,|the,|a,|an,|it,|by,|its,|or,|is,|on,|at,|to,|in,|off,|let,|nor,");
           List<string> propostionFreeText = new List<string>();
            foreach(var s in keywords)
            {
                var str = myRegex.Replace(s, String.Empty);
                if(!String.IsNullOrEmpty(str)&&str.Length>2)
                {
                    propostionFreeText.Add(str);
                }
            }
           
           return propostionFreeText;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", " ", RegexOptions.Compiled);
        }
    }
}
