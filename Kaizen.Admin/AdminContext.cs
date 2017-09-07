using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Kaizen.Admin.Mapping;
using Kaizen.Admin;
namespace Kaizen.Admin.DAL
{
    public partial class AdminContext : DbContext
    {
        static AdminContext()
        {
            Database.SetInitializer<AdminContext>(null);
        }
        public AdminContext(string CompanyID, string UserName, string UserPassword)
            : base("Name=ERPContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("SSSS", SSSS.Trim());
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("TWO", CompanyID);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("XXXX", UserPassword);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("UUUU", UserName);
        }
        public DbSet<MS_00201> MS_00201 { get; set; }
        public DbSet<MS_00200> MS_00200 { get; set; }
        public DbSet<MS_00003> MS_00003 { get; set; }
        public DbSet<MS_00002> MS_00002 { get; set; }
        public DbSet<MS_00001> MS_00001 { get; set; }
        public DbSet<MS_00000> MS_00000 { get; set; }

        public DbSet<CRM00200> CRM00200 { get; set; }
        public DbSet<CRM00201> CRM00201 { get; set; }
        public DbSet<CRM00202> CRM00202 { get; set; }

        public DbSet<Sys00003> Sys00003 { get; set; }
        public DbSet<Sys00004> Sys00004 { get; set; }
        public DbSet<Sys00005> Sys00005 { get; set; }
        public DbSet<Sys00006> Sys00006 { get; set; }
        public DbSet<Sys00007> Sys00007 { get; set; }
        public DbSet<Sys00010> Sys00010 { get; set; }
        public DbSet<Sys00011> Sys00011 { get; set; }
        public DbSet<Sys00013> Sys00013 { get; set; }
        public DbSet<Sys00014> Sys00014 { get; set; }
        public DbSet<Sys00016> Sys00016 { get; set; }
        public DbSet<Sys00020> Sys00020 { get; set; }
        public DbSet<Sys00021> Sys00021 { get; set; }
        public DbSet<Sys00030> Sys00030 { get; set; }
        public DbSet<Sys00100> Sys00100 { get; set; }
        public DbSet<Sys00101> Sys00101 { get; set; }
        public DbSet<Sys00000> Sys00000 { get; set; }
        public DbSet<Sys00102> Sys00102 { get; set; }
        public DbSet<Sys00103> Sys00103 { get; set; }
        public DbSet<Sys00104> Sys00104 { get; set; }
        public DbSet<Sys00001> Sys00001 { get; set; }
        public DbSet<CPR00100> CPR00100 { get; set; }
        public DbSet<CPR00101> CPR00101 { get; set; }
        public DbSet<CPR00001> CPR00001 { get; set; }
        public DbSet<Sys00008> Sys00008 { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Sys00104Map());

            modelBuilder.Configurations.Add(new Sys00008Map());
            modelBuilder.Configurations.Add(new CPR00100Map());
            modelBuilder.Configurations.Add(new CPR00101Map());
            modelBuilder.Configurations.Add(new CPR00001Map());

            modelBuilder.Configurations.Add(new Sys00001Map());
            modelBuilder.Configurations.Add(new Sys00103Map());
            modelBuilder.Configurations.Add(new Sys00102Map());
            modelBuilder.Configurations.Add(new Sys00000Map());
            modelBuilder.Configurations.Add(new Sys00003Map());
            modelBuilder.Configurations.Add(new Sys00004Map());
            modelBuilder.Configurations.Add(new Sys00005Map());
            modelBuilder.Configurations.Add(new Sys00006Map());
            modelBuilder.Configurations.Add(new Sys00007Map());
            modelBuilder.Configurations.Add(new Sys00010Map());
            modelBuilder.Configurations.Add(new Sys00011Map());
            modelBuilder.Configurations.Add(new Sys00013Map());
            modelBuilder.Configurations.Add(new Sys00014Map());
            modelBuilder.Configurations.Add(new Sys00016Map());
            modelBuilder.Configurations.Add(new Sys00020Map());
            modelBuilder.Configurations.Add(new Sys00021Map());
            modelBuilder.Configurations.Add(new Sys00030Map());
            modelBuilder.Configurations.Add(new Sys00100Map());
            modelBuilder.Configurations.Add(new Sys00101Map());

            modelBuilder.Configurations.Add(new MS_00201Map());
            modelBuilder.Configurations.Add(new MS_00200Map());
            modelBuilder.Configurations.Add(new MS_00003Map());
            modelBuilder.Configurations.Add(new MS_00002Map());
            modelBuilder.Configurations.Add(new MS_00001Map());
            modelBuilder.Configurations.Add(new MS_00000Map());
            modelBuilder.Configurations.Add(new CRM00200Map());
            modelBuilder.Configurations.Add(new CRM00201Map());
            modelBuilder.Configurations.Add(new CRM00202Map());
        }
    }
}
