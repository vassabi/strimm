using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public bool? IsCustomPricing { get; set; }

        public bool IsPricePerChannel { get; set; }

        public bool? IncludesUnlimitedEmails { get; set; }

        public bool? IncludesWhileLabeling { get; set; }

        public bool? IncludesPhoneSupport { get; set; }

        public int? IncludesFreePhoneSupportTimeInMin { get; set; }

        public string IncludesFreePhoneSupportInterval { get; set; }

        public bool? IncludesAccountManager { get; set; }

        public bool? IncludesChannelManagement { get; set; }

        public bool? IncludesMonthlyVisitorsReport { get; set; }

        public int? TrialPeriodInDays { get; set; }

        public bool? IsSubscription { get; set; }

        public decimal? SetupFee { get; set; }

        public DateTime? AsOfDate { get; set; }

        public DateTime AddedDate { get; set; }

        public bool? IncludesCustomBranding { get; set; }

        public bool? IncludesChannelMuting { get; set; }

        public bool? IncludesChannelPasswordProtection { get; set; }

        public bool? IncludesChannelSetupAssistance { get; set; }

	    public bool? IncludesChannelFeaturing { get; set; }

        public decimal? InitialFee { get; set; }

        public int MaxChannelCount { get; set; }

        public bool? IncludePrivateVideos { get; set; }

        public bool? IncludeMatureContent { get; set; }

        public bool? IncludeCustomPlayerControls { get; set; }

        public bool? IncludeEmbedOnlyMode { get; set; }

        public bool PrivateVideoModeSupport { get; set; }

        public bool MatureContentSupport { get; set; }

        public bool PersonalAdvertisementSupport { get; set; }

        public decimal? AnnualPrice { get; set; }

        public string SubscrMonthlyWithTrialButtonId { get; set; }

        public string SubscrMonthlyNoTrialButtonId { get; set; }

        public string SubscrAnnualWithTrialButtonId { get; set; }

        public string SubscrAnnualNoTrialButtonId { get; set; }

        public string SubscrMonthlyUpgrade1ButtonId { get; set; }

        public string SubscrMonthlyUpgrade2ButtonId { get; set; }

        public string SubscrAnnualUpgrade1ButtonId { get; set; }

        public string SubscrAnnualUpgrade2ButtonId { get; set; }

        public string UnSubscrButtonId { get; set; }
    }
}
