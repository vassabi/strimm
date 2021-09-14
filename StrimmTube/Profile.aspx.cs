using Strimm.Shared;
using StrimmBL;
using Strimm.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model.Projections;
using StrimmTube.Utils;
using Strimm.Model.Order;
namespace StrimmTube
{
    public partial class Profile : BasePage
    {
        public int userId { get; set; }
        UserPo user { get; set; }
        public string textState { get; set; }
        public string textCountry { get; set; }
        int year, month;
        List<Country> countryList { get; set; }
        List<State> stateList { get; set; }
        public string currUserPass { get; set; }
        public string email { get; set; }
        public bool isExternal { get; set; }

        public string subscribtionPlan { get; set; }

        public bool enablePrivateVideoMode { get; set; }

        public bool enableMatureContent { get; set; }
        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                TurnOffCrawling();
            }

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // get promt text for ccdCountry and State
            if (Session["userId"] != null)
            {
                userId = int.Parse(Session["userId"].ToString());
            }
            else
            {
                //uncheck when done
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                //   userId = 2;
                //   Session["userId"] = 2;
            }

            user = UserManage.GetUserPoByUserId(userId);

            var userNameInUrl = Page.RouteData.Values["UserName"] == null ? String.Empty : Page.RouteData.Values["UserName"].ToString();

            if (user == null || user.PublicUrl != userNameInUrl)
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            AddTitle(String.Format("{0} Personal Profile", user.UserName), false);
           
                
            email = user.Email;
            isExternal = user.IsExternalUser;

            //var passWithSalt = CryptoUtils.GetPasswordWithSalt(user.Email, user.Password);
           // var encryptedPass = CryptoUtils.Encrypt("someone");
            //var passWithoutSalt = CryptoUtils.HashPassword(user.Password);
            currUserPass = user.Password;
            countryList = ReferenceDataManage.GetCountries();
            if(user.CountryId!=0)
            {
                stateList = ReferenceDataManage.GetStatesByCountryId(user.CountryId);
            }
            
            // get user info he added on signUp
            if (!IsPostBack)
            {
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataSource = countryList;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("select country", "0"));

                if (stateList.Count != 0)
                {
                    stateHolder.Visible = true;
                    ddlSates.DataTextField = "Name";
                    ddlSates.DataValueField = "StateId";
                    ddlSates.DataSource = stateList;
                    ddlSates.DataBind();
                    ddlSates.Items.Insert(0, new ListItem("select state", "0"));
                    if(user.StateId!=0)
                    {
                        ddlSates.SelectedValue = user.StateId.ToString();
                    }

                }
                else
                {
                    stateHolder.Visible = false;
                }
                DateTime tnow = DateTime.Now;
                ArrayList ayear = new ArrayList();
                int i;
                for (i = 1924; i <= tnow.Year; i++)
                {
                    ayear.Add(i);
                }
                ArrayList amonth = new ArrayList();
                for (i = 1; i <= 12; i++)
                {
                    amonth.Add(i);
                }

                ddlYear.DataSource = ayear;
                ddlYear.DataBind();
                ddlMonth.DataSource = amonth;
                ddlMonth.DataBind();
                year = Int32.Parse(ddlYear.SelectedValue);
                month = Int32.Parse(ddlMonth.SelectedValue);
                ddlDay.DataSource = DateTimeUtils.BindDays(year, month);
                ddlDay.DataBind();
                if(user.BirthDate!=null)
                {
                   
                    ddlYear.SelectedValue = user.BirthDate.Year.ToString();
                    ddlMonth.SelectedValue = user.BirthDate.Month.ToString();                  
                    ddlDay.SelectedValue = user.BirthDate.Day.ToString();
                }
                if(user.CountryId!=0||user.Country!=null)
                {
                    ddlCountry.SelectedValue = user.CountryId.ToString();
                }
                else
                {
                    ddlCountry.SelectedValue = "0";
                }
               
                
               
                txtFirstName.Text = user.FirstName.Split(' ').First();
                txtlastName.Text = user.LastName;
                lblUserName.Text = user.UserName;
               
                
                if (!String.IsNullOrEmpty(user.Address))
                {
                    txtAddress.Text = user.Address;
                }
               
                if (!String.IsNullOrEmpty(user.Company))
                {
                    txtCompany.Text = user.Company;

                }

                //txtBirthday.Text = String.Format("{0:dd/MM/yyyy}", user.birthDate);
                if (user.Gender == "Female")
                {
                    gender.SelectedValue = "Female";
                }
                else
                {
                    gender.SelectedValue = "Male";
                }
               if(!String.IsNullOrEmpty(user.City))
               {
                   txtCity.Text = user.City;
               }
                if(!String.IsNullOrEmpty(user.ZipCode))
                {
                    txtZip.Text = user.ZipCode;
                }
                //if(user.MatureContentAllowed)
                //{
                //    allowMatureVideo.Checked=true;
                //}
                //if(user.PrivateVideoModeEnabled)
                //{
                //    allowPrivateViewMode.Checked = true;
                //}
               
               
              
                txtEmail.Text = user.Email;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //enableMatureContent = allowMatureVideo.Checked;
            //enablePrivateVideoMode = allowPrivateViewMode.Checked;
            bool isHasUserName = false;
            if (!String.IsNullOrEmpty(user.UserName))
            {
                isHasUserName = true;
            }
            else
            {
                isHasUserName = false;
            }
            
            if (!String.IsNullOrWhiteSpace(txtFirstName.Text) && (!String.IsNullOrEmpty(txtFirstName.Text)))
            {
                user.FirstName = txtFirstName.Text;
            }
            else
            {
              
                //lblMessage.Text = "Please add first name";
                return;
            }
            if (!String.IsNullOrWhiteSpace(txtlastName.Text) && (!String.IsNullOrEmpty(txtlastName.Text)))
            {
                user.LastName = txtlastName.Text;
            }
            else
            {
               
                //lblMessage.Text = "Please add last name";
                return;
            }
            if (ddlCountry.SelectedValue=="0")
            {
               
                //lblMessage.Text = "Please choose country";
                return;
            }
           
           
            if (txtCompany.Text != String.Empty)
            {
                user.Company = txtCompany.Text;
            }
           
            string day = ddlDay.SelectedValue;
            string month = ddlMonth.SelectedValue;
            string year = ddlYear.SelectedValue;
            if(day.Length==1)
            {
                day = "0" + ddlDay.SelectedValue;
            }
            if(month.Length==1)
            {
                month = "0" + ddlMonth.SelectedValue;
            }
            if(IsValidAge())
            {
                string dateFornat = "dd/MM/yyyy";
                string birthday = day + "/" + month + "/" + year;
                user.BirthDate = DateTime.ParseExact(birthday, dateFornat, CultureInfo.InvariantCulture);
            }
            else
            {
                return;
            }
                

          
            
            user.Gender = gender.SelectedValue;
            if (EmailUtils.isEmailValid(txtEmail.Text))
            {
                user.Email = txtEmail.Text;
            }
            else
            {
                
                //lblMessage.Text = "please enter valid email";
                return;
            }
          //  bool isZipmatched = false;
            
           
         if(!String.IsNullOrEmpty(txtAddress.Text))
         {
             user.Address = txtAddress.Text;
         }

         if (countryList != null && countryList.Count > 0 && !String.IsNullOrEmpty(ddlCountry.SelectedValue))
         {
             var country = countryList.FirstOrDefault(x => x.CountryId == Int32.Parse(ddlCountry.SelectedValue));
             if (country != null)
             {
                 user.Country = country.Name;
                 user.CountryId = country.CountryId;

                 user.StateId = 0;
                 user.StateOrProvince = String.Empty;
             }
             else
             {
                 user.Country = String.Empty;
                 user.UserId = 0;
                 user.StateOrProvince = String.Empty;
                 user.StateId = 0;
             }
         }

         if (ddlCountry.SelectedValue == "223")
         {
             if (ddlSates.SelectedValue != "0")
             {
                 user.StateOrProvince = ddlSates.SelectedItem.Text;
             }
             else
             {
                 //lblMessage.Text = "please enter state or province";
                 return;
             }
         }      

         if (!String.IsNullOrEmpty(txtZip.Text))
         {
             user.ZipCode = txtZip.Text;
         }
         else
         {
             //lblMessage.Text = "please enter zip or postal code";
         }
         if (!String.IsNullOrEmpty(txtCity.Text))
         {
             user.City = txtCity.Text;
         }
         else
         {
             //lblMessage.Text = "please enter city";
         }

         var clientTime = WebUtils.GetClientTimeFromCookie(Request.Cookies) ?? DateTime.Now;

         user.TermsAndConditionsAcceptanceDate = clientTime;
         user.MatureContentAllowed = enableMatureContent;
         user.PrivateVideoModeEnabled = enablePrivateVideoMode;

         UserManage.UpdateUserProfile(user);
         ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowConfirmationMessage()", "ShowConfirmationMessage()", true);
        }
       
        
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = Int32.Parse(ddlYear.SelectedValue);

            month = Int32.Parse(ddlMonth.SelectedValue);

            ddlDay.DataSource = DateTimeUtils.BindDays(year, month);
           ddlDay.DataBind();
           
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = Int32.Parse(ddlYear.SelectedValue);

            month = Int32.Parse(ddlMonth.SelectedValue);

            ddlDay.DataSource = DateTimeUtils.BindDays(year, month);
            ddlDay.DataBind(); 
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCountryId = Int32.Parse(ddlCountry.SelectedValue);
            stateList = ReferenceDataManage.GetStatesByCountryId(selectedCountryId);
            if (stateList.Count != 0)
            {
                stateHolder.Visible = true;
                ddlSates.DataTextField = "Name";
                ddlSates.DataValueField = "StateId";
                ddlSates.DataSource = stateList;
                ddlSates.DataBind();
                ddlSates.Items.Insert(0, new ListItem("select state", "0"));

            }
            else
            {
                stateHolder.Visible = false;
            }
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

      
    }
}
