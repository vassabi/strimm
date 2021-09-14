using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StrimmTube.Utils
{
    public class StripeOptions
    {
        public StripeOptions()
        {
            this.PublishableKey = ConfigurationManager.AppSettings["StripePublishableKey"];
            this.SecretKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            this.PaymentSuccessUrl = ConfigurationManager.AppSettings["StripePaymentSuccessUrl"];
            this.PaymentFailUrl = ConfigurationManager.AppSettings["StripePaymentFailUrl"];
            this.DomainToRedirect = ConfigurationManager.AppSettings["StripeDomainToRedirect"];
            this.ProductBasicId = ConfigurationManager.AppSettings["StripeProductBasicId"];
            this.ProductBasicAnnualId = ConfigurationManager.AppSettings["StripeProductBasicAnnualId"];
            this.ProductAdvancedId = ConfigurationManager.AppSettings["StripeProductAdvancedId"];
            this.ProductAdvancedIAnnualId = ConfigurationManager.AppSettings["StripeProductAdvancedIAnnualId"];
            this.ProductProfessionalId = ConfigurationManager.AppSettings["StripeProductProfessionalId"];
            this.ProductProfessionalAnnualId = ConfigurationManager.AppSettings["StripeProductProfessionalAnnualId"];
        }

        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
        public string DomainToRedirect { get; set; }
        public string PaymentSuccessUrl { get; set; }
        public string PaymentFailUrl { get; set; }
        public string ProductBasicId { get; set; }
        public string ProductBasicAnnualId { get; set; }
        public string ProductAdvancedId { get; set; }
        public string ProductAdvancedIAnnualId { get; set; }
        public string ProductProfessionalId { get; set; }
        public string ProductProfessionalAnnualId { get; set; }

    }
}