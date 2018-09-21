using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VantageHelpdesk.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace VantageHelpdesk.Pages.hd
{
    public partial class IssueList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                c_helpdesk hd = new c_helpdesk();
                string userid = HttpContext.Current.User.Identity.GetUserId();
                string user_email = hd.GetEmailByUserID(userid);
                //Get user details
                DataView dv = new DataView();
                dv = hd.GetUserDetails(userid);
                if (dv.Table.Rows.Count > 0)
                {
                    string team_ids = "";
                    foreach (DataRow row in dv.Table.Rows)
                    {
                        if (!user_email.ToUpper().Contains("VANTAGEDATA") || user_email.ToUpper().Contains("DEMO"))
                        {
                            Session["team_id"] = row["team_id"].ToString();
                            team_ids = team_ids + row["team_id"].ToString() + ",";
                        }
                        else
                        {
                            Session["team_id"] = null;
                        }

                        //Get user id of user who created the issue
                        ViewState["teamid"] = dv.Table.Rows[0]["team_id"].ToString();
                        ViewState["companyid"] = dv.Table.Rows[0]["company_id"].ToString();
                    }
                  

                }

                if (user_email.ToUpper().Contains("VANTAGEDATA") && !user_email.ToUpper().Contains("DEMO"))
                {
                    ViewState["vantageuser"] = "1";
                }
                else
                {
                    ViewState["vantageuser"] = "0";
                }
                LoadInit();
            }
        }

        private void LoadInit()
        {
            ddlSearchLoggedBy.DataBind();
            ddlSearchSite.DataBind();
            ddlSearchStatus.DataBind();
            ddlSearchCurrentOwner.DataBind();

            ListItem lis1 = new ListItem("--Please Select", "");
            ListItem lis2 = new ListItem("--Please Select", "");
            ListItem lis3 = new ListItem("--Please Select", "");
            ListItem lis4 = new ListItem("--Please Select", "");
            ddlSearchLoggedBy.Items.Insert(0, lis1);
            ddlSearchSite.Items.Insert(0, lis2);
            ddlSearchStatus.Items.Insert(0, lis3);
            ddlSearchCurrentOwner.Items.Insert(0, lis4);

            ddlSite.DataBind();
            ddlStatus.DataBind();
            ddlAccountManager.DataBind();
            ddlCurrentOwner.DataBind();
            ddlCompany.DataBind();
            ddlTeam.DataBind();

            ListItem li1 = new ListItem("--Please Select", "");
            ListItem li2 = new ListItem("--Please Select", "");
            ListItem li3 = new ListItem("--Please Select", "");
            ListItem li4 = new ListItem("--Please Select", "");
            ListItem li5 = new ListItem("--Please Select", "");
            ListItem li6 = new ListItem("--Please Select", "");
            ddlSite.Items.Insert(0, li1);
            ddlStatus.Items.Insert(0, li2);
            ddlAccountManager.Items.Insert(0, li3);
            ddlCurrentOwner.Items.Insert(0, li4);
            ddlCompany.Items.Insert(0, li5);
            ddlTeam.Items.Insert(0, li6);

            gvIssues.PageSize = 10;
        }

        protected void gvIssues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            int status_id = 0;
            int site_id = 0;
            int company_id = 0;
            string account_manager = "";
            string current_owner = "";
            string priority = "";
            c_helpdesk hd = new c_helpdesk();

            if (e.CommandName.Equals("updateRecord"))
            {
                //if (gvIssues.PageIndex > 0)
                //{
                //    index = Convert.ToInt32(e.CommandArgument) - (gvIssues.PageIndex * gvIssues.PageSize);
                //}
                //else { index = Convert.ToInt32(e.CommandArgument); }

                int issue_id = Convert.ToInt32(gvIssues.DataKeys[Convert.ToInt32(e.CommandArgument)]["issue_id"]);
                hidIssueID.Value = issue_id.ToString();

                DataView dv = new DataView();
                dv = hd.ListIssue(issue_id);
                if (dv.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        //Get user id of user who created the issue
                        ViewState["userid"] = dv.Table.Rows[i]["user_id"].ToString();

                        lblIssueID.Text = Convert.ToInt32(dv.Table.Rows[i]["id"]).ToString("#000000");
                        txtDesc.Text = dv.Table.Rows[i]["description"].ToString();
                        // txtCompany.Text = dv.Table.Rows[i]["company"].ToString();
                        status_id = Convert.ToInt32(dv.Table.Rows[i]["status_id"]);
                        site_id = Convert.ToInt32(dv.Table.Rows[i]["site_id"]);
                        if (dv.Table.Rows[i]["company_id"] != DBNull.Value)
                        {
                            company_id = Convert.ToInt32(dv.Table.Rows[i]["company_id"]);
                            ddlCompany.SelectedValue = company_id.ToString();
                        }
                        else
                        {
                            ddlCompany.SelectedIndex = 0;
                        }


                        ddlStatus.SelectedValue = status_id.ToString();
                        ddlSite.SelectedValue = site_id.ToString();
                        if (dv.Table.Rows[i]["priority"] != DBNull.Value)
                        {
                            ddlPriority.SelectedValue = dv.Table.Rows[i]["priority"].ToString();
                        }
                        else
                        {
                            ddlPriority.SelectedValue = "Normal";
                        }


                    }
                }

                // Get latest entries
                DataView dv1 = new DataView();
                dv1 = hd.GetLatestIssueHistory(issue_id);
                if (dv1.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv1.Table.Rows.Count; i++)
                    {
                        account_manager = dv1.Table.Rows[i]["assigned_to_account_manager"].ToString();
                        current_owner = dv1.Table.Rows[i]["assigned_to_current_owner"].ToString();
                    }
                }

                try
                {
                    ddlAccountManager.SelectedValue = account_manager.ToString();

                    ddlCurrentOwner.SelectedValue = current_owner.ToString();

                    ViewState["currentowner"] = current_owner.ToString();
                }
                catch (Exception ex)
                {

                }




                //gvIssues.DataBind();

                //aw.Delete(ArtistPieceID, Session["FV_Username"].ToString());
                //LoadGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);


            }
            else if (e.CommandName.Equals("viewHistory"))
            {
                //if (gvIssues.PageIndex > 0)
                //{
                //    index = Convert.ToInt32(e.CommandArgument) - (gvIssues.PageIndex * gvIssues.PageSize);
                //}
                //else { index = Convert.ToInt32(e.CommandArgument); }

                int issue_id = Convert.ToInt32(gvIssues.DataKeys[Convert.ToInt32(e.CommandArgument)]["issue_id"]);
                hidIssueID.Value = issue_id.ToString();

                DataView dv = new DataView();
                dv = hd.GetLatestIssueHistory(issue_id);
                if (dv.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        lblHist_issue_id.Text = Convert.ToInt32(dv.Table.Rows[i]["id"]).ToString("#000000");
                        lblHist_Desc.Text = dv.Table.Rows[i]["comments"].ToString();
                        lblHist_Company.Text = dv.Table.Rows[i]["company"].ToString();
                        lblHist_AccountManager.Text = dv.Table.Rows[i]["AccountManager"].ToString();
                        lblHist_CurrentOwner.Text = dv.Table.Rows[i]["CurrentOwner"].ToString();
                        lblHist_Status.Text = dv.Table.Rows[i]["Status_Desc"].ToString();
                        lblHist_Site.Text = dv.Table.Rows[i]["Site_Desc"].ToString();
                    }
                }

                gvIssues.DataBind();
                gvIssueHistory.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupHistory();", true);
            }
        }

        protected void butUpdate_Click(object sender, EventArgs e)
        {
            c_helpdesk hd = new c_helpdesk();

            hd.AddIssueHistory(Convert.ToInt32(hidIssueID.Value), txtDesc.Text, Convert.ToInt32(ddlSite.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue),
                ddlAccountManager.SelectedValue, ddlCurrentOwner.SelectedValue, txtComments.Text, Convert.ToInt32(ddlStatus.SelectedValue), User.Identity.GetUserId(), ddlPriority.SelectedItem.Text);

            //Get Account Manager's email
            string account_manager = "";
            if (ddlAccountManager.SelectedIndex > 0)
            {
                account_manager = ddlAccountManager.SelectedValue;
            }

            string account_manager_email = "";
            if (account_manager != "")
            {
                account_manager_email = hd.GetEmailByUserID(account_manager);
            }


            //Send email to assigned user
            //Only send an email to assigned user if the user has changed. We track this through Viewstate
            string current_owner = ViewState["currentowner"].ToString();
            if (ddlCurrentOwner.SelectedValue != current_owner)
            {
                string new_owner = ddlCurrentOwner.SelectedValue;
                //get email by userid
                string owner_email = hd.GetEmailByUserID(new_owner);
                string status = ddlStatus.SelectedItem.Text;
                if (owner_email != "")
                {

                    //Send email
                    SendMailHelpdesk("", Convert.ToInt32(hidIssueID.Value), "issue", txtComments.Text, 0, owner_email, "Vantage Helpdesk", ddlCurrentOwner.SelectedItem.Text, status, account_manager_email);
                }

            }
            else if (ddlStatus.SelectedItem.Text.ToUpper() == "COMPLETED")
            {
                //Get email of user who created the issue
                string userid = ViewState["userid"].ToString();
                string user_email = hd.GetEmailByUserID(userid);
                //Send Completion Email 
                if (user_email != "")
                {
                    SendMailHelpdesk("", Convert.ToInt32(hidIssueID.Value), "issue", txtComments.Text, 0, user_email, "Vantage Helpdesk", ddlCurrentOwner.SelectedItem.Text, "COMPLETED", account_manager_email);
                }

            }


            txtComments.Text = "";

            gvIssues.DataBind();
        }

        public bool SendMailHelpdesk(string user_id, int hd_id, string hd_type, string hd_comment, int hd_site_id, string email_to, string site_name, string user_name, string status, string account_manager_email)
        {
            try
            {
                c_helpdesk hd = new c_helpdesk();
                string MailFrom = "";
                string MailSMTPServer = "";
                string MailSMTPUserName = "";
                string MailSMTPPassword = "";
                string MailSMTPPort = "";
                string MailSMTPEnableSSL = "";

                DataSet dsSiteSettings = hd.GetMailSettings();
                if (dsSiteSettings.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsSiteSettings.Tables[0].Rows)
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

                int iMailSMTPPort = 587;
                int.TryParse(MailSMTPPort, out iMailSMTPPort);

                if (email_to != null)
                {
                    var message = new MailMessage();
                    message.From = new MailAddress(MailFrom);
                    message.To.Add(email_to);
                    if (account_manager_email != "")
                    {
                        message.CC.Add(account_manager_email);
                    }

                    if (user_name == null)
                    {
                        user_name = "-";
                    }
                    if (site_name == null)
                    {
                        site_name = "-";
                    }
                    if (hd_type == "issue")
                    {
                        if (status.ToUpper() == "COMPLETED")
                        {
                            message.Subject = "Completed: Vantage Helpdesk Issue - #" + hd_id.ToString("#00000");
                        }
                        else
                        {
                            message.Subject = "Assigned: Vantage Helpdesk Issue - #" + hd_id.ToString("#00000");
                        }

                        sb.AppendLine("<p><b>Issue #" + hd_id.ToString("#00000") + "</b><br>Please see details below:</p><hr>");
                        sb.AppendLine("<p>");
                        sb.AppendLine("<table width='100%'>");
                        if (status.ToUpper() == "COMPLETED")
                        {
                            sb.AppendLine("<tr><th align='left' style='width:25%'>Issue Completed By User:</td><td style='width:75%'>" + user_name + "</td></tr>");
                        }
                        else
                        {
                            sb.AppendLine("<tr><th align='left' style='width:25%'>Issue Assigned to User:</td><td style='width:75%'>" + user_name + "</td></tr>");
                        }

                        sb.AppendLine("<tr><th align='left' style='width:25%'>Email Address:</td><td style='width:75%'>" + email_to + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Site:</td><td style='width:75%'>" + site_name + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Status:</td><td style='width:75%'>" + status + "</td></tr>");
                        sb.AppendLine("<tr><th align='left' style='width:25%'>Comment:</td><td style='width:75%'>" + hd_comment + "</td></tr>");
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
                            if (status.ToUpper() == "COMPLETED")
                            {
                                message_helpdesk.Subject = "Completed: Vantage Helpdesk Feedback - #" + hd_id.ToString("#00000");
                            }
                            else
                            {
                                message_helpdesk.Subject = "Assigned: Vantage Helpdesk Feedback - #" + hd_id.ToString("#00000");
                            }

                        }
                        else if (hd_type == "issue")
                        {
                            if (status.ToUpper() == "COMPLETED")
                            {
                                message_helpdesk.Subject = "Completed: Vantage Helpdesk Issue - #" + hd_id.ToString("#00000");
                            }
                            else
                            {
                                message_helpdesk.Subject = "Assigned: Vantage Helpdesk Issue - #" + hd_id.ToString("#00000");
                            }

                        }
                        //Uncomment when going live
                        //---------------------------
                        //smtp.Send(message_helpdesk);
                        //----------------------------
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            gvIssues.DataBind();
        }

        protected void butReset_Click(object sender, EventArgs e)
        {
            txtSearchComments.Text = "";
            ddlSearchLoggedBy.SelectedValue = "";
            ddlSearchSite.SelectedValue = "";
            ddlSearchStatus.SelectedValue = "";
            ddlSearchCurrentOwner.SelectedValue = "";
            txtSearchIssueID.Text = "";
            gvIssues.DataBind();
        }

        protected void gvIssues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIssues.PageIndex = e.NewPageIndex;
        }

        protected void gvIssues_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //hide the current owner column if its a client user
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["vantageuser"] != null)
                {
                    if (ViewState["vantageuser"].ToString() == "1")
                    {
                        e.Row.Cells[5].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[5].Visible = false;
                    }
                }
            }


            int comment_count = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                comment_count = Convert.ToInt32(gvIssues.DataKeys[e.Row.RowIndex]["comment_count"]);

                Label lblCommentCount = (Label)e.Row.FindControl("lblCommentCount");
                if (lblCommentCount != null)
                {
                    lblCommentCount.Text = comment_count.ToString();
                    if (comment_count == 0)
                    {
                        lblCommentCount.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblCommentCount.ForeColor = System.Drawing.Color.Black;
                    }
                }



                Label lblStatusDesc = (Label)e.Row.FindControl("lblStatusDesc");
                if (lblStatusDesc != null)
                {
                    if (lblStatusDesc.Text.Trim().ToUpper() == "DELAYED")
                    {
                        lblStatusDesc.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (lblStatusDesc.Text.Trim().ToUpper() == "COMPLETED")
                    {
                        lblStatusDesc.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (lblStatusDesc.Text.Trim().ToUpper() == "CANCELLED")
                    {
                        lblStatusDesc.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblStatusDesc.ForeColor = System.Drawing.Color.Black;
                    }
                }

                ImageButton imgbutSave = (ImageButton)e.Row.FindControl("imgbutSave");
                if (imgbutSave != null)
                {
                    if (ViewState["vantageuser"] != null)
                    {
                        if (ViewState["vantageuser"].ToString() == "1")
                        {
                            imgbutSave.Visible = true;
                        }
                        else
                        {
                            imgbutSave.Visible = false;
                        }
                    }
                }

                ImageButton imgbutHistory = (ImageButton)e.Row.FindControl("imgbutHistory");
                if (imgbutHistory != null)
                {
                    if (ViewState["vantageuser"] != null)
                    {
                        if (ViewState["vantageuser"].ToString() == "1")
                        {
                            imgbutHistory.Visible = true;
                        }
                        else
                        {
                            imgbutHistory.Visible = false;
                        }
                    }
                }
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadTeams
        }
    }
}