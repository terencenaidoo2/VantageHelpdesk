namespace VantageHelpdesk.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_t_issue_history_list
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int issue_id { get; set; }

        [StringLength(1000)]
        public string comments { get; set; }

        [StringLength(100)]
        public string TimeSinceSubmitted { get; set; }

        public DateTime? timestamp { get; set; }

        [StringLength(256)]
        public string AccountManager { get; set; }

        [StringLength(256)]
        public string CurrentOwner { get; set; }

        [StringLength(256)]
        public string LastModifiedBy { get; set; }

        [StringLength(50)]
        public string status_desc { get; set; }
    }
}
