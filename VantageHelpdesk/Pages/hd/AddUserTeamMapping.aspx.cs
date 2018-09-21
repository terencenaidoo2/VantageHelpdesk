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
    public partial class AddUserTeamMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            if (!Page.IsPostBack)
            {
                LoadUsers();
                LoadCompanies();
            }

        }

        private void LoadUsers()
        {
            c_helpdesk hd = new c_helpdesk();

            DataSet dsUsers = hd.GetAllUsers();
            ddlUser.DataSource = dsUsers.Tables[0];
            ddlUser.DataBind();

        }

        private void LoadCompanies()
        {
            c_helpdesk hd = new c_helpdesk();

            DataSet dsCompanies = hd.GetAllCompanies();
            ddlCompany.DataSource = dsCompanies.Tables[0];
            ddlCompany.DataBind();

            //Load Teams based on company id
            if (ddlCompany.SelectedIndex > -1)
            {
                int companyid = Convert.ToInt32(ddlCompany.SelectedItem.Value);
                LoadTeamsByCompanyID(companyid);
            }

        }

        private void LoadTeamsByCompanyID(int companyid)
        {
            c_helpdesk hd = new c_helpdesk();

            DataSet dsTeams = hd.GetTeamsByCompanyID(companyid);
            ddlTeam.DataSource = dsTeams.Tables[0];
            ddlTeam.DataBind();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load Teams based on company id
            if (ddlCompany.SelectedIndex > -1)
            {
                int companyid = Convert.ToInt32(ddlCompany.SelectedItem.Value);
                LoadTeamsByCompanyID(companyid);
            }
        }

        protected void btnSaveUserTeamMapping_Click(object sender, EventArgs e)
        {
            SaveUserTeamMapping();
            lblSuccess.Text = "Mapping saved successfully";
            lblSuccess.Visible = true;
        }

        private void SaveUserTeamMapping()
        {
            c_helpdesk hd = new c_helpdesk();
            if (ddlUser.SelectedIndex > -1 && ddlTeam.SelectedIndex > -1)
            {
                string user_id = ddlUser.SelectedItem.Value.ToString();
                int team_id = Convert.ToInt32(ddlTeam.SelectedItem.Value);
                hd.AddUserMapping(user_id, team_id);
            }
           
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}