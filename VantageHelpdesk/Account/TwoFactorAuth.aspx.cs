using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VantageHelpdesk.Models;
using VantageHelpdesk.DAL;

namespace VantageHelpdesk.Account
{
    public partial class TwoFactorAuth : System.Web.UI.Page
    {
        private ApplicationSignInManager signinManager;
        private ApplicationUserManager manager;

        public TwoFactorAuth()
        {
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                Response.Redirect("/Account/Error", true);
            }
            else
            {
                bool is_valid = false;
                string application_id = "A590FA58-A885-4F32-81D5-C829D90F3C82";
                c_auth auth = new c_auth();

                is_valid = auth.ValidateUser(userId, application_id);

                if (is_valid == true)
                { 
                    //Only vantagedata users should have access to full list of users
                    //c_helpdesk hd = new c_helpdesk();
                    //string userid = HttpContext.Current.User.Identity.GetUserId();
                    //string user_email = hd.GetEmailByUserID(userid);
                    //if (user_email.ToUpper().Contains("VANTAGEDATA"))
                    //{
                    //    IdentityHelper.RedirectToReturnUrl("~/Pages/hd/IssueList.aspx", Response);
                    //}
                    //else
                    //{
                    //    //Redirect to Capture New Request Page
                    //    IdentityHelper.RedirectToReturnUrl("~/Pages/hd/Main.aspx", Response);
                    //}
                    IdentityHelper.RedirectToReturnUrl("~/Pages/hd/IssueList.aspx", Response);
                }
                else
                {
                    Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    Response.Redirect("/Account/Error", true);
                }
            }
        }
    }
}