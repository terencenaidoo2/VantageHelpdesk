namespace VantageHelpdesk.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HelpdeskModel : DbContext
    {
        public HelpdeskModel()
            : base("name=HelpdeskConnStr")
        {
        }

        public virtual DbSet<t_company> t_company { get; set; }

        public virtual DbSet<t_team> t_team { get; set; }
        public virtual DbSet<t_feedback> t_feedback { get; set; }
        public virtual DbSet<t_feedback_history> t_feedback_history { get; set; }
        public virtual DbSet<t_issue> t_issue { get; set; }
        public virtual DbSet<t_issue_history> t_issue_history { get; set; }
        public virtual DbSet<t_site> t_site { get; set; }
        public virtual DbSet<t_status> t_status { get; set; }
        public virtual DbSet<vw_t_feedback_history_list> vw_t_feedback_history_list { get; set; }
        public virtual DbSet<vw_t_feedback_list> vw_t_feedback_list { get; set; }
        public virtual DbSet<vw_t_issue_history_list> vw_t_issue_history_list { get; set; }
        public virtual DbSet<vw_t_issue_list> vw_t_issue_list { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_feedback_history>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<t_issue_history>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<vw_t_feedback_history_list>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<vw_t_issue_history_list>()
                .Property(e => e.comments)
                .IsUnicode(false);
        }
    }
}
