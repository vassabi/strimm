using Strimm.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    public class IpnMessage
    {
        public IpnMessage() { }

        public IpnMessage(string rawMessage)
        {
            if (!String.IsNullOrEmpty(rawMessage))
            {
                var data = new Hashtable();
                rawMessage.Split('&').ToList().ForEach(segment =>
                {
                    data.Add(segment.Split('=')[0], segment.Split('=')[1]);
                });

                IpnTrackingId = data["ipn_track_id"] != null ? data["ipn_track_id"].ToString() : null;
                RawMessage = rawMessage;
                TxnType = data["txn_type"] != null ? data["txn_type"].ToString() : null;
                ItemName = data["item_name"] != null ? data["item_name"].ToString() : null;
                ItemNumber = data["item_number"] != null ? data["item_number"].ToString() : null;
                OptionName1 = data["option_name1"] != null ? data["option_name1"].ToString() : null;
                OptionSelection1 = data["option_selection1"] != null ? data["option_selection1"].ToString() : null;
                OptionName2 = data["option_name2"] != null ? data["option_name2"].ToString() : null;
                OptionSelection2 = data["option_selection2"] != null ? data["option_selection2"].ToString() : null;
                PayerId = data["payer_id"] != null ? data["payer_id"].ToString() : null;
                FirstName = data["first_name"] != null ? data["first_name"].ToString() : null;
                LastName = data["last_name"] != null ? data["last_name"].ToString() : null;
                PayerStatus = data["payer_status"] != null ? data["payer_status"].ToString() : null;
                BusinessEmail = data["business"] != null ? data["business"].ToString() : null;
                PayerEmail = data["payer_email"] != null ? data["payer_email"].ToString() : null;
                ResidenceCountry = data["residence_country"] != null ? data["residence_country"].ToString() : null;
                ReceiverEmail = data["receiver_email"] != null ? data["receiver_email"].ToString() : null;
                Amount1 = data["amount1"] != null ? data["amount1"].ToString().GetValueOrNull<decimal>() : null;
                Amount3 = data["amount3"] != null ? data["amount3"].ToString().GetValueOrNull<decimal>() : null;
                McAmount1 = data["mc_amount1"] != null ? data["mc_amount1"].ToString().GetValueOrNull<decimal>() : null;
                McAmount3 = data["mc_amount3"] != null ? data["mc_amount3"].ToString().GetValueOrNull<decimal>() : null;
                Period1 = data["period1"] != null ? data["period1"].ToString() : null;
                Period3 = data["period3"] != null ? data["period3"].ToString() : null;
                Reattempt = data["reattempt"] != null ? data["reattempt"].ToString() : null;
                RecurTimes = data["recur_times"] != null ? data["recur_times"].ToString().GetValueOrNull<int>() : null;
                SubscriptionDate = data["subscr_date"] != null ? data["subscr_date"].ToString() : null;
                SubscriberId = data["subscr_id"] != null ? data["subscr_id"].ToString() : null;
                McCurrency = data["mc_currency"] != null ? data["mc_currency"].ToString() : null;
                NotifyVersion = data["notify_version"] != null ? data["notify_version"].ToString() : null;
                VerifySign = data["verify_sign"] != null ? data["verify_sign"].ToString() : null;
                TestIpn = data["test_ipn"] != null ? data["test_ipn"].ToString() : null;
                BtnId = data["btn_id"] != null ? data["btn_id"].ToString() : null;
                TxnId = data["txn_id"] != null ? data["txn_id"].ToString() : null;
                PaymentStatus = data["payment_status"] != null ? data["payment_status"].ToString() : null;
                PaymentDate = data["payment_date"] != null ? data["payment_date"].ToString() : null;
                PaymentNumber = data["id"] != null ? data["id"].ToString().GetValueOrNull<int>() : null;
                PaymentGross = data["payment_gross"] != null ? data["payment_gross"].ToString().GetValueOrNull<decimal>() : null;
                PaymentType = data["payment_type"] != null ? data["payment_type"].ToString() : null;
                PaymentFee = data["payment_fee"] != null ? data["payment_fee"].ToString().GetValueOrNull<decimal>() : null;
                McFee = data["mc_fee"] != null ? data["mc_fee"].ToString().GetValueOrNull<decimal>() : null;
                McGross = data["mc_gross"] != null ? data["mc_gross"].ToString().GetValueOrNull<decimal>() : null;
                ProtectionEligibility = data["protection_eligibility"] != null ? data["protection_eligibility"].ToString() : null;
                TransactionSubject = data["transaction_subject"] != null ? data["transaction_subject"].ToString() : null;
                ReceiverId = data["receiver_id"] != null ? data["receiver_id"].ToString() : null;
            }
        }

        public IpnMessage(string pdtMessage, string delimeter="&")
        {
            var data = new Hashtable();
            pdtMessage.Split(delimeter.ToCharArray()).ToList().ForEach(segment =>
            {
                string[] parts = segment.Split('=');
                if (parts.Length == 2)
                {
                    data.Add(parts[0], parts[1]);
                }
            });

            IpnTrackingId = data["ipn_track_id"] != null ? data["ipn_track_id"].ToString() : null;
            RawMessage = pdtMessage;
            TxnType = data["txn_type"] != null ? data["txn_type"].ToString() : null;
            ItemName = data["item_name"] != null ? data["item_name"].ToString() : null;
            ItemNumber = data["item_number"] != null ? data["item_number"].ToString() : null;
            OptionName1 = data["option_name1"] != null ? data["option_name1"].ToString() : null;
            OptionSelection1 = data["option_selection1"] != null ? data["option_selection1"].ToString() : null;
            OptionName2 = data["option_name2"] != null ? data["option_name2"].ToString() : null;
            OptionSelection2 = data["option_selection2"] != null ? data["option_selection2"].ToString() : null;
            PayerId = data["payer_id"] != null ? data["payer_id"].ToString() : null;
            FirstName = data["first_name"] != null ? data["first_name"].ToString() : null;
            LastName = data["last_name"] != null ? data["last_name"].ToString() : null;
            PayerStatus = data["payer_status"] != null ? data["payer_status"].ToString() : null;
            BusinessEmail = data["business"] != null ? data["business"].ToString() : null;
            PayerEmail = data["payer_email"] != null ? data["payer_email"].ToString() : null;
            ResidenceCountry = data["residence_country"] != null ? data["residence_country"].ToString() : null;
            ReceiverEmail = data["receiver_email"] != null ? data["receiver_email"].ToString() : null;
            Amount1 = data["amount1"] != null ? data["amount1"].ToString().GetValueOrNull<decimal>() : null;
            Amount3 = data["amount3"] != null ? data["amount3"].ToString().GetValueOrNull<decimal>() : null;
            McAmount1 = data["mc_amount1"] != null ? data["mc_amount1"].ToString().GetValueOrNull<decimal>() : null;
            McAmount3 = data["mc_amount3"] != null ? data["mc_amount3"].ToString().GetValueOrNull<decimal>() : null;
            Period1 = data["period1"] != null ? data["period1"].ToString() : null;
            Period3 = data["period3"] != null ? data["period3"].ToString() : null;
            Reattempt = data["reattempt"] != null ? data["reattempt"].ToString() : null;
            RecurTimes = data["recur_times"] != null ? data["recur_times"].ToString().GetValueOrNull<int>() : null;
            SubscriptionDate = data["subscr_date"] != null ? data["subscr_date"].ToString() : null;
            SubscriberId = data["subscr_id"] != null ? data["subscr_id"].ToString() : null;
            McCurrency = data["mc_currency"] != null ? data["mc_currency"].ToString() : null;
            NotifyVersion = data["notify_version"] != null ? data["notify_version"].ToString() : null;
            VerifySign = data["verify_sign"] != null ? data["verify_sign"].ToString() : null;
            TestIpn = data["test_ipn"] != null ? data["test_ipn"].ToString() : null;
            BtnId = data["btn_id"] != null ? data["btn_id"].ToString() : null;
            TxnId = data["txn_id"] != null ? data["txn_id"].ToString() : null;
            PaymentStatus = data["payment_status"] != null ? data["payment_status"].ToString() : null;
            PaymentDate = data["payment_date"] != null ? data["payment_date"].ToString() : null;
            PaymentNumber = data["id"] != null ? data["id"].ToString().GetValueOrNull<int>() : null;
            PaymentGross = data["payment_gross"] != null ? data["payment_gross"].ToString().GetValueOrNull<decimal>() : null;
            PaymentType = data["payment_type"] != null ? data["payment_type"].ToString() : null;
            PaymentFee = data["payment_fee"] != null ? data["payment_fee"].ToString().GetValueOrNull<decimal>() : null;
            McFee = data["mc_fee"] != null ? data["mc_fee"].ToString().GetValueOrNull<decimal>() : null;
            McGross = data["mc_gross"] != null ? data["mc_gross"].ToString().GetValueOrNull<decimal>() : null;
            ProtectionEligibility = data["protection_eligibility"] != null ? data["protection_eligibility"].ToString() : null;
            TransactionSubject = data["transaction_subject"] != null ? data["transaction_subject"].ToString() : null;
            ReceiverId = data["receiver_id"] != null ? data["receiver_id"].ToString() : null;
        }

        public int IpnMessageId { get; set; }

        public string IpnTrackingId { get; set; }

        public string RawMessage { get; set; }

        public string TxnType { get; set; }

        public string ItemName { get; set; }

        public string ItemNumber { get; set; }

        public string BtnId { get; set; }

        public string OptionName1 { get; set; }

        public string OptionSelection1 { get; set; }

        public string PayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PayerStatus { get; set; }

        public string BusinessEmail { get; set; }

        public string PayerEmail { get; set; }

        public string ResidenceCountry { get; set; }

        public string RecieverId { get; set; }

        public string ReceiverEmail { get; set; }

        public decimal? Amount1 { get; set; }

        public decimal? Amount3 { get; set; }

        public decimal? McAmount1 { get; set; }

        public decimal? McAmount3 { get; set; }

        public string Period1 { get; set; }

        public string Period3 { get; set; }

        public string Reattempt { get; set; }

        public int? RecurTimes { get; set; }

        public string SubscriptionDate { get; set; }

        public string SubscriberId { get; set; }
        
        public string McCurrency { get; set; }

        public string NotifyVersion { get; set; }

        public string VerifySign { get; set; }

        public string TestIpn { get; set; }

        public string ReceiverId { get; set; }

        public string TxnId { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentDate { get; set; }

        public int? PaymentNumber { get; set; }

        public decimal? PaymentGross { get; set; }

        public string PaymentType { get; set; }

        public decimal? PaymentFee { get; set; }

        public decimal? McFee { get; set; }

        public decimal? McGross { get; set; }

        public string ProtectionEligibility { get; set; }

        public string TransactionSubject { get; set; }

        public string OptionName2 { get; set; }

        public string OptionSelection2 { get; set; }
    }
}
