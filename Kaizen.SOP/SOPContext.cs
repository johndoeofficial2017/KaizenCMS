using System.Data.Entity;
using Kaizen.SOP.Mapping;
namespace Kaizen.SOP.DAL
{
    public partial class SOPContext : DbContext
    {
        static SOPContext()
        {
            Database.SetInitializer<SOPContext>(null);
        }
        public SOPContext(string CompanyID, string UserName, string UserPassword)
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
        public DbSet<SOP00000> SOP00000 { get; set; }
        public DbSet<SOP00001> SOP00001 { get; set; }
        public DbSet<SOP000014> SOP000014 { get; set; }
        public DbSet<SOP00002> SOP00002 { get; set; }
        public DbSet<SOP00003> SOP00003 { get; set; }
        public DbSet<SOP00004> SOP00004 { get; set; }
        public DbSet<SOP00005> SOP00005 { get; set; }
        public DbSet<SOP00008> SOP00008 { get; set; }
        public DbSet<SOP00009> SOP00009 { get; set; }
        public DbSet<SOP00010> SOP00010 { get; set; }
        public DbSet<SOP00011> SOP00011 { get; set; }
        public DbSet<SOP00012> SOP00012 { get; set; }
        public DbSet<SOP00013> SOP00013 { get; set; }
        public DbSet<SOP00014> SOP00014 { get; set; }
        public DbSet<SOP00015> SOP00015 { get; set; }
        public DbSet<SOP00020> SOP00020 { get; set; }
        public DbSet<SOP00021> SOP00021 { get; set; }
        public DbSet<SOP00022> SOP00022 { get; set; }
        public DbSet<SOP00023> SOP00023 { get; set; }
        public DbSet<SOP00024> SOP00024 { get; set; }
        public DbSet<SOP00100> SOP00100 { get; set; }
        public DbSet<SOP00101> SOP00101 { get; set; }
        public DbSet<SOP00102> SOP00102 { get; set; }
        public DbSet<SOP00103> SOP00103 { get; set; }
        public DbSet<SOP00105> SOP00105 { get; set; }
        public DbSet<SOP00106> SOP00106 { get; set; }
        public DbSet<SOP00110> SOP00110 { get; set; }
        public DbSet<SOP00200> SOP00200 { get; set; }
        public DbSet<SOP00201> SOP00201 { get; set; }
        public DbSet<SOP00500> SOP00500 { get; set; }
        public DbSet<SOP00501> SOP00501 { get; set; }
        public DbSet<SOP00503> SOP00503 { get; set; }
        public DbSet<SOP00504> SOP00504 { get; set; }
        public DbSet<SOP00507> SOP00507 { get; set; }
        public DbSet<SOP00508> SOP00508 { get; set; }
        public DbSet<SOP10100> SOP10100 { get; set; }
        public DbSet<SOP10101> SOP10101 { get; set; }
        public DbSet<SOP10102> SOP10102 { get; set; }
        public DbSet<SOP10104> SOP10104 { get; set; }
        public DbSet<SOP10105> SOP10105 { get; set; }
        public DbSet<SOP10107> SOP10107 { get; set; }
        public DbSet<SOP10108> SOP10108 { get; set; }
        public DbSet<SOP10200> SOP10200 { get; set; }
        public DbSet<SOP10201> SOP10201 { get; set; }
        public DbSet<SOP10202> SOP10202 { get; set; }
        public DbSet<SOP10203> SOP10203 { get; set; }
        public DbSet<SOP10250> SOP10250 { get; set; }
        public DbSet<SOP10251> SOP10251 { get; set; }
        public DbSet<SOP10252> SOP10252 { get; set; }
        public DbSet<SOP10300> SOP10300 { get; set; }
        public DbSet<SOP10301> SOP10301 { get; set; }
        public DbSet<SOP10302> SOP10302 { get; set; }
        public DbSet<SOP10303> SOP10303 { get; set; }
        public DbSet<SOP10304> SOP10304 { get; set; }
        public DbSet<SOP10305> SOP10305 { get; set; }
        public DbSet<SOP10310> SOP10310 { get; set; }
        public DbSet<SOP10311> SOP10311 { get; set; }

        public DbSet<SOP10500> SOP10500 { get; set; }
        public DbSet<SOP10501> SOP10501 { get; set; }
        public DbSet<SOP10502> SOP10502 { get; set; }
        public DbSet<SOP10503> SOP10503 { get; set; }
        public DbSet<SOP10504> SOP10504 { get; set; }
        public DbSet<SOP10505> SOP10505 { get; set; }
        public DbSet<SOP10506> SOP10506 { get; set; }
        public DbSet<SOP10510> SOP10510 { get; set; }
        public DbSet<PROJ00100> PROJ00100 { get; set; }
        public DbSet<Sys00003> Sys00003 { get; set; }
        public DbSet<Sys00010> Sys00010 { get; set; }
        public DbSet<Sys00011> Sys00011 { get; set; }
        public DbSet<UPR00030> UPR00030 { get; set; }
        public DbSet<Sys00030> Sys00030 { get; set; }
        public DbSet<IV00100> IV00100 { get; set; }
        public DbSet<Sys00013> Sys00013 { get; set; }
        public DbSet<Sys00014> Sys00014 { get; set; }
        public DbSet<Sys00020> Sys00020 { get; set; }
        public DbSet<IV00108> IV00108 { get; set; }
        public DbSet<GL00100> GL00100 { get; set; }
        public DbSet<IV00014> IV00014 { get; set; }
        public DbSet<IV00013> IV00013 { get; set; }
        public DbSet<Sys00005> Sys00005 { get; set; }
        public DbSet<Sys00006> Sys00006 { get; set; }
        public DbSet<Sys00007> Sys00007 { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Sys00007Map());
            modelBuilder.Configurations.Add(new Sys00006Map());
            modelBuilder.Configurations.Add(new Sys00005Map());
            modelBuilder.Configurations.Add(new PROJ00100Map());


            modelBuilder.Configurations.Add(new SOP00000Map());
            modelBuilder.Configurations.Add(new SOP00001Map());
            modelBuilder.Configurations.Add(new SOP000014Map());
            modelBuilder.Configurations.Add(new SOP00002Map());
            modelBuilder.Configurations.Add(new SOP00003Map());
            modelBuilder.Configurations.Add(new SOP00004Map());
            modelBuilder.Configurations.Add(new SOP00005Map());
            modelBuilder.Configurations.Add(new SOP00008Map());
            modelBuilder.Configurations.Add(new SOP00009Map());
            modelBuilder.Configurations.Add(new SOP00010Map());
            modelBuilder.Configurations.Add(new SOP00011Map());
            modelBuilder.Configurations.Add(new SOP00012Map());
            modelBuilder.Configurations.Add(new SOP00013Map());
            modelBuilder.Configurations.Add(new SOP00014Map());
            modelBuilder.Configurations.Add(new SOP00015Map());
            modelBuilder.Configurations.Add(new SOP00020Map());
            modelBuilder.Configurations.Add(new SOP00021Map());
            modelBuilder.Configurations.Add(new SOP00022Map());
            modelBuilder.Configurations.Add(new SOP00023Map());
            modelBuilder.Configurations.Add(new SOP00024Map());
            modelBuilder.Configurations.Add(new SOP00100Map());
            modelBuilder.Configurations.Add(new SOP00101Map());
            modelBuilder.Configurations.Add(new SOP00102Map());
            modelBuilder.Configurations.Add(new SOP00103Map());
            modelBuilder.Configurations.Add(new SOP00105Map());
            modelBuilder.Configurations.Add(new SOP00106Map());
            modelBuilder.Configurations.Add(new SOP00110Map());
            modelBuilder.Configurations.Add(new SOP00200Map());
            modelBuilder.Configurations.Add(new SOP00201Map());
            modelBuilder.Configurations.Add(new SOP00500Map());
            modelBuilder.Configurations.Add(new SOP00501Map());
            modelBuilder.Configurations.Add(new SOP00503Map());
            modelBuilder.Configurations.Add(new SOP00504Map());
            modelBuilder.Configurations.Add(new SOP00507Map());
            modelBuilder.Configurations.Add(new SOP00508Map());
            modelBuilder.Configurations.Add(new SOP10100Map());
            modelBuilder.Configurations.Add(new SOP10101Map());
            modelBuilder.Configurations.Add(new SOP10102Map());
            modelBuilder.Configurations.Add(new SOP10104Map());
            modelBuilder.Configurations.Add(new SOP10105Map());
            modelBuilder.Configurations.Add(new SOP10107Map());
            modelBuilder.Configurations.Add(new SOP10108Map());
            modelBuilder.Configurations.Add(new SOP10200Map());
            modelBuilder.Configurations.Add(new SOP10201Map());
            modelBuilder.Configurations.Add(new SOP10202Map());
            modelBuilder.Configurations.Add(new SOP10203Map());
            modelBuilder.Configurations.Add(new SOP10250Map());
            modelBuilder.Configurations.Add(new SOP10251Map());
            modelBuilder.Configurations.Add(new SOP10252Map());
            modelBuilder.Configurations.Add(new SOP10300Map());
            modelBuilder.Configurations.Add(new SOP10301Map());
            modelBuilder.Configurations.Add(new SOP10302Map());
            modelBuilder.Configurations.Add(new SOP10303Map());
            modelBuilder.Configurations.Add(new SOP10304Map());
            modelBuilder.Configurations.Add(new SOP10305Map());
            modelBuilder.Configurations.Add(new SOP10310Map());
            modelBuilder.Configurations.Add(new SOP10311Map());
            modelBuilder.Configurations.Add(new SOP10500Map());
            modelBuilder.Configurations.Add(new SOP10501Map());
            modelBuilder.Configurations.Add(new SOP10502Map());
            modelBuilder.Configurations.Add(new SOP10503Map());
            modelBuilder.Configurations.Add(new SOP10504Map());
            modelBuilder.Configurations.Add(new SOP10505Map());
            modelBuilder.Configurations.Add(new SOP10506Map());
            modelBuilder.Configurations.Add(new SOP10510Map());
            modelBuilder.Configurations.Add(new Sys00003Map());
            modelBuilder.Configurations.Add(new Sys00010Map());
            modelBuilder.Configurations.Add(new Sys00011Map());
            modelBuilder.Configurations.Add(new UPR00030Map());
            modelBuilder.Configurations.Add(new Sys00030Map());
            modelBuilder.Configurations.Add(new IV00100Map());
            modelBuilder.Configurations.Add(new Sys00013Map());
            modelBuilder.Configurations.Add(new Sys00014Map());
            modelBuilder.Configurations.Add(new Sys00020Map());
            modelBuilder.Configurations.Add(new IV00108Map());
            modelBuilder.Configurations.Add(new GL00100Map());
            modelBuilder.Configurations.Add(new IV00014Map());
            modelBuilder.Configurations.Add(new IV00013Map());
        }
    }
}
