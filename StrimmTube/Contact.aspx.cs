using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Recaptcha.Web;
using System.Net.Mail;
using StrimmBL;
using Strimm.Shared;
using System.Net;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;


namespace StrimmTube
{
    public partial class Contact : BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }
        }

        protected async void btnSend_Click(object sender, EventArgs e)
        {
            int index=0;
            if (Session["contactIndex"] != null)
            {
                index = int.Parse(Session["contactIndex"].ToString());

            }

            var request = Request;
            var response = Request.Params["g-recaptcha-response"];
            var error = String.Empty;

            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                error = "Email could not be sent. Please add your name.";
            }
            else if (!EmailUtils.isEmailValid(txtEmail.Text))
            {
                error = "Email could not be sent. Please add valid email address.";
            }            else if (index == 0)
            {
                error = "Email could not be sent. Please select a subject";
            }
            else if (String.IsNullOrWhiteSpace(txtDescription.Text) || (String.IsNullOrEmpty(txtDescription.Text)))
            {
                error = "Email could not be sent. Please add a message.";
            }
            else if (String.IsNullOrEmpty(response))
            {
                error = "Email could not be sent. Captcha cannot be empty.";
            }

            if (String.IsNullOrEmpty(error))
            {
                string secret = ConfigurationManager.AppSettings["recaptchaPrivateKey"];

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                //when response is false check for the error message
                if (!captchaResponse.Success)
                {
                    var captchaError = captchaResponse.ErrorCodes[0].ToLower();
                    switch (captchaError)
                    {
                        case ("missing-input-secret"):
                            //lblMessage.Text = "The secret parameter is missing.";
                            break;
                        case ("invalid-input-secret"):
                            //lblMessage.Text = "The secret parameter is invalid or malformed.";
                            break;
                        case ("missing-input-response"):
                            //lblMessage.Text = "The response parameter is missing.";
                            break;
                        case ("invalid-input-response"):
                            //lblMessage.Text = "The response parameter is invalid or malformed.";
                            break;
                        default:
                            //lblMessage.Text = "Error occured. Please try again";
                            break;
                    }
                    error = "Captcha validation failed, please try again.";
                }
                else
                {
                    switch (index)
                    {
                        case 1:
                            SendEmailConfiramtion("marketing@strimm.com", "feedback/tip");
                            break;
                        case 2:
                            SendEmailConfiramtion("support@strimm.com", "support");
                            break;
                        case 3:
                            SendEmailConfiramtion("itservice@strimm.com", "technical issues");
                            break;
                        case 4:
                            SendEmailConfiramtion("advertisement@strimm.com", "advertising");
                            break;
                        case 5:
                            SendEmailConfiramtion("investments@strimm.com", "investments");
                            break;
                        case 6:
                            SendEmailConfiramtion("marketing@strimm.com", "marketing");
                            break;
                        case 7:
                            SendEmailConfiramtion("report-abuse@strimm.com", "copyright");
                            break;
                        case 8:
                            SendEmailConfiramtion("strimminfo@strimm.com", "message from user");
                            break;
                    }

                    txtDescription.Text = String.Empty;
                    txtEmail.Text = String.Empty;
                    txtName.Text = String.Empty;
                }
            }

            if (!String.IsNullOrEmpty(error))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowErrorMessage('" + error + "')", "ShowErrorMessage('" + error + "')", true);
            }
       }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["contactIndex"] = ddlSubject.SelectedIndex;
        }
        public  void  SendEmailConfiramtion(string email, string strSubject)
        {
            bool isSent = false;
            try
            {
                Uri templateUri = new Uri(Server.MapPath("~/Emails/Contact.html"));
                //try catch (if server down threw exception)
                string subject = strSubject;

                string body = String.Empty;

                using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/Feedback.html"))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{subject}", strSubject);

                body = body.Replace("{name}", txtName.Text);

               

                body = body.Replace("{email}", txtEmail.Text);
                body = body.Replace("{message}", txtDescription.Text);



                //isSent = EmailUtils.SendEmail(subject, body, email, txtEmail.Text);


                isSent = EmailUtils.SendEmail(strSubject, body, email, txtEmail.Text);
               
            }
            catch
            {
                isSent = false;
            }
            if (isSent)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowConfirmationMessage('Message was successfully sent!')", "ShowConfirmationMessage('Message was successfully sent!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowErrorMessage('Failed to send a message. Please try again!')", "ShowErrorMessage('Failed to send a message. Please try again!')", true);
            }
        }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}