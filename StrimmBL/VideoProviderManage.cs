using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public static class VideoProviderManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoProviderManage));

        public static List<VideoProvider> GetAllVideoProviders()
        {
            Logger.Info("Retrieving all video providers");

            List<VideoProvider> providers = null;

            using (var videoProviderRepository = new VideoProviderRepository())
            {
                providers = videoProviderRepository.GetAllVideoProviders();
            }

            return providers;
        }

        public static List<VideoProvider> GetActiveVideoProviders()
        {
            Logger.Info("Retrieving active video providers");

            List<VideoProvider> providers = null;

            using (var videoProviderRepository = new VideoProviderRepository())
            {
                providers = videoProviderRepository.GetActiveVideoProviders();
            }

            return providers;
        }
    }
}
