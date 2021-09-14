namespace Strimm.Model.Roku
{
    public class VideoContentCaptionModel
    {
        /// <summary>
        /// The URL of the video caption file. The URL must use the secure protocol prefix "https://".
        /// Supported formats are described in Closed Caption Support.
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// A language code for the subtitle (e.g., “en”, “es-mx”, “fr”, etc). ISO 639-2 or alpha-3 language code string.
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// The type of caption. Default is SUBTITLE. Must be one of the following:
        ///     CLOSED_CAPTION | SUBTITLE
        /// </summary>
        public string captionType { get; set; }
    }
}
