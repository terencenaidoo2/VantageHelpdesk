namespace VantageHelpdesk.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_issue_history
    {
        public int id { get; set; }

        public int issue_id { get; set; }

        [StringLength(128)]
        public string assigned_to_account_manager { get; set; }

        [StringLength(128)]
        public string assigned_to_current_owner { get; set; }

        [StringLength(1000)]
        public string comments { get; set; }

        public int? status_id { get; set; }

        public int? time_since_submitted { get; set; }

        public DateTime? timestamp { get; set; }

        [StringLength(128)]
        public string last_modified_by { get; set; }
    }
}
