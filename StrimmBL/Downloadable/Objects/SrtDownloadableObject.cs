using System.Text;

namespace StrimmBL.Downloadable
{
    public class SrtDownloadableObject : BaseDownloadableObject
    {
        public SrtDownloadableObject()
        {
            ContentType = "text/plain";
            FileFormat = "srt";
            FileExtension = "srt";
        }

        public override byte[] GenerateFile(string channelName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1");
            sb.AppendLine("00:00:01,000 --> 00:00:06,000");
            sb.AppendLine("- Welcome!");

            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}
