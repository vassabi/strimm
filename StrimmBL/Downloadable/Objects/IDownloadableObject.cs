namespace StrimmBL.Downloadable
{
    public interface IDownloadableObject
    {
        string ContentType { get; }
        string FileFormat { get; }


        string GenerateLink(string channelName);
        byte[] GenerateFile(string channelName);
    }
}
