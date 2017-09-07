using System.Data.Entity;
using Kaizen.Configuration.Mapping;

namespace Kaizen.Configuration.DAL
{ 
    public partial class ConfigurationContext : DbContext
    { 
        static ConfigurationContext()
        {
            Database.SetInitializer<ConfigurationContext>(null);
        }
        public ConfigurationContext()
          : base("Name=ERPContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            string UserPassword = cr.Decrypting(System.Configuration.ConfigurationManager.AppSettings["XXXX"]);
            string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
            string UUUU = System.Configuration.ConfigurationManager.AppSettings["UUUU"].ToString();
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("TWO", "Kaizen");
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("XXXX", "lw131");
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("UUUU", UUUU);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("SSSS", SSSS.Trim());
        }
        //public ConfigurationContext(string _UserName, string _UserPassword)
        //    : base("Name=ERPContext")
        //{
        //    this.Configuration.LazyLoadingEnabled = false;
        //    this.Configuration.ProxyCreationEnabled = false;
        //    CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
        //    string UserPassword = cr.Decrypting(System.Configuration.ConfigurationManager.AppSettings["XXXX"]);
        //    this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("TWO", CompanyID.Trim());
        //    this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("XXXX", UserPassword);
        //}
        //public ConfigurationContext(string _UserName, string _UserPassword,string UserName,string UserPassword)
        //   : base("Name=ERPContext")
        //{
        //    this.Configuration.LazyLoadingEnabled = false;
        //    this.Configuration.ProxyCreationEnabled = false;
           
        //    string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
        //    this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("SSSS", SSSS.Trim());
        //    this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("TWO", CompanyID.Trim());
        //    this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("XXXX", UserPassword);
        //    this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("UUUU", UserName);

        //}
        public ConfigurationContext(string UserName, string UserPassword)
           : base("Name=ERPContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("SSSS", SSSS.Trim());
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("TWO", "Kaizen");
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("XXXX", UserPassword);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("UUUU", UserName);
        }

        public DbSet<CM00000> CM00000 { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAccess> CompanyAccesses { get; set; }
        public DbSet<CompanySegment> CompanySegments { get; set; }
        public DbSet<Config00000> Config00000 { get; set; }
        public DbSet<Config00001> Config00001 { get; set; }
        public DbSet<Config00100> Config00100 { get; set; }
        public DbSet<DT00001> DT00001 { get; set; }
        public DbSet<DT00100> DT00100 { get; set; }
        public DbSet<DT00101> DT00101 { get; set; }
        public DbSet<DT00102> DT00102 { get; set; }
        public DbSet<DT00103> DT00103 { get; set; }
        public DbSet<Ext00001> Ext00001 { get; set; }
        public DbSet<Ext00002> Ext00002 { get; set; }
        public DbSet<Ext00003> Ext00003 { get; set; }
        public DbSet<Ext00004> Ext00004 { get; set; }
        public DbSet<Ext00005> Ext00005 { get; set; }
        public DbSet<Kaizen000> Kaizen000 { get; set; }
        public DbSet<Kaizen00000> Kaizen00000 { get; set; }
        public DbSet<Kaizen00001> Kaizen00001 { get; set; }
        public DbSet<Kaizen00002> Kaizen00002 { get; set; }
        public DbSet<Kaizen00003> Kaizen00003 { get; set; }
        public DbSet<Kaizen00004> Kaizen00004 { get; set; }
        public DbSet<Kaizen00005> Kaizen00005 { get; set; }
        public DbSet<Kaizen00006> Kaizen00006 { get; set; }
        public DbSet<Kaizen00007> Kaizen00007 { get; set; }
        public DbSet<Kaizen00008> Kaizen00008 { get; set; }
        public DbSet<Kaizen00009> Kaizen00009 { get; set; }
        public DbSet<Kaizen00010> Kaizen00010 { get; set; }
        public DbSet<Kaizen00011> Kaizen00011 { get; set; }
        public DbSet<Kaizen00012> Kaizen00012 { get; set; }
        public DbSet<Kaizen00013> Kaizen00013 { get; set; }
        public DbSet<Kaizen00014> Kaizen00014 { get; set; }
        public DbSet<Kaizen00015> Kaizen00015 { get; set; }
        public DbSet<Kaizen00016> Kaizen00016 { get; set; }
        public DbSet<Kaizen00020> Kaizen00020 { get; set; }
        public DbSet<Kaizen00025> Kaizen00025 { get; set; }
        public DbSet<Kaizen00026> Kaizen00026 { get; set; }
        public DbSet<Kaizen00030> Kaizen00030 { get; set; }
        public DbSet<Kaizen00040> Kaizen00040 { get; set; }
        public DbSet<Kaizen00050> Kaizen00050 { get; set; }
        public DbSet<Kaizen00051> Kaizen00051 { get; set; }
        public DbSet<Kaizen00052> Kaizen00052 { get; set; }
        public DbSet<Kaizen00053> Kaizen00053 { get; set; }
        public DbSet<Kaizen00054> Kaizen00054 { get; set; }
        public DbSet<Kaizen00055> Kaizen00055 { get; set; }
        public DbSet<Kaizen001> Kaizen001 { get; set; }
        public DbSet<Kaizen00101> Kaizen00101 { get; set; }
        public DbSet<Kaizen002> Kaizen002 { get; set; }
        public DbSet<Kaizen003> Kaizen003 { get; set; }
        public DbSet<Kaizen004> Kaizen004 { get; set; }
        public DbSet<Kaizen00400> Kaizen00400 { get; set; }
        public DbSet<Kaizen006> Kaizen006 { get; set; }
        public DbSet<Kaizen10200> Kaizen10200 { get; set; }
        public DbSet<KaizenAudit> KaizenAudits { get; set; }
        public DbSet<KaizenGridViewAccess> KaizenGridViewAccesses { get; set; }
        public DbSet<KaizenSequence> KaizenSequences { get; set; }
        public DbSet<KaizenSession> KaizenSessions { get; set; }
        public DbSet<KaizenSessionFail> KaizenSessionFails { get; set; }
        public DbSet<KaizenUserRole> KaizenUserRoles { get; set; }
        public DbSet<Met00001> Met00001 { get; set; }
        public DbSet<Met00002> Met00002 { get; set; }
        public DbSet<Met00003> Met00003 { get; set; }
        public DbSet<Met00005> Met00005 { get; set; }
        public DbSet<Met00006> Met00006 { get; set; }
        public DbSet<Met00007> Met00007 { get; set; }
        public DbSet<Met00008> Met00008 { get; set; }
        public DbSet<Met00009> Met00009 { get; set; }
        public DbSet<Met00011> Met00011 { get; set; }
        public DbSet<Met00012> Met00012 { get; set; }
        public DbSet<Met00013> Met00013 { get; set; }
        public DbSet<Met00100> Met00100 { get; set; }
        public DbSet<Met00101> Met00101 { get; set; }
        public DbSet<Met00102> Met00102 { get; set; }
        public DbSet<Met00200> Met00200 { get; set; }
        public DbSet<Met00201> Met00201 { get; set; }
        public DbSet<Met00202> Met00202 { get; set; }
        public DbSet<Met00203> Met00203 { get; set; }
        public DbSet<Met00204> Met00204 { get; set; }
        public DbSet<Met00205> Met00205 { get; set; }
        public DbSet<Met00206> Met00206 { get; set; }
        public DbSet<Prn00001> Prn00001 { get; set; }
        public DbSet<Prn00101> Prn00101 { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workstation> Workstations { get; set; }
        public DbSet<Kaizen004View> Kaizen004View { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CM00000Map());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyAccessMap());
            modelBuilder.Configurations.Add(new CompanySegmentMap());
            modelBuilder.Configurations.Add(new Config00000Map());
            modelBuilder.Configurations.Add(new Config00001Map());
            modelBuilder.Configurations.Add(new Config00100Map());
            modelBuilder.Configurations.Add(new DT00001Map());
            modelBuilder.Configurations.Add(new DT00100Map());
            modelBuilder.Configurations.Add(new DT00101Map());
            modelBuilder.Configurations.Add(new DT00102Map());
            modelBuilder.Configurations.Add(new DT00103Map());
            modelBuilder.Configurations.Add(new Ext00001Map());
            modelBuilder.Configurations.Add(new Ext00002Map());
            modelBuilder.Configurations.Add(new Ext00003Map());
            modelBuilder.Configurations.Add(new Ext00004Map());
            modelBuilder.Configurations.Add(new Ext00005Map());
            modelBuilder.Configurations.Add(new Kaizen000Map());
            modelBuilder.Configurations.Add(new Kaizen00000Map());
            modelBuilder.Configurations.Add(new Kaizen00001Map());
            modelBuilder.Configurations.Add(new Kaizen00002Map());
            modelBuilder.Configurations.Add(new Kaizen00003Map());
            modelBuilder.Configurations.Add(new Kaizen00004Map());
            modelBuilder.Configurations.Add(new Kaizen00005Map());
            modelBuilder.Configurations.Add(new Kaizen00006Map());
            modelBuilder.Configurations.Add(new Kaizen00007Map());
            modelBuilder.Configurations.Add(new Kaizen00008Map());
            modelBuilder.Configurations.Add(new Kaizen00009Map());
            modelBuilder.Configurations.Add(new Kaizen00010Map());
            modelBuilder.Configurations.Add(new Kaizen00011Map());
            modelBuilder.Configurations.Add(new Kaizen00012Map());
            modelBuilder.Configurations.Add(new Kaizen00013Map());
            modelBuilder.Configurations.Add(new Kaizen00014Map());
            modelBuilder.Configurations.Add(new Kaizen00015Map());
            modelBuilder.Configurations.Add(new Kaizen00016Map());
            modelBuilder.Configurations.Add(new Kaizen00020Map());
            modelBuilder.Configurations.Add(new Kaizen00025Map());
            modelBuilder.Configurations.Add(new Kaizen00026Map());
            modelBuilder.Configurations.Add(new Kaizen00030Map());
            modelBuilder.Configurations.Add(new Kaizen00040Map());
            modelBuilder.Configurations.Add(new Kaizen00050Map());
            modelBuilder.Configurations.Add(new Kaizen00051Map());
            modelBuilder.Configurations.Add(new Kaizen00052Map());
            modelBuilder.Configurations.Add(new Kaizen00053Map());
            modelBuilder.Configurations.Add(new Kaizen00054Map());
            modelBuilder.Configurations.Add(new Kaizen00055Map());
            modelBuilder.Configurations.Add(new Kaizen001Map());
            modelBuilder.Configurations.Add(new Kaizen00101Map());
            modelBuilder.Configurations.Add(new Kaizen002Map());
            modelBuilder.Configurations.Add(new Kaizen003Map());
            modelBuilder.Configurations.Add(new Kaizen004Map());
            modelBuilder.Configurations.Add(new Kaizen00400Map());
            modelBuilder.Configurations.Add(new Kaizen006Map());
            modelBuilder.Configurations.Add(new Kaizen10200Map());
            modelBuilder.Configurations.Add(new KaizenAuditMap());
            modelBuilder.Configurations.Add(new KaizenGridViewAccessMap());
            modelBuilder.Configurations.Add(new KaizenSequenceMap());
            modelBuilder.Configurations.Add(new KaizenSessionMap());
            modelBuilder.Configurations.Add(new KaizenSessionFailMap());
            modelBuilder.Configurations.Add(new KaizenUserRoleMap());
            modelBuilder.Configurations.Add(new Met00001Map());
            modelBuilder.Configurations.Add(new Met00002Map());
            modelBuilder.Configurations.Add(new Met00003Map());
            modelBuilder.Configurations.Add(new Met00005Map());
            modelBuilder.Configurations.Add(new Met00006Map());
            modelBuilder.Configurations.Add(new Met00007Map());
            modelBuilder.Configurations.Add(new Met00008Map());
            modelBuilder.Configurations.Add(new Met00009Map());
            modelBuilder.Configurations.Add(new Met00011Map());
            modelBuilder.Configurations.Add(new Met00012Map());
            modelBuilder.Configurations.Add(new Met00013Map());
            modelBuilder.Configurations.Add(new Met00100Map());
            modelBuilder.Configurations.Add(new Met00101Map());
            modelBuilder.Configurations.Add(new Met00102Map());
            modelBuilder.Configurations.Add(new Met00200Map());
            modelBuilder.Configurations.Add(new Met00201Map());
            modelBuilder.Configurations.Add(new Met00202Map());
            modelBuilder.Configurations.Add(new Met00203Map());
            modelBuilder.Configurations.Add(new Met00204Map());
            modelBuilder.Configurations.Add(new Met00205Map());
            modelBuilder.Configurations.Add(new Met00206Map());
            modelBuilder.Configurations.Add(new Prn00001Map());
            modelBuilder.Configurations.Add(new Prn00101Map());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new WorkstationMap());
            modelBuilder.Configurations.Add(new Kaizen004ViewMap());
        }

    }
}
