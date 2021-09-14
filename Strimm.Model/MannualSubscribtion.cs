using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class MannualSubscribtion
    {
       private int IpnMessageId { get; set; }
        private int OrderId { get; set; }
        private int OrderTransactionId { get; set; }
        private int UserId { get; set; }
        private int PayingSubscriberId { get; set; }
        private DateTime Date_Create { get; set; }
        private DateTime Date_TrialPeriodStart { get; set; }
        private DateTime Date_SubscriptionStart { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string PayPalSubscriberId { get; set; }
        private string PayPalPayerId { get; set; }
        private string PayPalPayerStatus { get; set; }
        private string PayPalBusinessEmail { get; set; }
        private string PayPalPayerEmail { get; set; }
        private string PayPalReceiverEmail { get; set; }
        private string PayPalResidenceCountry { get; set; }
        private string PayPalSubscriptionDate { get; set; }
        private string SubscriptionPlan { get; set; }
        private string PayPalSubscriptionId { get; set; }
        private string ButtonId { get; set; }
        private double TrialPaymentAmount { get; set; }
        private double CycleBillingAmount { get; set; }
        private string TrialPeriod { get; set; }
        private string illingCycle { get; set; }
        private string Currency { get; set; }
        private DateTime reatedDate { get; set; }



        private int IpnMessageId { get; set; }
        private int OrderId { get; set; }
        private int OrderTransactionId { get; set; }
        private int UserId { get; set; }
        private int PayingSubscriberId { get; set; }
        private DateTime Date_Create { get; set; }
        private DateTime Date_TrialPeriodStart { get; set; }
        private DateTime Date_SubscriptionStart { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string PayPalSubscriberId { get; set; }
        private string PayPalPayerId { get; set; }
        private string PayPalPayerStatus { get; set; }
        private string PayPalBusinessEmail { get; set; }
        private string PayPalPayerEmail { get; set; }
        private string PayPalReceiverEmail { get; set; }
        private string PayPalResidenceCountry { get; set; }
        private string PayPalSubscriptionDate { get; set; }
        private string SubscriptionPlan { get; set; }
        private string PayPalSubscriptionId { get; set; }
        private string ButtonId { get; set; }
        private double TrialPaymentAmount { get; set; }
        private double CycleBillingAmount { get; set; }
        private string TrialPeriod { get; set; }
        private string illingCycle { get; set; }
        private string Currency { get; set; }
        private DateTime reatedDate { get; set; }
    }
}
