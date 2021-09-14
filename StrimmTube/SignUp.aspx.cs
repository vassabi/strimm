using Strimm.Model.Projections;
using Strimm.Shared;
using Strimm.Model;
using StrimmTube.WebServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StrimmBL;
using Strimm.Data.Repositories;
using log4net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using StrimmTube.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace StrimmTube
{
    public partial class SignUp : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SignUp));
        
        private int SelectedYear { get; set; }

        private int SelectedMonth { get; set; }

        private int SelectedDay { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            txtEmail.Attributes.Add("autocomplete", "off");
            txtReenterEmail.Attributes.Add("autocomplete", "off");
            txtPasswordSignUp.Attributes.Add("autocomplete", "off");
            txtUserNameSignUp.Attributes.Add("autocomplete", "off");
            
            if (!IsPostBack)
            {
                if (!IsProd)
                {
                    TurnOffCrawling();
                }

                txtEmail.Text = String.Empty;
                txtFirstName.Text = String.Empty;
                txtLastName.Text = String.Empty;
                txtPasswordSignUp.Text = String.Empty;
                txtReenterEmail.Text = String.Empty;
                txtUserNameSignUp.Text = String.Empty;

                BindDateOfBirth();
                BindCountries();
            }
        }

        private void BindDateOfBirth()
        {
            var today = DateTime.Now;
            var years = new ArrayList();
            var months = new ArrayList();
            var days = new ArrayList();

            for (int i = today.Year; i >= 1924; i--)
            {
                years.Add(i);
            }

            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }

            days = DateTimeUtils.BindDays(today.Year, today.Month);

            years.Insert(0, "Year");
            months.Insert(0, "Month");
            days.Insert(0, "Day");
            
            ddlYear.DataSource = years;
            ddlYear.DataBind();

            ddlMonth.DataSource = months;
            ddlMonth.DataBind();

            ddlDay.DataSource = days;
            ddlDay.DataBind();

            ddlYear.SelectedValue = "Year";
            ddlMonth.SelectedValue = "Month";
            ddlDay.SelectedValue = "Day";
        }

        private void BindCountries()
        {
            var countries = CountriesManage.GetAllCountries();

            countries.Insert(0, new Country() { CountryId = 0, Name = "Select Country" });

            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataSource = countries;
            ddlCountry.SelectedValue = "0"; // countries.Single(x => x.CountryId == 223).CountryId.ToString();

            ddlCountry.DataBind();
        }

        private int GetSelectedYear()
        {
            int year = 0;
            Int32.TryParse(ddlYear.SelectedValue, out year);
            return year;
        }

        private int GetSelectedMonth()
        {
            int month = 0;
            Int32.TryParse(ddlMonth.SelectedValue, out month);
            return month;
        }

        private int GetSelectedDay()
        {
            int day = 0;
            Int32.TryParse(ddlDay.SelectedValue, out day);
            return day;
        }

        private void SetDays()
        {
            int day = GetSelectedDay();
            int month = GetSelectedMonth();
            int year = GetSelectedYear();

            DateTime today = DateTime.Now;

            var days = DateTimeUtils.BindDays((year > 0 ? year : today.Year), (month > 0 ? month : today.Month));
            days.Insert(0, "Day");
            ddlDay.DataSource = days;
            ddlDay.DataBind();

            ddlDay.SelectedValue = day == 0 ? "Day" : days.Contains(day) ? Convert.ToString(day) : "Day";
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDays();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDays();
        }

        protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/home", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void CheckPasswordStrength(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = UserManage.IsStrongPassword(args.Value);
        }

        protected void CheckUsernameUniqueness(object source, ServerValidateEventArgs args)        
        {
            args.IsValid = UserManage.IsUserNameUnique(args.Value);
        }

        protected void CheckEmailUniqueness(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !UserManage.IsEmailExist(args.Value);
        }

        protected void CheckUserAge(object source, ServerValidateEventArgs args)
        {
            args.IsValid = IsValidAge();
        }

        protected bool IsValidAge()
        {
            var year = GetSelectedYear();
            var month = GetSelectedMonth();
            var day = GetSelectedDay();

            DateTime birthdate = DateTime.MinValue;

            if (year > 0 && month > 0 && day > 0)
            {
                birthdate = new DateTime(GetSelectedYear(), GetSelectedMonth(), GetSelectedDay());
            }

            int age = birthdate != DateTime.MinValue ? DateTimeUtils.GetAge(birthdate) : 0;

            return (age > 18);
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {            
            if(!ValidateReCaptcha())
            {
                lblMessage.Text = "Verification process not passed";
                return;
            }

            Logger.Debug("New user is joining Strimm");

            string userName = String.Empty;
            string password = String.Empty;
            string email = txtEmail.Text.ToLower();
            string recoveryEmail = String.Empty;
            string firstName = String.Empty;
            string lastname = String.Empty;
            DateTime birthdate = new DateTime();
            string country = String.Empty;
            string gender = String.Empty;
            bool agreeWithTerms = false;
            bool isUserExist = false;
            bool isUserTemp = false;
            bool isFilmMaker = false;

            if (!String.IsNullOrEmpty(email))
            {
                var user = UserManage.GetUserPoByEmail(email);

                isUserExist = user != null;
                isUserTemp = user != null ? user.IsTempUser : false;
            }

            if (isUserExist)
            {
                if (isUserTemp)
                {
                    var proxy = new UserService();
                    string message = proxy.ResendActivationLink(email);
                    lblMessage.Text = "Email already exists.</br> Activation link has been resend to your email";

                    return;
                }
                else
                {
                    lblMessage.Text = "You are already an active Strimm user.";

                    return;
                }
            }
            else
            {
                firstName = txtFirstName.Text.Trim();
                lastname = txtLastName.Text.Trim();
                List<string> wordsInName = new List<string>();
                var publicName = txtUserNameSignUp.Text.TrimEnd();
                publicName = PublicNameUtils.UrlDecodePublicName(publicName);

                if (!PublicNameUtils.IsValidPublicName(publicName))
                {
                    this.lblMessage.Text = "Invalid Public Name entered. Only space, apostrophe, '-' and '&' are allowed in the Public Name.";
                    return;
                }

                if (!UserManage.IsUserNameUnique(publicName))
                {
                    this.lblMessage.Text = "Public Name entered is not unique. Please re-enter.";
                    return;
                }

                userName = publicName;

                //As per request in this task https://trello.com/c/yRymna8w/5-update-sign-up-page, gender field should be removed
                //All further registrations will have gender = CUSTOM
                //gender = rdbGender.SelectedValue.Trim(); //This line was commented on 02/08/2021 by Karen A.
                gender = "CUSTOM";

                if (String.IsNullOrEmpty(txtPasswordSignUp.Text))
                {
                    lblMessage.Text = "Invalid password specified";
                    spnErPass.Attributes.CssStyle.Add("display", "block");
                    txtPasswordSignUp.Attributes.Add("class", "errorActive");
      
                    return;
                }

                password = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(email, txtPasswordSignUp.Text));

                //Gender field removed task https://trello.com/c/yRymna8w/5-update-sign-up-page
                //if (String.IsNullOrEmpty(gender))
                //{
                //    erGender.Attributes.CssStyle.Add("display", "block");
                //    return;
                //}

                if (ddlCountry.SelectedIndex != 0)
                {
                    country = ddlCountry.SelectedItem.Text;
                }
                else
                {
                    lblMessage.Text = "Please select a country";                                              
                    return;
                }


                int day = 0;
                int month = 0;
                int year = 0;

                Int32.TryParse(ddlDay.SelectedValue, out day);
                Int32.TryParse(ddlMonth.SelectedValue, out month);
                Int32.TryParse(ddlYear.SelectedValue, out year);

                if (day > 0 && month > 0 && year > 0)
                {
                    if (IsValidAge())
                    {
                        birthdate = new DateTime(year, month, day);
                    }
                    else
                    {
                        //lblMessage.Text = "You have to be over the age of 13 in order to signup";
                        return;
                    }
                }
                else
                {
                    //lblMessage.Text = "Please specify your birthday";
                    return;
                }


                //Independent film maker checkbox removed task https://trello.com/c/yRymna8w/5-update-sign-up-page
                //All further registrations will have isFilmMaker = false
                //isFilmMaker = this.chkBxIamfilmmaker.Checked; //This line was commented on 02/08/2021 by Karen A.
                agreeWithTerms = this.chkTerms.Checked;
                if(!agreeWithTerms)
                {
                    lblMessage.Text = "Please agree with terms and condtions";
                    return;
                }
                try
                {
                    UserLocation loc = WebUtils.GetUserLocationFromCookie();


                    string userIp = getUserIP();                  

                    if (loc != null)
                    {
                        loc = new UserLocation();
                        loc.isSignUp = true;
                        userIp = loc.UserIp;
                    }

    
                    var confirmationEmailTemplateUri = new Uri(Server.MapPath("~/Emails/ConfirmationEmail.html"));
                    UserManage.InsertUser(userName, userIp, password, email, firstName, lastname, birthdate, country, gender, confirmationEmailTemplateUri, loc, isFilmMaker);

                    if (!String.IsNullOrEmpty(userName))
                    {
                        UserManage.UpdateUserLastKnownLocationByUsername(loc, userName);
                    }

                    Response.Redirect("thank-you", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Failed to insert new user with username '{0}'", userName), ex);
                }
            }        
        }

        public bool ValidateReCaptcha()
        {
            var response = Request.Form["g-Recaptcha-Response"];
            if (string.IsNullOrEmpty(response))
                return false;

            //Post to google to resolve captcha response
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>
            ("secret", ConfigurationManager.AppSettings["ReCaptchaSecretKey"]));
            values.Add(new KeyValuePair<string, string>
             ("response", response));
            FormUrlEncodedContent content  = new FormUrlEncodedContent(values);

            HttpResponseMessage gresponse = client.PostAsync("/recaptcha/api/siteverify", content).Result;
            string verificationResponse = gresponse.Content.ReadAsStringAsync().Result;
            var verificationResult = JsonConvert.DeserializeObject<ReCaptchaValidationResult>(verificationResponse);
            return verificationResult.Success;
        }

        public string getUserIP()
        {
            string clientIP = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ?
                              HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            return String.IsNullOrEmpty(clientIP) ? ":1" : clientIP;
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }

    public class ReCaptchaValidationResult
    {
        public bool Success { get; set; }
        public string HostName { get; set; }
        [JsonProperty("challenge_ts")]
        public string TimeStamp { get; set; }
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}