﻿
namespace VantageHelpdesk.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_team
    {
        public int id { get; set; }

        [StringLength(100)]
        public string description { get; set; }

    }
}
