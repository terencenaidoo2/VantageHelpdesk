namespace VantageHelpdesk.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_t_issue_list
    {
        [StringLength(1000)]
        public string issue { get; set; }

        [StringLength(128)]
        public string user_name { get; set; }

        [StringLength(50)]
        public string site_desc { get; set; }

        [StringLength(50)]
        public string status_desc { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int issue_id { get; set; }

        public DateTime? timestamp { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int status_id { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? team_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public string user_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int site_id { get; set; }

        [StringLength(100)]
        public string comment_count { get; set; }

        [StringLength(256)]
        public string current_owner { get; set; }

        [StringLength(128)]
        public string assigned_to_current_owner { get; set; }

        [StringLength(256)]
        public string account_manager { get; set; }

        [StringLength(128)]
        public string assigned_to_account_manager { get; set; }
    }
}
