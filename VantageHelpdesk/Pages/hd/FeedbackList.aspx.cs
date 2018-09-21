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

namespace VantageHelpdesk.Pages.hd
{
    public partial class FeedbackList : System.Web.UI.Page
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
                    if (!user_email.ToUpper().Contains("VANTAGEDATA") || user_email.ToUpper().Contains("DEMO"))
                    {
                        Session["team_id"] = dv.Table.Rows[0]["team_id"].ToString();
                    }
                    else
                    {
                        Session["team_id"] = null;
                    }

                    //Get user id of user who created the issue
                    ViewState["teamid"] = dv.Table.Rows[0]["team_id"].ToString();
                    ViewState["companyid"] = dv.Table.Rows[0]["company_id"].ToString();

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

            ListItem li1 = new ListItem("--Please Select", "");
            ListItem li2 = new ListItem("--Please Select", "");
            ListItem li3 = new ListItem("--Please Select", "");
            ListItem li4 = new ListItem("--Please Select", "");
            ddlSite.Items.Insert(0, li1);
            ddlStatus.Items.Insert(0, li2);
            ddlAccountManager.Items.Insert(0, li3);
            ddlCurrentOwner.Items.Insert(0, li4);

            gvFeedback.PageSize = 10;
        }

        protected void gvFeedback_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            int status_id = 0;
            int site_id = 0;
            string account_manager = "";
            string current_owner = "";
            string priority = "";
            c_helpdesk hd = new c_helpdesk();

            if (e.CommandName.Equals("updateRecord"))
            {
                //if (gvFeedback.PageIndex > 0)
                //{
                //    index = Convert.ToInt32(e.CommandArgument) - (gvFeedback.PageIndex * gvFeedback.PageSize);
                //}
                //else { index = Convert.ToInt32(e.CommandArgument); }

                int feedback_id = Convert.ToInt32(gvFeedback.DataKeys[Convert.ToInt32(e.CommandArgument)]["feedback_id"]);
                hidFeedbackID.Value = feedback_id.ToString();

                DataView dv = new DataView();
                dv = hd.ListFeedback(feedback_id);
                if (dv.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        lblFeedbackID.Text = Convert.ToInt32(dv.Table.Rows[i]["id"]).ToString("#000000");                        
                        txtDesc.Text = dv.Table.Rows[i]["description"].ToString();
                        status_id = Convert.ToInt32(dv.Table.Rows[i]["status_id"]);
                        site_id = Convert.ToInt32(dv.Table.Rows[i]["site_id"]);

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
                dv1 = hd.GetLatestFeedbackHistory(feedback_id);
                if (dv1.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv1.Table.Rows.Count; i++)
                    {
                        account_manager = dv1.Table.Rows[i]["assigned_to_account_manager"].ToString();
                        current_owner = dv1.Table.Rows[i]["assigned_to_current_owner"].ToString();
                    }
                }

                ddlAccountManager.SelectedValue = account_manager.ToString();
                ddlCurrentOwner.SelectedValue = current_owner.ToString();

                //gvFeedback.DataBind();

                //aw.Delete(ArtistPieceID, Session["FV_Username"].ToString());
                //LoadGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);

               
            }
            else if (e.CommandName.Equals("viewHistory"))
            {
                //if (gvFeedback.PageIndex > 0)
                //{
                //    index = Convert.ToInt32(e.CommandArgument) - (gvFeedback.PageIndex * gvFeedback.PageSize);
                //}
                //else { index = Convert.ToInt32(e.CommandArgument); }

                int feedback_id = Convert.ToInt32(gvFeedback.DataKeys[Convert.ToInt32(e.CommandArgument)]["feedback_id"]);
                hidFeedbackID.Value = feedback_id.ToString();

                DataView dv = new DataView();
                dv = hd.GetLatestFeedbackHistory(feedback_id);
                if (dv.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        lblHist_feedback_id.Text = Convert.ToInt32(dv.Table.Rows[i]["id"]).ToString("#000000");
                        lblHist_Desc.Text = dv.Table.Rows[i]["comments"].ToString();
                        lblHist_AccountManager.Text = dv.Table.Rows[i]["AccountManager"].ToString();
                        lblHist_CurrentOwner.Text = dv.Table.Rows[i]["CurrentOwner"].ToString();
                        lblHist_Status.Text = dv.Table.Rows[i]["Status_Desc"].ToString();
                        lblHist_Site.Text = dv.Table.Rows[i]["Site_Desc"].ToString();
                    }
                }

                gvFeedback.DataBind();
                gvFeedbackHistory.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupHistory();", true);
            }
        }

        protected void butUpdate_Click(object sender, EventArgs e)
        {
            c_helpdesk hd = new c_helpdesk();

            hd.AddFeedbackHistory(Convert.ToInt32(hidFeedbackID.Value), txtDesc.Text, Convert.ToInt32(ddlSite.SelectedValue), 
                ddlAccountManager.SelectedValue, ddlCurrentOwner.SelectedValue, txtComments.Text, Convert.ToInt32(ddlStatus.SelectedValue), User.Identity.GetUserId(), ddlPriority.SelectedItem.Text);

            txtComments.Text = "";

            gvFeedback.DataBind();
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            gvFeedback.DataBind();
        }

        protected void butReset_Click(object sender, EventArgs e)
        {
            txtSearchComments.Text = "";
            ddlSearchLoggedBy.SelectedValue = "";
            ddlSearchSite.SelectedValue = "";
            ddlSearchStatus.SelectedValue = "";
            ddlSearchCurrentOwner.SelectedValue = "";
            txtSearchFeedbackID.Text = "";
            gvFeedback.DataBind();
        }

        protected void gvFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFeedback.PageIndex = e.NewPageIndex;
        }

        protected void gvFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //hide the current owner column if its a client user
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            //{
            //    if (ViewState["vantageuser"] != null)
            //    {
            //        if (ViewState["vantageuser"].ToString() == "1")
            //        {
            //            e.Row.Cells[5].Visible = true;
            //        }
            //        else
            //        {
            //            e.Row.Cells[5].Visible = false;
            //        }
            //    }
            //}

            int comment_count = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                comment_count = Convert.ToInt32(gvFeedback.DataKeys[e.Row.RowIndex]["comment_count"]);

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
    }
}