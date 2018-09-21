using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using VantageHelpdesk.DAL;

namespace VantageHelpdesk.DAL
{
    public class c_helpdesk
    {
        public DataView ListIssue(int id)
        {
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@id", id, DbType.Int32);
            dv = db.ExecuteDataView("spe_t_issue_list", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public DataView ListFeedback(int id)
        {
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@id", id, DbType.Int32);
            dv = db.ExecuteDataView("spe_t_feedback_list", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public DataView GetLatestFeedbackHistory(int feedback_id)
        {
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@feedback_id", feedback_id, DbType.Int32);
            dv = db.ExecuteDataView("spe_t_feedback_history_get_latest", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public DataView GetLatestIssueHistory(int issue_id)
        {
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@issue_id", issue_id, DbType.Int32);
            dv = db.ExecuteDataView("spe_t_issue_history_get_latest", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public void AddIssueHistory(int issue_id, string description, int site_id, int company_id,
            string assigned_to_account_manager, string assigned_to_current_owner, string comments, int status_id, string last_modified_by, string priority)
        {
            int i = 0;
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@issue_id", issue_id, DbType.Int32);
            db.AddParameter("@description", description, DbType.String);
            db.AddParameter("@site_id", site_id, DbType.Int32);
            db.AddParameter("@company_id", company_id, DbType.Int32);
            db.AddParameter("@assigned_to_account_manager", assigned_to_account_manager, DbType.String);
            db.AddParameter("@assigned_to_current_owner", assigned_to_current_owner, DbType.String);
            db.AddParameter("@comments", comments, DbType.String);
            db.AddParameter("@status_id", status_id, DbType.Int32);
            db.AddParameter("@priority", priority, DbType.String);
            db.AddParameter("@last_modified_by", last_modified_by, DbType.String); 
            i = db.ExecuteNonQuery("spe_t_issue_history_add", System.Data.CommandType.StoredProcedure);
        }

        public void AddFeedbackHistory(int feedback_id, string description, int site_id, 
            string assigned_to_account_manager, string assigned_to_current_owner, string comments, int status_id, string last_modified_by, string priority)
        {
            int i = 0;
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@feedback_id", feedback_id, DbType.Int32);
            db.AddParameter("@description", description, DbType.String);
            db.AddParameter("@site_id", site_id, DbType.Int32);
            db.AddParameter("@assigned_to_account_manager", assigned_to_account_manager, DbType.String);
            db.AddParameter("@assigned_to_current_owner", assigned_to_current_owner, DbType.String);
            db.AddParameter("@comments", comments, DbType.String);
            db.AddParameter("@status_id", status_id, DbType.Int32);
            db.AddParameter("@priority", priority, DbType.String);
            db.AddParameter("@last_modified_by", last_modified_by, DbType.String);
            i = db.ExecuteNonQuery("spe_t_feedback_history_add", System.Data.CommandType.StoredProcedure);
            
        }

        public void AddFeedback(string description, string user_id, string user_name, int site_id)
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@description", description, DbType.String);
            db.AddParameter("@user_id", user_id, DbType.String);
            db.AddParameter("@user_name", user_name, DbType.String);
            db.AddParameter("@site_id", site_id, DbType.Int32);
            db.ExecuteNonQuery("[app].[spe_feedback_add]", System.Data.CommandType.StoredProcedure);

        }

        public void AddIsse(string description, int status_id, string user_id, string user_name, int site_id)
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@description", description, DbType.String);
            db.AddParameter("@status_id", status_id, DbType.Int32);
            db.AddParameter("@user_id", user_id, DbType.String);
            db.AddParameter("@user_name", user_name, DbType.String);
            db.AddParameter("@site_id", site_id, DbType.Int32);
            db.ExecuteNonQuery("[app].[spe_issue_add]", System.Data.CommandType.StoredProcedure);

        }

        public int GetNextFeedbackID()
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);

            object feedbackid = db.ExecuteScalar("Select max(id) + 1 From t_feedback", System.Data.CommandType.Text);

            if (feedbackid != null)
            {
                return Convert.ToInt32(feedbackid);
            }
            else
            {
                return 1;
            }

        }

        public int GetNextIssueID()
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);

            object issueid = db.ExecuteScalar("Select max(id) + 1 From t_issue", System.Data.CommandType.Text);

            if (issueid != null)
            {
                return Convert.ToInt32(issueid);
            }
            else
            {
                return 1;
            }

        }


        public DataSet GetMailSettings()
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.vPortalConnStr);
            return db.ExecuteDataSet("select * from [VantagePortal].[dbo].[site_settings] where site_id = 1003 and setting_category = 'MAIL'", System.Data.CommandType.Text);
        }

        public string GetEmailByUserID(string userId)
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.vAppUsersConnStr);
            object email = db.ExecuteScalar("Select Email From [Vantage_App_Users].[dbo].[AspNetUsers] Where [Id] = '" + userId + "'", System.Data.CommandType.Text);
            if (email != null)
            {
                return email.ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        public DataSet GetAllUsers()
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.vAppUsersConnStr);
            return db.ExecuteDataSet("Select Id, UserName From [Vantage_App_Users].[dbo].[AspNetUsers]", System.Data.CommandType.Text);
        }

        public DataSet GetAllCompanies()
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            return db.ExecuteDataSet("Select id, description From [VantageHelpdesk].[dbo].[t_company]", System.Data.CommandType.Text);
        }

        public DataSet GetTeamsByCompanyID(int company_id)
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            return db.ExecuteDataSet("Select T.id, T.description From t_team_company TC Inner Join t_team T On TC.team_id = T.id Where TC.company_id = " + company_id.ToString(), System.Data.CommandType.Text);
        }

        public DataView SiteList(string userId)
        {
            DataView dv = new DataView();
            //Need to change when going live
            //---------------------------------
            //db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStrLive);
            //-------------------------------
            db.AddParameter("@user_id", userId, DbType.String);
            dv = db.ExecuteDataView("[dbo].[spe_t_site_list]", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public DataView GetUserDetails(string user_id)
        {
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@user_id", user_id, DbType.String);
            dv = db.ExecuteDataView("[spe_t_user_details]", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public DataView GetUserMapping()
        {
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            dv = db.ExecuteDataView("[spe_t_alluser_mapping]", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public void AddUserMapping(string user_id, int team_id)
        {
            db_helper db = new db_helper(db_helper.ConnectionStr.HelpdeskConnStr);
            db.AddParameter("@user_id", user_id, DbType.String);
            db.AddParameter("@team_id", team_id, DbType.Int32);
          
            db.ExecuteNonQuery("[spe_t_user_team_mapping_add]", System.Data.CommandType.StoredProcedure);

        }

        ////public static DataSet GetMailSettings(string site_id, string setting_category)
        ////{
        ////    //Configure the DatabaseFactory to read its configuration from the.config file
        ////    DatabaseProviderFactory factory = new DatabaseProviderFactory();

        ////    // Create the default Database object from the factory.
        ////    // The actual concrete type is determined by the configuration settings.
        ////    Database db = factory.Create("PortalConn");
        ////    DbCommand dbCommand = db.GetSqlStringCommand("select * from [VantagePortal].[dbo].[site_settings] where site_id = 1003 and setting_category = 'MAIL'");

        ////    return db.ExecuteDataSet(dbCommand);

        ////}
    }
}
