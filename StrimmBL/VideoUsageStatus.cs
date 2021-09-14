using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public class VideoUsageStatus
    {
        // View: https://developers.google.com/youtube/v3/docs/videos#status.uploadStatus
        //
        private static readonly IList<string> videoRejectionStatuses = new List<string>() { "failed", "deleted", "rejected" };

        public VideoStateByProvider State { get; set; }

        public string UsageFailureReason { get; set; }

        public VideoUsageStatus(Video video)
        {
            if (video != null)
            {
                State = VideoStateByProvider.VIEWABLE;
                UsageFailureReason = String.Empty;

                if (video.Status != null)
                {
                    if (videoRejectionStatuses.Contains(video.Status.UploadStatus))
                    {
                        switch (video.Status.UploadStatus)
                        {
                            case "deleted":
                                State = VideoStateByProvider.DELETED;
                                break;
                            case "failed":
                                State = VideoStateByProvider.FAILED;
                                break;
                            case "rejected":
                                State = VideoStateByProvider.REJECTED;
                                break;
                            default:
                                break;
                        }
                        UsageFailureReason = video.Status.RejectionReason;
                    }
                    else if (video.Status.PrivacyStatus == "private")
                    {
                        State = VideoStateByProvider.PRIVATE;
                        UsageFailureReason = "video is private";
                    }
                    else if (video.Status.Embeddable != null && !video.Status.Embeddable.Value)
                    {
                        State = VideoStateByProvider.RESTRICTED;
                        UsageFailureReason = "video is not embeddable on other sites";
                    }
                }

                if (State == VideoStateByProvider.VIEWABLE && video.ContentDetails != null)
                {
                    if (video.ContentDetails.ContentRating != null && !String.IsNullOrEmpty(video.ContentDetails.ContentRating.YtRating))
                    {
                        State = VideoStateByProvider.RESTRICTED;
                        UsageFailureReason = video.ContentDetails.ContentRating.YtRating;
                    }
                    else if (video.ContentDetails.CountryRestriction != null && video.ContentDetails.CountryRestriction.Allowed != null && !video.ContentDetails.CountryRestriction.Allowed.Value)
                    {
                        State = VideoStateByProvider.RESTRICTED;
                        UsageFailureReason = "video country restriction is set";
                    }
                    else if (video.ContentDetails.RegionRestriction != null && (video.ContentDetails.RegionRestriction.Allowed != null ||
                        video.ContentDetails.RegionRestriction.Blocked != null))
                    {
                        State = VideoStateByProvider.RESTRICTED;
                        UsageFailureReason = "video region restriction is set";
                    }
                }
            }
        }

        public VideoUsageStatus(LiveBroadcast video)
        {
            if (video != null)
            {
                State = VideoStateByProvider.VIEWABLE;
                UsageFailureReason = String.Empty;

                if (video.Status != null)
                {
                    if (videoRejectionStatuses.Contains(video.Status.PrivacyStatus))
                    {
                        switch (video.Status.PrivacyStatus)
                        {
                            case "deleted":
                                State = VideoStateByProvider.DELETED;
                                break;
                            case "failed":
                                State = VideoStateByProvider.FAILED;
                                break;
                            case "rejected":
                                State = VideoStateByProvider.REJECTED;
                                break;
                            default:
                                break;
                        }
                        UsageFailureReason = video.Status.LifeCycleStatus;
                    }
                    else if (video.ContentDetails.EnableEmbed != null && !video.ContentDetails.EnableEmbed.Value)
                    {
                        State = VideoStateByProvider.RESTRICTED;
                        UsageFailureReason = "video is not embeddable on other sites";
                    }
                }
            }
        }
    }
}
