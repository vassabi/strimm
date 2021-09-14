using log4net;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL.Processor
{
    public class VideoProcessor : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoProcessor));

        private FFMpegConverter converter;

        public VideoProcessor()
        {
            converter = new FFMpegConverter();
            converter.LogReceived += converter_LogReceived;
        }

        void converter_LogReceived(object sender, FFMpegLogEventArgs e)
        {
            Logger.DebugFormat("Received converter log even: {0}", e.ToString());
        }

        public FFMpegConverter Converter
        {
            get
            {
                return converter;
            }
        }

        /// <summary>
        /// This method will determine duration of a video file specified by
        /// either a local path or an external URL
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        public string GetVideoDuration(string inputFile)
        {
            Logger.Info(String.Format("Retrieving video duration for video file - {0}", inputFile));

            string duration = String.Empty;

            try
            {
                TimeSpan totalDuration = TimeSpan.Zero;

                converter.ConvertProgress += (o, args) =>
                {
                    totalDuration = args.TotalDuration;
                };

                converter.LogReceived += (o, args) =>
                {
                    Logger.Info(String.Format("Ncero.VideoConverter log statement received: {0}", args.Data));
                };

                converter.ConvertMedia(inputFile, null, "-", "mpeg", new ConvertSettings()
                {

                    VideoFrameRate = 1,
                    VideoCodec = "libx264"
                });

                duration = totalDuration.ToString();

                Logger.Debug(String.Format("Video duration for '{0}' is {1}", inputFile, duration));
            }
            catch (Exception ex)
            {
                Logger.Error("Error occurred while getting video duration", ex);
            }

            return duration;
        }

        /// <summary>
        /// This method will get video thumbnail from a local file or a remote resource
        /// identified by the URI, will upload it to S3 and will return it final URL
        /// </summary>
        /// <param name="inputFile">Local or remove video file path or URI respectively</param>
        /// <param name="timeFrameInSec">Time offset where to take video thumbnail</param>
        /// <returns>Memory stream that contains video thumbnail</returns>
        public Stream GetVideoTumbnail(string inputFile, float timeFrameInSec)
        {
            Logger.Info(String.Format("Retrieving thumbnail for video '{0}' at {1} sec", inputFile, timeFrameInSec));

            string outputFile = String.Empty;

            Stream oStream = new MemoryStream();

            try
            {
                converter.GetVideoThumbnail(inputFile, oStream, timeFrameInSec);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occurred while extracting a video thumbnail for video '{0}' at time frame of {1} sec", inputFile, timeFrameInSec), ex);
            }

            return oStream;
        }

        /// <summary>
        /// This method will take a remote video resource or local file path, generate a video priview clip, uploads it to
        /// the S3 bucket and return the URL to uploaded clip.
        /// </summary>
        /// <param name="inputFileUrl">Input video URL or local file path</param>
        /// <param name="timeFrameInSec">Duration of the preview clip to create</param>
        /// <returns>Memory stream that contains preview clip content</returns>
        public Stream GetVideoPreviewClip(string inputFile, float timeFrameInSec)
        {
            string outputFile = String.Empty;

            Stream oStream = new MemoryStream();

            try
            {
                converter.ConvertMedia(inputFile, null, oStream, "mp4", new ConvertSettings()
                {
                    VideoCodec = "libx264",
                    MaxDuration = timeFrameInSec
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while retrieving preview clip: " + ex.StackTrace);
            }

            return oStream;
        }

        /// <summary>
        /// This method will return file name without the extension regardless if this is
        /// a local file or a remove file represented by URI
        /// </summary>
        /// <param name="inputFile">Input file path or URI</param>
        /// <returns>File name</returns>
        public string GetFilenameWithoutPath(string inputFile)
        {
            Logger.Info(String.Format("Retrieving file name without extension for: {0}", inputFile));

            if (String.IsNullOrEmpty(inputFile))
            {
                return null;
            }

            string filename = String.Empty;

            try
            {
                var iFile = new FileInfo(inputFile);
                filename = iFile.Name.TrimEnd(iFile.Extension.ToCharArray());
            }
            catch (Exception ex)
            {
                Logger.Debug(String.Format("Specified file path does not represent a local file path: {0}", inputFile));

                filename = GetFilenameFromUrl(inputFile);
            }

            return filename;
        }

        private string GetFilenameFromUrl(string inputFile)
        {
            string filename;

            try
            {
                var url = new Uri(inputFile);
                var name = url.Segments[url.Segments.Count() - 1];

                filename = name.Substring(0, name.IndexOf("."));
            }
            catch (Exception uex)
            {
                Logger.Error(String.Format("Specified path {0} is not a local path or a valid URI", inputFile), uex);
                filename = String.Empty;
            }

            return filename;
        }

        public void Dispose()
        {
            if (converter != null)
            {
                try
                {
                    converter.ConvertProgress -= null;
                    converter.LogReceived -= converter_LogReceived;
                    converter.Stop();
                    converter = null;
                }
                catch (Exception ex)
                {
                    Logger.Error("Error occured while disposing of the converter", ex);
                }
            }
        }
    }
}
