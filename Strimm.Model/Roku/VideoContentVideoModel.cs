namespace Strimm.Model.Roku
{
    public class VideoContentVideoModel
    {
        /// <summary>
        /// The URL of the video itself. The URL must use the secure protocol prefix "https://".
        /// All videos must play across multiple devices.
        /// The video should be served from a CDN(Content Distribution Network).
        /// Supported formats are described in Audio and Video Support.
        /// </summary>
        public string url { get; set; }     //	https://bitdash-a.akamaihd.net/content/MI201109210084_1/m3u8s/f08e80da-bf1d-4e3d-8899-f0f6155f6efa.m3u8",

        /// <summary>
        /// Must be one of the following display types:
        ///     SD: Anything under 720p
        ///     HD: 720p
        ///     FHD: 1080p
        ///     UHD: 4K
        /// </summary>
        public string quality { get; set; }

        /// <summary>
        /// Must be one of the following:
        ///     HLS
        ///     SMOOTH
        ///     DASH
        ///     MP4
        ///     MOV
        ///     M4V
        ///     
        /// Provided videos must be unencrypted because there is no encryption support:
        /// Audio should be as follows:
        ///     Minimum: first track of Stereo
        ///     Preferred: second track of Dolby(optional)
        /// </summary>
        public string videoType { get; set; }
    }
}
