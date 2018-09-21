using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VantageHelpdesk.DAL;

namespace VantageHelpdesk
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Only vantagedata users should have access to full list of users
            if (Request.IsAuthenticated)
            {
                //Only vantagedata users should have access to full list of users
                c_helpdesk hd = new c_helpdesk();
                string userid = HttpContext.Current.User.Identity.GetUserId();
                string user_email = hd.GetEmailByUserID(userid);
                //if (user_email.ToUpper().Contains("VANTAGEDATA"))
                //{
                //    Response.Redirect("~/Pages/hd/IssueList.aspx");
                //}
                //else
                //{
                //    //Redirect to Capture New Request Page
                //    Response.Redirect("~/Pages/hd/Main.aspx");
                //}

                Response.Redirect("~/Pages/hd/IssueList.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}