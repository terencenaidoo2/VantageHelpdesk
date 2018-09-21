using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VantageHelpdesk.DAL;

namespace VantageHelpdesk.Pages.hd
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        LoadUserMappings();
                    }
                }

            }
            catch (Exception ex)
            {
                //LogError(ex);

            }
        }

        private void LoadUserMappings()
        {
            try
            {
                c_helpdesk hd = new c_helpdesk();
                DataView dvUserMappings = hd.GetUserMapping();
                gvUserMapping.DataSource = dvUserMappings;
                gvUserMapping.DataBind();
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void gvUserMapping_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvUserMapping.EditIndex = e.NewEditIndex;
                LoadUserMappings();
            }
            catch (Exception ex)
            {

               
            }
        }

        protected void gvUserMapping_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvUserMapping.EditIndex = -1;
                LoadUserMappings();
            }
            catch (Exception ex)
            {

               
            }
        }

        protected void btnAddNewMapping_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUserTeamMapping.aspx");
        }
    }
}