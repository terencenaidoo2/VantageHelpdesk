namespace VantageHelpdesk.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VantageAppUsersModel : DbContext
    {
        public VantageAppUsersModel()
            : base("name=vAppUsersConnStr")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
