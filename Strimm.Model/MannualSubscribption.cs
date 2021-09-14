using System;
using System.Runtime.Serialization;


namespace Strimm.Model
{
    [DataContract]
    public class MannualSubscribption
    {
        private int ipnMessageId;
        private int orderId;
        private int orderTransactionId;
        private int userId;
        private int payingSubscriberId;
        private DateTime date_Create;
        private DateTime date_TrialPeriodStart;
        private DateTime date_SubscriptionStart;
        private string firstName;
        private string lastName;
        private string payPalSubscriberId;
        private string payPalPayerId;
        private string payPalPayerStatus;
        private string payPalBusinessEmail;
        private string payPalPayerEmail;
        private string payPalReceiverEmail;
        private string payPalResidenceCountry;
        private string payPalSubscriptionDate;
        private string pubscriptionPlan;
        private string payPalSubscriptionId;
        private string buttonId;
        private double trialPaymentAmount;
        private double cycleBillingAmount;
        private string trialPeriod;
        private string billingCycle;
        private string currency;
        private DateTime createdDate;
        private string subscriptionPlan;

        [DataMember]
        public int IpnMessageId
        {
            get
            {
                return this.ipnMessageId;
            }
            set
            {
                this.ipnMessageId = value;
            }
        }

        [DataMember]
        public int OrderId
        {
            get
            {
                return this.orderId;
            }
            set
            {
                this.orderId = value;
            }
        }

        [DataMember]
        public int OrderTransactionId
        {
            get
            {
                return this.orderTransactionId;
            }
            set
            {
                this.orderTransactionId = value;
            }
        }

        [DataMember]
        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        [DataMember]
        public int PayingSubscriberId
        {
            get
            {
                return this.payingSubscriberId;
            }
            set
            {
                this.payingSubscriberId = value;
            }
        }

        [DataMember]
        public DateTime Date_Create
        {
            get
            {
                return this.date_Create;
            }
            set
            {
                this.date_Create = value;
            }
        }

        [DataMember]
        public DateTime Date_TrialPeriodStart
        {
            get
            {
                return this.date_TrialPeriodStart;
            }
            set
            {
                this.date_TrialPeriodStart = value;
            }
        }

        [DataMember]
        public DateTime Date_SubscriptionStart
        {
            get
            {
                return this.date_SubscriptionStart;
            }
            set
            {
                this.date_SubscriptionStart = value;
            }
        }

        [DataMember]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        [DataMember]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        [DataMember]
        public string PayPalSubscriberId
        {
            get
            {
                return this.payPalSubscriberId;
            }
            set
            {
                this.payPalSubscriberId = value;
            }
        }

        [DataMember]
        public string PayPalPayerId
        {
            get
            {
                return this.payPalPayerId;
            }
            set
            {
                this.payPalPayerId = value;
            }
        }

        [DataMember]
        public string PayPalPayerStatus
        {
            get
            {
                return this.payPalPayerStatus;
            }
            set
            {
                this.payPalPayerStatus = value;
            }
        }

        [DataMember]
        public string PayPalBusinessEmail
        {
            get
            {
                return this.payPalBusinessEmail;
            }
            set
            {
                this.payPalBusinessEmail = value;
            }
        }

        [DataMember]
        public string PayPalPayerEmail
        {
            get
            {
                return this.payPalPayerEmail;
            }
            set
            {
                this.payPalPayerEmail = value;
            }
        }

        [DataMember]
        public string PayPalReceiverEmail
        {
            get
            {
                return this.payPalReceiverEmail;
            }
            set
            {
                this.payPalReceiverEmail = value;
            }
        }

        [DataMember]
        public string PayPalResidenceCountry
        {
            get
            {
                return this.payPalResidenceCountry;
            }
            set
            {
                this.payPalResidenceCountry = value;
            }
        }

        [DataMember]
        public string PayPalSubscriptionDate
        {
            get
            {
                return this.payPalSubscriptionDate;
            }
            set
            {
                this.payPalSubscriptionDate = value;
            }
        }

        [DataMember]
        public string SubscriptionPlan
        {
            get
            {
                return this.subscriptionPlan;
            }
            set
            {
                this.subscriptionPlan = value;
            }
        }

        [DataMember]
        public string PayPalSubscriptionId
        {
            get
            {
                return this.payPalSubscriptionId;
            }
            set
            {
                this.payPalSubscriptionId = value;
            }
        }

        [DataMember]
        public string ButtonId
        {
            get
            {
                return this.buttonId;
            }
            set
            {
                this.buttonId = value;
            }
        }

        [DataMember]
        public double TrialPaymentAmount
        {
            get
            {
                return this.trialPaymentAmount;
            }
            set
            {
                this.trialPaymentAmount = value;
            }
        }

        [DataMember]
        public double CycleBillingAmount
        {
            get
            {
                return this.cycleBillingAmount;
            }
            set
            {
                this.cycleBillingAmount = value;
            }
        }

        [DataMember]
        public string TrialPeriod
        {
            get
            {
                return this.trialPeriod;
            }
            set
            {
                this.trialPeriod = value;
            }
        }

        [DataMember]
        public string BillingCycle
        {
            get
            {
                return this.billingCycle;
            }
            set
            {
                this.billingCycle = value;
            }
        }

        [DataMember]
        public string Currency
        {
            get
            {
                return this.currency;
            }
            set
            {
                this.currency = value;
            }
        }

        [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set
            {
                this.createdDate = value;
            }
        }


    }
}
