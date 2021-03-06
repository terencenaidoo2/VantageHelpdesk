namespace VantageHelpdesk.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_site
    {
        public int id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public int sort_order { get; set; }

        public DateTime? timestamp { get; set; }
    }
}
