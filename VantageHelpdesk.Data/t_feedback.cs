namespace VantageHelpdesk.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_feedback
    {
        public int id { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [Required]
        [StringLength(128)]
        public string user_id { get; set; }

        [StringLength(128)]
        public string user_name { get; set; }

        public int site_id { get; set; }

        public int team_id { get; set; }

        public int? status_id { get; set; }

        [StringLength(20)]
        public string priority { get; set; }

        public DateTime? timestamp { get; set; }

        public int? total_time_since_submitted { get; set; }

        [StringLength(128)]
        public string last_modified_by { get; set; }

        [StringLength(128)]
        public string assigned_to_current_owner { get; set; }
    }
}
