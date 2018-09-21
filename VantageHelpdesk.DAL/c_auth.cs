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
    public class c_auth
    {
        public bool ValidateUser(string user_id, string application_id)
        {
            bool is_valid = false;
            DataView dv = new DataView();
            db_helper db = new db_helper(db_helper.ConnectionStr.vAppUsersConnStr);
            db.AddParameter("@user_id", user_id, DbType.String);
            db.AddParameter("@application_id", application_id, DbType.String);
            dv = db.ExecuteDataView("spe_sso_validateuserforapp", System.Data.CommandType.StoredProcedure);
            if (dv.Table.Rows.Count > 0)
            {
                is_valid = true;
            }
            return is_valid;
        }
    }
}
