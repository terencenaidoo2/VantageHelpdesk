using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VantageHelpdesk.DAL;

namespace VantageHelpdesk.Pages.hd
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            c_helpdesk hd = new c_helpdesk();
            string userId = User.Identity.GetUserId();

            string user_email = hd.GetEmailByUserID(userId);
            //Get user details
            DataView dv = new DataView();
            dv = hd.GetUserDetails(userId);
            if (dv.Table.Rows.Count > 0)
            {
                if (!user_email.ToUpper().Contains("DEMO"))
                {
                    DataView dvSiteList = hd.SiteList(userId);
                    ddlSite.DataSource = dvSiteList;
                    ddlSite.DataBind();
                }
            }
         
        }

        public bool SendMailHelpdesk(string user_id, int hd_id, string hd_type, string hd_comment,  int hd_site_id, string email_to, string site_name, string user_name)
        {
            try
            {
                string MailFrom = "";
                string MailSMTPServer = "";
                string MailSMTPUserName = "";
                string MailSMTPPassword = "";
                string MailSMTPPort = "";
                string MailSMTPEnableSSL = "";

                c_helpdesk hd = new c_helpdesk();

                DataSet dsSiteSettings = hd.GetMailSettings();
                if (dsSiteSettings.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in dsSiteSettings.Tables[0].Rows)
                    {
                        if (dr["setting_key"].ToString() == "MailFrom")
                        {
                            MailFrom = dr["setting_value"].ToString();
                        }
                        else if (dr["setting_key"].ToString() == "MailSMTPServer")
                        {
                            MailSMTPServer = dr["setting_value"].ToString();
                        }
                        else if (dr["setting_key"].ToString() == "MailSMTPUserName")
                        {
                            MailSMTPUserName = dr["setting_value"].ToString();
                        }
                        else if (dr["setting_key"].ToString() == "MailSMTPPassword")
                        {
                            MailSMTPPassword = dr["setting_value"].ToString();
                        }
                        else if (dr["setting_key"].ToString() == "MailSMTPPort")
                        {
                            MailSMTPPort = dr["setting_value"].ToString();
                        }
                        else if (dr["setting_key"].ToString() == "MailSMTPEnableSSL")
                        {
                            MailSMTPEnableSSL = dr["setting_value"].ToString();
                        }
                    }
                }


                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                int iMailSMTPPort = 25;
                int.TryParse(MailSMTPPort, out iMailSMTPPort);

                if (email_to != null)
                {
                    var message = new MailMessage();
                    message.From = new MailAddress(MailFrom);
                    message.To.Add(email_to);


                    if (user_name == null)
                    {
                        user_name = "-";
                    }
                    if (site_name == null)
                    {
                        site_name = "-";
                    }

                    if (hd_type == "feedback")
                    {
                        message.Subject = "New: Vantage Helpdesk Feedback - #" + hd_id.ToString("#00000");
                        sb.AppendLine("<p><b>Feedback #" + hd_id.ToString("#00000") + "</b><br>Please see details below:</p><hr>");
                        sb.AppendLine("<p>");
                        sb.AppendLine("<table width='100%'>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>User:</td><td style='width:75%'>" + user_name + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Email Address:</td><td style='width:75%'>" + email_to + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Site:</td><td style='width:75%'>" + site_name + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Feedback:</td><td style='width:75%'>" + hd_comment + "</td></tr>");
                        sb.AppendLine("</table>");
                        sb.AppendLine("</p><hr>");
                        sb.AppendLine("<p>");
                        sb.AppendLine("Sincerely<br>Your Vantage Data Team");
                        sb.AppendLine("</p>");
                    }
                    else if (hd_type == "issue")
                    {
                        message.Subject = "New: Vantage Helpdesk Issue - #" + hd_id.ToString("#00000");
                        sb.AppendLine("<p><b>Issue #" + hd_id.ToString("#00000") + "</b><br>Please see details below:</p><hr>");
                        sb.AppendLine("<p>");
                        sb.AppendLine("<table width='100%'>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>User:</td><td style='width:75%'>" + user_name + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Email Address:</td><td style='width:75%'>" + email_to + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Site:</td><td style='width:75%'>" + site_name + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Status:</td><td style='width:75%'>Opened</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Issue:</td><td style='width:75%'>" + hd_comment + "</td></tr>");
                        sb.AppendLine("</table>");
                        sb.AppendLine("</p><hr>");
                        sb.AppendLine("<p>");
                        sb.AppendLine("Sincerely<br>Your Vantage Data Team");
                        sb.AppendLine("</p>");
                    }

                    message.Body = sb.ToString();
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = MailSMTPUserName,
                            Password = MailSMTPPassword
                        };
                        smtp.Credentials = credential;
                        smtp.Host = MailSMTPServer;
                        smtp.Port = iMailSMTPPort;
                        smtp.Timeout = 15000;

                        if (MailSMTPEnableSSL == "true") { smtp.EnableSsl = true; } else { smtp.EnableSsl = false; }

                        smtp.Send(message);

                        // send a copy to helpdesk
                        var message_helpdesk = new MailMessage();
                        message_helpdesk.To.Add(MailFrom);
                        message_helpdesk.From = new MailAddress(MailFrom);
                        message_helpdesk.Body = sb.ToString();
                        message_helpdesk.IsBodyHtml = true;
                        if (hd_type == "feedback")
                        {
                            message_helpdesk.Subject = "New: Vantage Helpdesk Feedback - #" + hd_id.ToString("#00000");
                        }
                        else if (hd_type == "issue")
                        {
                            message_helpdesk.Subject = "New: Vantage Helpdesk Issue - #" + hd_id.ToString("#00000");
                        }
                        smtp.Send(message_helpdesk);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void SendHelpdeskRequest()
        {
            try
            {
                c_helpdesk hd = new c_helpdesk();

                string user_id = User.Identity.GetUserId();
                string user_name = User.Identity.Name;

                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = manager.FindById(user_id);
                string email_to = user.Email;

                string hd_type = "";
                int hd_id = 0;
                string hd_comment = txtComments.Text.Trim();
                int hd_site_id = Convert.ToInt32(ddlSite.SelectedItem.Value);
                if (ddlFeedbackIssue.SelectedItem.Value == "feedback")
                {
                    hd_type = ddlFeedbackIssue.SelectedItem.Value;
                    hd_id = hd.GetNextFeedbackID();
                    hd.AddFeedback(hd_comment, user_id, user_name, hd_site_id);
                }
                else if (ddlFeedbackIssue.SelectedItem.Value == "issue")
                {
                    hd_type = ddlFeedbackIssue.SelectedItem.Value;
                    hd_id = hd.GetNextIssueID();
                    hd.AddIsse(hd_comment, 1,user_id, user_name, hd_site_id);
                }

              

                SendMailHelpdesk(user_id, hd_id, hd_type, hd_comment, hd_site_id, email_to, ddlSite.SelectedItem.Text, user_name);

                Response.Redirect("Thankyou.aspx");
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }
   

        protected void btnSaveHelpdeskDetails_Click(object sender, EventArgs e)
        {
            try
            {
                SendHelpdeskRequest();
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}