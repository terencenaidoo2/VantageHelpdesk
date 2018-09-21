using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace VantageHelpdesk
{
    public partial class ssolegacy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/scripts/jquery-1.12.4.min.js",
                    DebugPath = "~/scripts/jquery-1.12.4.min.js",
                    CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.min.js",
                    CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.js"
                });

            var user_id = HttpUtility.UrlEncode(Request.QueryString["ui"]);
            if (!String.IsNullOrEmpty(user_id))
            {
                ValidateUser(user_id);
            }
            else
            {
                login_form.Style["display"] = "none";
                ErrorMessage.Text = "Your request is invalid. Please access this application from the Vantage Portal.";
                ErrorMessage.Visible = true;
            }
        }

        private void ValidateUser(string user_id)
        {
            bool user_exists = false;
            bool user_exists_sso = false;
            var application_id = HttpUtility.UrlEncode(Request.QueryString["ai"]);

            Vantage_App_Users_Sso.c_sso csso = new Vantage_App_Users_Sso.c_sso();

            if (!String.IsNullOrEmpty(application_id))
            {
                // check user exists in vantage_app_users
                user_exists = csso.checkuserexists(user_id, application_id);
                if (user_exists == true)
                {
                    // check user exists in sso
                    user_exists_sso = csso.checkuserexistssso(user_id, application_id);
                    if (user_exists_sso == true)
                    {
                        AutoLogin(user_id, application_id);
                    }
                    else
                    {
                        // ask user for password
                        login_form.Style["display"] = "";
                        ErrorMessage.Visible = false;
                    }
                }
                else
                {
                    // user does not exists, inform user to contact vantage portal administrator
                    login_form.Style["display"] = "none";
                    ErrorMessage.Text = "Your login credentials for this application are invalid. Please contact your Vantage Portal you t to assign you the correct access.";
                    ErrorMessage.Visible = true;
                }
            }
            else
            {
                login_form.Style["display"] = "none";
                ErrorMessage.Text = "Your request is invalid. Please access this application from the Vantage Portal.";
                ErrorMessage.Visible = true;
            }
        }

        private void AutoLogin(string user_id, string application_id)
        {
            var user_name = "";
            var user_pass = "";
            Vantage_App_Users_Sso.c_sso csso = new Vantage_App_Users_Sso.c_sso();
            System.Data.DataView dv = new System.Data.DataView();
            dv = csso.get(user_id, application_id);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["user_name"] != DBNull.Value)
                    {
                        user_name = dv.Table.Rows[i]["user_name"].ToString();
                    }
                    if (dv.Table.Rows[i]["user_pass"] != DBNull.Value)
                    {
                        user_pass = dv.Table.Rows[i]["user_pass"].ToString();
                    }
                }
            }

            if (user_name.Length > 0 && user_pass.Length > 0)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(user_name, user_pass, true, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        Session["username"] = user_name;
                        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        Response.Redirect("~/Default.aspx");
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("~/Account/Lockout.aspx");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn.aspx?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        false),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        // ask user for password
                        login_form.Style["display"] = "";
                        ErrorMessage.Text = "Single sign-on failed. Please enter and confirm your password.";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var user_id = HttpUtility.UrlEncode(Request.QueryString["ui"]);
            var application_id = HttpUtility.UrlEncode(Request.QueryString["ai"]);
            string user_name = "";

            Vantage_App_Users_Sso.c_sso csso = new Vantage_App_Users_Sso.c_sso();
            if (txtPassword.Text != "")
            {
                // get user name
                user_name = csso.getusername(user_id, application_id);

                // check logon with password
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                var result = signinManager.PasswordSignIn(user_name, txtPassword.Text, true, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        csso.passwordupdate(user_id, txtPassword.Text);

                        Response.Redirect("~/Default.aspx");
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("~/Account/Lockout.aspx");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn.aspx?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        false),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        ErrorMessage.Text = "The password you entered is invalid, please try again or use the Vantage Portal to change your password.";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}