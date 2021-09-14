using Strimm.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    public class IpmMessage
    {
        public IpmMessage(string rawMessage)
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
                ParentTxnId = data["parent_txn_id"] != null ? data["parent_txn_id"].ToString() : null;
                TxnId = data["txn_id"] != null ? data["txn_id"].ToString() : null;
                TxnType = data["txn_type"] != null ? data["txn_type"].ToString() : null;
                TransactionSubject = data["transaction_subject"] != null ? data["transaction_subject"].ToString() : null;
                Custom = data["custom"] != null ? data["custom"].ToString() : null;
                ItemName = data["item_name"] != null ? data["item_name"].ToString() : null;
                ItemNumber = data["item_number"] != null ? data["item_number"].ToString() : null;
                OptionName1 = data["option_name1"] != null ? data["option_name1"].ToString() : null;
                OptionSelection1 = data["option_selection1"] != null ? data["option_selection1"].ToString() : null;
                PaymentType = data["payment_type"] != null ? data["payment_type"].ToString() : null;
                PaymentReason = data["payment_reason"] != null ? data["payment_reason"].ToString() : null;
                PaymentStatus = data["payment_status"] != null ? data["payment_status"].ToString() : null;
                PaymentDate = data["payment_date"] != null ? data["payment_date"].ToString() : null;
                ReasonCode = data["reason_code"] != null ? data["reason_code"].ToString() : null;
                ProtectionEligibility = data["protection_eligibility"] != null ? data["protection_eligibility"].ToString() : null;
                PayerId = data["payer_id"] != null ? data["payer_id"].ToString() : null;
                FirstName = data["first_name"] != null ? data["first_name"].ToString() : null;
                LastName = data["last_name"] != null ? data["last_name"].ToString() : null;
                PayerBusinessName = data["payer_business_name"] != null ? data["payer_business_name"].ToString() : null;
                PayerStatus = data["payer_status"] != null ? data["payer_status"].ToString() : null;
                BusinessEmail = data["business"] != null ? data["business"].ToString() : null;
                PayerEmail = data["payer_email"] != null ? data["payer_email"].ToString() : null;
                AddressCountry = data["address_country"] != null ? data["address_country"].ToString() : null;
                AddressCity = data["address_city"] != null ? data["address_city"].ToString() : null;
                AddressCountryCode = data["address_country_code"] != null ? data["address_country_code"].ToString() : null;
                AddressName = data["address_name"] != null ? data["address_name"].ToString() : null;
                AddressState = data["address_state"] != null ? data["address_state"].ToString() : null;
                AddressStatus = data["address_status"] != null ? data["address_status"].ToString() : null;
                AddressStreet = data["address_street"] != null ? data["address_street"].ToString() : null;
                AddressZip = data["address_zip"] != null ? data["address_zip"].ToString() : null;
                ContactPhone = data["contact_phone"] != null ? data["contact_phone"].ToString() : null;
                ResidenceCountry = data["residence_country"] != null ? data["residence_country"].ToString() : null;
                ReceiverId = data["receiver_id"] != null ? data["receiver_id"].ToString() : null;
                ReceiverEmail = data["receiver_email"] != null ? data["receiver_email"].ToString() : null;
                Quantity = data["quantity"] != null ? data["quantity"].ToString().GetValueOrNull<int>().Value : 0;
                McGross = data["mc_gross"] != null ? data["mc_gross"].ToString().GetValueOrNull<decimal>() : null;
                McFee = data["mc_fee"] != null ? data["mc_fee"].ToString().GetValueOrNull<decimal>() : null;
                HandlingAmount = data["mc_handling"] != null ? data["mc_handling"].ToString().GetValueOrNull<decimal>() : null;
                Shipping = data["mc_shipping"] != null ? data["mc_shipping"].ToString().GetValueOrNull<decimal>() : null;
                PaymentFee = data["payment_fee"] != null ? data["payment_fee"].ToString().GetValueOrNull<decimal>() : null;
                PaymentGross = data["payment_gross"] != null ? data["payment_gross"].ToString().GetValueOrNull<decimal>() : null;
                ExchangeRate = data["exchange_rate"] != null ? data["exchange_rate"].ToString().GetValueOrNull<decimal>() : null;
                SettleAmount = data["settle_amount"] != null ? data["settle_amount"].ToString().GetValueOrNull<decimal>() : null;
                SettleCurrency = data["settle_currency"] != null ? data["settle_currency"].ToString() : null;
                Amount1 = data["amount1"] != null ? data["amount1"].ToString().GetValueOrNull<decimal>() : null;
                Amount2 = data["amount2"] != null ? data["amount2"].ToString().GetValueOrNull<decimal>() : null;
                Amount3 = data["amount3"] != null ? data["amount3"].ToString().GetValueOrNull<decimal>() : null;
                McAmount1 = data["mc_amount1"] != null ? data["mc_amount1"].ToString().GetValueOrNull<decimal>() : null;
                McAmount2 = data["mc_amount2"] != null ? data["mc_amount2"].ToString().GetValueOrNull<decimal>() : null;
                McAmount3 = data["mc_amount3"] != null ? data["mc_amount3"].ToString().GetValueOrNull<decimal>() : null;
                Period1 = data["period1"] != null ? data["period1"].ToString() : null;
                Period2 = data["period2"] != null ? data["period2"].ToString() : null;
                Period3 = data["period3"] != null ? data["period3"].ToString() : null;
                Reattempt = data["reattempt"] != null ? data["reattempt"].ToString() : null;
                RecurTimes = data["recur_times"] != null ? data["recur_times"].ToString().GetValueOrNull<int>() : null;
                RetryAt = data["retry_at"] != null ? data["retry_at"].ToString() : null;
                SubscriptionDate = data["subscr_date"] != null ? data["subscr_date"].ToString() : null;
                SubscriptionEffective = data["subscr_effective"] != null ? data["subscr_effective"].ToString() : null;
                SubscriberId = data["subscr_id"] != null ? data["subscr_id"].ToString() : null;
                Memo = data["memo"] != null ? data["memo"].ToString() : null;
                Resend = data["resend"] != null ? data["resend"].ToString().GetValueOrNull<bool>() : false;
                McCurrency = data["mc_currency"] != null ? data["mc_currency"].ToString() : null;
                BuyerAdditionalInformation = data["buyer_additional_information"] != null ? data["buyer_additional_information"].ToString() : null;
                CaseCreationDate = data["case_creation_date"] != null ? data["case_creation_date"].ToString() : null;
                CaseId = data["case_id"] != null ? data["case_id"].ToString() : null;
                CaseType = data["case_type"] != null ? data["case_type"].ToString() : null;

                NotifyVersion = data["notify_version"] != null ? data["notify_version"].ToString() : null;
                VerifySign = data["verify_sign"] != null ? data["verify_sign"].ToString() : null;
                TestIpn = data["test_ipn"] != null ? data["test_ipn"].ToString() : null;
            }
        }

        public int IpmMessageId { get; set; }

        public string IpnTrackingId { get; set; }

        public string RawMessage { get; set; }

        public string ParentTxnId { get; set; }

        public string TxnId { get; set; }

        public string TxnType { get; set; }

        public string TransactionSubject { get; set; }

        public string Custom { get; set; }

        public string ItemName { get; set; }

        public string ItemNumber { get; set; }

        public string OptionName1 { get; set; }

        public string OptionSelection1 { get; set; }

        public string PaymentType { get; set; }

        public string PaymentReason { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentDate { get; set; }

        public string ReasonCode { get; set; }

        public string ProtectionEligibility { get; set; }

        public string PayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PayerBusinessName { get; set; }

        public string PayerStatus { get; set; }

        public string BusinessEmail { get; set; }

        public string PayerEmail { get; set; }

        public string AddressCountry { get; set; }

        public string AddressCity { get; set; }

        public string AddressCountryCode { get; set; }

        public string AddressName { get; set; }

        public string AddressState { get; set; }

        public string AddressStatus { get; set; }

        public string AddressStreet { get; set; }

        public string AddressZip { get; set; }

        public string ContactPhone { get; set; }

        public string ResidenceCountry { get; set; }

        public string RecieverId { get; set; }

        public string ReceiverEmail { get; set; }

        public int Quantity { get; set; }

        public decimal? McGross { get; set; }

        public decimal? McFee { get; set; }

        public decimal? HandlingAmount { get; set; }

        public decimal? Shipping { get; set; }

        public decimal? PaymentFee { get; set; }

        public decimal? PaymentGross { get; set; }

        public decimal? SettleAmount { get; set; }

        public string SettleCurrency { get; set; }

        public decimal? Amount1 { get; set; }

        public decimal? Amount2 { get; set; }

        public decimal? Amount3 { get; set; }

        public decimal? McAmount1 { get; set; }

        public decimal? McAmount2 { get; set; }

        public decimal? McAmount3 { get; set; }

        public string Period1 { get; set; }

        public string Period2 { get; set; }

        public string Period3 { get; set; }

        public string Reattempt { get; set; }

        public int? RecurTimes { get; set; }

        public string SubscriptionDate { get; set; }

        public string SubscriptionEffective { get; set; }

        public string SubscriberId { get; set; }

        public string Memo { get; set; }

        public bool? Resend { get; set; }
        
        public string McCurrency { get; set; }

        public string NotifyVersion { get; set; }

        public string VerifySign { get; set; }

        public string TestIpn { get; set; }

        public string ReceiverId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public string RetryAt { get; set; }

        public string BuyerAdditionalInformation { get; set; }

        public string CaseCreationDate { get; set; }

        public string CaseId { get; set; }

        public string CaseType { get; set; }
    }
}
