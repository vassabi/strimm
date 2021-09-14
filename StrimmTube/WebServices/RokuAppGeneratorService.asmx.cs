using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using Strimm.Model;
using StrimmBL;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for RokuAppGeneratorService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RokuAppGeneratorService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GenerateApp(string AppName, string AdLink, string AppAbout, string PrivacyPolicyLink, string HdImageData, string SdImageData, string Username, string UserId)
        {
            string startPath = Server.MapPath("~/RokuAppTemplate");
            string tempPath = Server.MapPath("~/RokuGeneratedApps/Temp/"+ AppName);
            string zipPath = Server.MapPath("~/RokuGeneratedApps/" + AppName + ".zip");
            DirectoryCopy(startPath, tempPath, true);
            AppendData(tempPath, AppName, AdLink, AppAbout, PrivacyPolicyLink, HdImageData, SdImageData, Username, UserId);
            if(File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            var z = new ICSharpCode.SharpZipLib.Zip.FastZip();
            z.CreateEmptyDirectories = true;
            z.CreateZip(zipPath, tempPath, true, "");
            //ZipFile.CreateFromDirectory(tempPath, zipPath, CompressionLevel.NoCompression, false);
            Directory.Delete(tempPath, true);

            return AppName + ".zip";
        }

        [WebMethod]
        public string GetChannelTubeRokuSettings(string channelId)
        {
            var settings = ChannelManage.GetChannelRokuSettings(int.Parse(channelId));
            return JsonConvert.SerializeObject(settings);
        }

        [WebMethod]
        public string GetUserRokuApp(string userId)
        {
            var app = RokuAppManager.GetUserApp(int.Parse(userId));
            return JsonConvert.SerializeObject(app);
        }

        [WebMethod]
        public void UpsertChannelTubeRokuSettings(string channelId, string addedToRoku)
        {
            var settings = new ChannelTubeRokuSettings();
            settings.AddedToRoku = addedToRoku.ToLower() == "true";
            settings.ChannelTubeId = int.Parse(channelId);
            settings.LastUpdateDate = DateTime.Now;
            ChannelManage.UpsertChannelRokuSettings(settings);
        }

        private void AppendData(string path, string AppName, string AdLink, string AppAbout, string PrivacyPolicyLink, string HdImageData, string SdImageData, string Username, string UserId)
        {
            HdImageData = HdImageData.Replace("data:image/png;base64,", string.Empty);
            SdImageData = SdImageData.Replace("data:image/png;base64,", string.Empty);

            byte[] hdImage = Convert.FromBase64String(HdImageData);
            byte[] sdImage = Convert.FromBase64String(SdImageData);
            File.WriteAllBytes(path + "/images/mm_icon_focus_hd.png", hdImage);
            File.WriteAllBytes(path + "/images/mm_icon_focus_sd.png", sdImage);

            string apikey = UpsertAppData(AppName, AdLink, AppAbout, PrivacyPolicyLink, hdImage, sdImage, Username, UserId);
            //Get config.json file data and replace variables
            var config = File.ReadAllText(path + "/data/config.json");
            config = config.Replace("<api_key>", apikey);
            File.WriteAllText(path + "/data/config.json", config);
            //Get manifest file data and replace variables
            string build_version = DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day;
            var manifest = File.ReadAllText(path + "/manifest");
            manifest = manifest.Replace("<title>", AppName);
            manifest = manifest.Replace("<build_version>", build_version);
            File.WriteAllText(path + "/manifest", manifest);
        }

        private string UpsertAppData(string AppName, string AdLink, string AppAbout, string PrivacyPolicyLink, byte[] HdImageData, byte[] SdImageData, string Username, string UserId)
        {
            var rokuApp = new RokuApp();
            rokuApp.AppName = AppName;
            rokuApp.AdLink = AdLink;
            rokuApp.About = AppAbout;
            rokuApp.PrivacyPolicyLink = PrivacyPolicyLink;
            rokuApp.ImageHD = HdImageData;
            rokuApp.ImageSD = SdImageData;
            rokuApp.UserID = int.Parse(UserId);
            return RokuAppManager.UpsertUserApp(rokuApp).ToString();
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
