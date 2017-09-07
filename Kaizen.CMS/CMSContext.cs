using System.Data.Entity;
using Kaizen.CMS.Mapping;
namespace Kaizen.CMS.DAL
{
    public partial class CMSContext : DbContext
    {
        static CMSContext()
        {
            Database.SetInitializer<CMSContext>(null);
        }
        public CMSContext(string CompanyID)
          : base("Name=ERPContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            string UserPassword = cr.Decrypting(System.Configuration.ConfigurationManager.AppSettings["XXXX"]);
             
            string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
            string UUUU = System.Configuration.ConfigurationManager.AppSettings["UUUU"].ToString();
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("TWO", CompanyID);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("XXXX", UserPassword);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("UUUU", UUUU);
            this.Database.Connection.ConnectionString = this.Database.Connection.ConnectionString.Replace("SSSS", SSSS.Trim());
        }
        public CMSContext(string CompanyID ,string UserName, string UserPassword)
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
      
        public DbSet<CM00001> CM00001 { get; set; }
        public DbSet<CM00002> CM00002 { get; set; }
        public DbSet<CM00003> CM00003 { get; set; }
        public DbSet<CM00004> CM00004 { get; set; }
        public DbSet<CM00005> CM00005 { get; set; }
        public DbSet<CM00006> CM00006 { get; set; }
        public DbSet<CM00007> CM00007 { get; set; }
        public DbSet<CM00008> CM00008 { get; set; }
        public DbSet<CM00009> CM00009 { get; set; }
        public DbSet<CM00010> CM00010 { get; set; }
        public DbSet<CM00011> CM00011 { get; set; }
        public DbSet<CM00012> CM00012 { get; set; }
        public DbSet<CM00013> CM00013 { get; set; }
        public DbSet<CM00014> CM00014 { get; set; }
        public DbSet<CM00015> CM00015 { get; set; }
        public DbSet<CM00016> CM00016 { get; set; }
        public DbSet<CM00017> CM00017 { get; set; }
        public DbSet<CM00018> CM00018 { get; set; }
        public DbSet<CM00019> CM00019 { get; set; }
        public DbSet<CM00020> CM00020 { get; set; }
        public DbSet<CM00021> CM00021 { get; set; }
        public DbSet<CM00022> CM00022 { get; set; }
        public DbSet<CM00023> CM00023 { get; set; }
        public DbSet<CM00700> CM00700 { get; set; }
        public DbSet<CM00025> CM00025 { get; set; }
        public DbSet<CM00026> CM00026 { get; set; }
        public DbSet<CM00027> CM00027 { get; set; }
        public DbSet<CM00028> CM00028 { get; set; }
        public DbSet<CM00029> CM00029 { get; set; }
        public DbSet<CM00030> CM00030 { get; set; }
        public DbSet<CM00031> CM00031 { get; set; }
        public DbSet<CM00032> CM00032 { get; set; }
        public DbSet<CM00033> CM00033 { get; set; }
        public DbSet<CM00034> CM00034 { get; set; }
        public DbSet<CM00035> CM00035 { get; set; }
        public DbSet<CM00036> CM00036 { get; set; }
        public DbSet<CM00041> CM00041 { get; set; }
        public DbSet<CM00042> CM00042 { get; set; }
        public DbSet<CM00043> CM00043 { get; set; }
        public DbSet<CM00044> CM00044 { get; set; }
        public DbSet<CM00045> CM00045 { get; set; }
        public DbSet<CM00046> CM00046 { get; set; }
        public DbSet<CM00051> CM00051 { get; set; }
        public DbSet<CM00052> CM00052 { get; set; }
        public DbSet<CM00053> CM00053 { get; set; }
        public DbSet<CM00054> CM00054 { get; set; }
        public DbSet<CM00055> CM00055 { get; set; }
        public DbSet<CM00056> CM00056 { get; set; }
        public DbSet<CM00057> CM00057 { get; set; }
        public DbSet<CM00058> CM00058 { get; set; }
        public DbSet<CM00059> CM00059 { get; set; }
        public DbSet<CM00060> CM00060 { get; set; }
        public DbSet<CM00061> CM00061 { get; set; }
        public DbSet<CM00062> CM00062 { get; set; }
        public DbSet<CM00063> CM00063 { get; set; }
        public DbSet<CM00064> CM00064 { get; set; }
        public DbSet<CM00065> CM00065 { get; set; }
        public DbSet<CM00066> CM00066 { get; set; }
        public DbSet<CM00100> CM00100 { get; set; }
        public DbSet<CM00101> CM00101 { get; set; }
        public DbSet<CM00102> CM00102 { get; set; }
        public DbSet<CM00104> CM00104 { get; set; }
        public DbSet<CM00105> CM00105 { get; set; }
        public DbSet<CM00106> CM00106 { get; set; }
        public DbSet<CM00107> CM00107 { get; set; }
        public DbSet<CM00108> CM00108 { get; set; }
        public DbSet<CM00109> CM00109 { get; set; }
        public DbSet<CM00110> CM00110 { get; set; }
        public DbSet<CM00111> CM00111 { get; set; }
        public DbSet<CM00112> CM00112 { get; set; }
        public DbSet<CM00113> CM00113 { get; set; }
        public DbSet<CM00115> CM00115 { get; set; }
        public DbSet<CM00120> CM00120 { get; set; }
        public DbSet<CM00130> CM00130 { get; set; }
        public DbSet<CM00131> CM00131 { get; set; }
        public DbSet<CM00140> CM00140 { get; set; }
        public DbSet<CM00141> CM00141 { get; set; }
        public DbSet<CM00150> CM00150 { get; set; }
        public DbSet<CM00151> CM00151 { get; set; }
        public DbSet<CM00152> CM00152 { get; set; }
        public DbSet<CM00153> CM00153 { get; set; }
        public DbSet<CM00155> CM00155 { get; set; }
        public DbSet<CM00200> CM00200 { get; set; }
        public DbSet<CM00201> CM00201 { get; set; }
        public DbSet<CM00202> CM00202 { get; set; }
        public DbSet<CM00203> CM00203 { get; set; }
        public DbSet<CM002030> CM002030 { get; set; }
        public DbSet<CM00204> CM00204 { get; set; }
        public DbSet<CM10701> CM10701 { get; set; }
        public DbSet<CM00206> CM00206 { get; set; }
        public DbSet<CM00207> CM00207 { get; set; }
        public DbSet<CM10207> CM10207 { get; set; }
        public DbSet<CM20207> CM20207 { get; set; }
        public DbSet<CM00208> CM00208 { get; set; }
        public DbSet<CM00209> CM00209 { get; set; }
        public DbSet<CM00210> CM00210 { get; set; }
        public DbSet<CM00211> CM00211 { get; set; }
        public DbSet<CM00212> CM00212 { get; set; }
        public DbSet<CM00213> CM00213 { get; set; }
        public DbSet<CM00214> CM00214 { get; set; }
        public DbSet<CM00215> CM00215 { get; set; }
        public DbSet<CM00216> CM00216 { get; set; }
        public DbSet<CM00217> CM00217 { get; set; }
        public DbSet<CM00220> CM00220 { get; set; }
        public DbSet<CM00230> CM00230 { get; set; }
        public DbSet<CM00231> CM00231 { get; set; }
        public DbSet<CM00232> CM00232 { get; set; }
        public DbSet<CM00233> CM00233 { get; set; }
        public DbSet<CM00234> CM00234 { get; set; }
        public DbSet<CM00235> CM00235 { get; set; }
        public DbSet<CM00307> CM00307 { get; set; }
        public DbSet<CM00308> CM00308 { get; set; }
        public DbSet<CM00309> CM00309 { get; set; }
        public DbSet<CM10100> CM10100 { get; set; }
        public DbSet<CM10110> CM10110 { get; set; }
        public DbSet<CM10200> CM10200 { get; set; }
        public DbSet<CM10201> CM10201 { get; set; }
        public DbSet<CM20200> CM20200 { get; set; }
        public DbSet<CM20201> CM20201 { get; set; }
        public DbSet<CM20202> CM20202 { get; set; }
        public DbSet<CM20203> CM20203 { get; set; }
        public DbSet<CM20205> CM20205 { get; set; }
        public DbSet<CM20206> CM20206 { get; set; }
        public DbSet<CM20210> CM20210 { get; set; }
        public DbSet<CM30200> CM30200 { get; set; }
        public DbSet<CM30201> CM30201 { get; set; }
        public DbSet<CM30202> CM30202 { get; set; }
        public DbSet<CM30203> CM30203 { get; set; }
        public DbSet<CM30204> CM30204 { get; set; }
        public DbSet<CM30205> CM30205 { get; set; }
        public DbSet<CM30206> CM30206 { get; set; }
        public DbSet<CM30207> CM30207 { get; set; }
        public DbSet<CM30230> CM30230 { get; set; }

        public DbSet<CM_600200> CM_600200 { get; set; }
        public DbSet<CM_60601> CM_60601 { get; set; }
        public DbSet<CM_60610> CM_60610 { get; set; }
        public DbSet<CM_CaseUploadMissingAgent> CM_CaseUploadMissingAgent { get; set; }
        public DbSet<CM_LetterView> CM_LetterView { get; set; }
        public DbSet<CM_UploadValidateEmail01> CM_UploadValidateEmail01 { get; set; }
        public DbSet<CM_UploadValidateMissingNationalityID> CM_UploadValidateMissingNationalityID { get; set; }
        public DbSet<CM_UploadValidatePnone01> CM_UploadValidatePnone01 { get; set; }
        public DbSet<CM_UploadValidatePnone02> CM_UploadValidatePnone02 { get; set; }
        public DbSet<CM_UploadValidatePnone03> CM_UploadValidatePnone03 { get; set; }
        public DbSet<CM_UploadValidatePnone04> CM_UploadValidatePnone04 { get; set; }
        public DbSet<CM00203View01> CM00203View01 { get; set; }
        public DbSet<CM00203View02> CM00203View02 { get; set; }
        public DbSet<CMV00200> CMV00200 { get; set; }
        public DbSet<CMV00201> CMV00201 { get; set; }
        public DbSet<CMV00202> CMV00202 { get; set; }
        public DbSet<CM00170> CM00170 { get; set; }
        public DbSet<CM00038> CM00038 { get; set; }
        public DbSet<CM00039> CM00039 { get; set; }
        public DbSet<CM00040> CM00040 { get; set; }
        public DbSet<CM00047> CM00047 { get; set; }
        public DbSet<CM00080> CM00080 { get; set; }
        public DbSet<CM00081> CM00081 { get; set; }
        public DbSet<CM00088> CM00088 { get; set; }
        public DbSet<CM00089> CM00089 { get; set; }
        public DbSet<CM00070> CM00070 { get; set; }
        public DbSet<CM00071> CM00071 { get; set; }
        public DbSet<CM00072> CM00072 { get; set; }
        public DbSet<CM00073> CM00073 { get; set; }
        public DbSet<CM00074> CM00074 { get; set; }
        public DbSet<CM00075> CM00075 { get; set; }
        public DbSet<CM00076> CM00076 { get; set; }
        public DbSet<CM00077> CM00077 { get; set; }
        public DbSet<CM00078> CM00078 { get; set; }
        public DbSet<CM00024> CM00024 { get; set; }


        public DbSet<CM00048> CM00048 { get; set; }
        public DbSet<CM00049> CM00049 { get; set; }
        public DbSet<CM10307> CM10307 { get; set; }
        public DbSet<CM00037> CM00037 { get; set; }
        public DbSet<CM00050> CM00050 { get; set; }

        public DbSet<CM00079> CM00079 { get; set; }
        public DbSet<CM00205> CM00205 { get; set; }
        public DbSet<CM00085> CM00085 { get; set; }
        public DbSet<CM00086> CM00086 { get; set; }
        public DbSet<CM00084> CM00084 { get; set; }
        public DbSet<CM00083> CM00083 { get; set; }
        public DbSet<CM00082> CM00082 { get; set; }
        public DbSet<CM00116> CM00116 { get; set; }
        public DbSet<CM00117> CM00117 { get; set; }
        public DbSet<CM00118> CM00118 { get; set; }
        public DbSet<CM00119> CM00119 { get; set; }

        public DbSet<CM00090> CM00090 { get; set; }
        public DbSet<CM00091> CM00091 { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CM00118Map());
            modelBuilder.Configurations.Add(new CM00119Map());
            modelBuilder.Configurations.Add(new CM00091Map());
            modelBuilder.Configurations.Add(new CM00090Map());

            modelBuilder.Configurations.Add(new CM00116Map());
            modelBuilder.Configurations.Add(new CM00117Map());

            modelBuilder.Configurations.Add(new CM00086Map());
            modelBuilder.Configurations.Add(new CM00079Map());
            modelBuilder.Configurations.Add(new CM00205Map());
            modelBuilder.Configurations.Add(new CM00085Map());
            modelBuilder.Configurations.Add(new CM00084Map());
            modelBuilder.Configurations.Add(new CM00083Map());
            modelBuilder.Configurations.Add(new CM00082Map());

            modelBuilder.Configurations.Add(new CM00037Map());
            modelBuilder.Configurations.Add(new CM00050Map());

            modelBuilder.Configurations.Add(new CM00089Map());
            modelBuilder.Configurations.Add(new CM00088Map());

            modelBuilder.Configurations.Add(new CM00048Map());
            modelBuilder.Configurations.Add(new CM00049Map());
            modelBuilder.Configurations.Add(new CM10307Map());

            modelBuilder.Configurations.Add(new CM00081Map());
            modelBuilder.Configurations.Add(new CM00080Map());
            modelBuilder.Configurations.Add(new CM00024Map());
            modelBuilder.Configurations.Add(new CM00070Map());
            modelBuilder.Configurations.Add(new CM00071Map());
            modelBuilder.Configurations.Add(new CM00072Map());
            modelBuilder.Configurations.Add(new CM00073Map());
            modelBuilder.Configurations.Add(new CM00074Map());
            modelBuilder.Configurations.Add(new CM00075Map());
            modelBuilder.Configurations.Add(new CM00076Map());
            modelBuilder.Configurations.Add(new CM00077Map());
            modelBuilder.Configurations.Add(new CM00078Map());

            modelBuilder.Configurations.Add(new CM00038Map());
            modelBuilder.Configurations.Add(new CM00039Map());
            modelBuilder.Configurations.Add(new CM00040Map());
            modelBuilder.Configurations.Add(new CM00047Map());
            modelBuilder.Configurations.Add(new CM00170Map());
          
            modelBuilder.Configurations.Add(new CM00001Map());
            modelBuilder.Configurations.Add(new CM00002Map());
            modelBuilder.Configurations.Add(new CM00003Map());
            modelBuilder.Configurations.Add(new CM00004Map());
            modelBuilder.Configurations.Add(new CM00005Map());
            modelBuilder.Configurations.Add(new CM00006Map());
            modelBuilder.Configurations.Add(new CM00007Map());
            modelBuilder.Configurations.Add(new CM00008Map());
            modelBuilder.Configurations.Add(new CM00009Map());
            modelBuilder.Configurations.Add(new CM00010Map());
            modelBuilder.Configurations.Add(new CM00011Map());
            modelBuilder.Configurations.Add(new CM00012Map());
            modelBuilder.Configurations.Add(new CM00013Map());
            modelBuilder.Configurations.Add(new CM00014Map());
            modelBuilder.Configurations.Add(new CM00015Map());
            modelBuilder.Configurations.Add(new CM00016Map());
            modelBuilder.Configurations.Add(new CM00017Map());
            modelBuilder.Configurations.Add(new CM00018Map());
            modelBuilder.Configurations.Add(new CM00019Map());
            modelBuilder.Configurations.Add(new CM00020Map());
            modelBuilder.Configurations.Add(new CM00021Map());
            modelBuilder.Configurations.Add(new CM00022Map());
            modelBuilder.Configurations.Add(new CM00023Map());
            modelBuilder.Configurations.Add(new CM00700Map());
            modelBuilder.Configurations.Add(new CM00025Map());
            modelBuilder.Configurations.Add(new CM00026Map());
            modelBuilder.Configurations.Add(new CM00027Map());
            modelBuilder.Configurations.Add(new CM00028Map());
            modelBuilder.Configurations.Add(new CM00029Map());
            modelBuilder.Configurations.Add(new CM00030Map());
            modelBuilder.Configurations.Add(new CM00031Map());
            modelBuilder.Configurations.Add(new CM00032Map());
            modelBuilder.Configurations.Add(new CM00033Map());
            modelBuilder.Configurations.Add(new CM00034Map());
            modelBuilder.Configurations.Add(new CM00035Map());
            modelBuilder.Configurations.Add(new CM00036Map());
            modelBuilder.Configurations.Add(new CM00041Map());
            modelBuilder.Configurations.Add(new CM00042Map());
            modelBuilder.Configurations.Add(new CM00043Map());
            modelBuilder.Configurations.Add(new CM00044Map());
            modelBuilder.Configurations.Add(new CM00045Map());
            modelBuilder.Configurations.Add(new CM00046Map());
            modelBuilder.Configurations.Add(new CM00051Map());
            modelBuilder.Configurations.Add(new CM00052Map());
            modelBuilder.Configurations.Add(new CM00053Map());
            modelBuilder.Configurations.Add(new CM00054Map());
            modelBuilder.Configurations.Add(new CM00055Map());
            modelBuilder.Configurations.Add(new CM00056Map());
            modelBuilder.Configurations.Add(new CM00057Map());
            modelBuilder.Configurations.Add(new CM00058Map());
            modelBuilder.Configurations.Add(new CM00059Map());
            modelBuilder.Configurations.Add(new CM00060Map());
            modelBuilder.Configurations.Add(new CM00061Map());
            modelBuilder.Configurations.Add(new CM00062Map());
            modelBuilder.Configurations.Add(new CM00063Map());
            modelBuilder.Configurations.Add(new CM00064Map());
            modelBuilder.Configurations.Add(new CM00065Map());
            modelBuilder.Configurations.Add(new CM00066Map());
            modelBuilder.Configurations.Add(new CM00100Map());
            modelBuilder.Configurations.Add(new CM00101Map());
            modelBuilder.Configurations.Add(new CM00102Map());
            modelBuilder.Configurations.Add(new CM00104Map());
            modelBuilder.Configurations.Add(new CM00105Map());
            modelBuilder.Configurations.Add(new CM00106Map());
            modelBuilder.Configurations.Add(new CM00107Map());
            modelBuilder.Configurations.Add(new CM00108Map());
            modelBuilder.Configurations.Add(new CM00109Map());
            modelBuilder.Configurations.Add(new CM00110Map());
            modelBuilder.Configurations.Add(new CM00111Map());
            modelBuilder.Configurations.Add(new CM00112Map());
            modelBuilder.Configurations.Add(new CM00113Map());
            modelBuilder.Configurations.Add(new CM00115Map());
            modelBuilder.Configurations.Add(new CM00120Map());
            modelBuilder.Configurations.Add(new CM00130Map());
            modelBuilder.Configurations.Add(new CM00131Map());
            modelBuilder.Configurations.Add(new CM00140Map());
            modelBuilder.Configurations.Add(new CM00141Map());
            modelBuilder.Configurations.Add(new CM00150Map());
            modelBuilder.Configurations.Add(new CM00151Map());
            modelBuilder.Configurations.Add(new CM00152Map());
            modelBuilder.Configurations.Add(new CM00153Map());
            modelBuilder.Configurations.Add(new CM00155Map());
            modelBuilder.Configurations.Add(new CM00200Map());
            modelBuilder.Configurations.Add(new CM00201Map());
            modelBuilder.Configurations.Add(new CM00202Map());
            modelBuilder.Configurations.Add(new CM00203Map());
            modelBuilder.Configurations.Add(new CM002030Map());
            modelBuilder.Configurations.Add(new CM00204Map());
            modelBuilder.Configurations.Add(new CM10701Map());
            modelBuilder.Configurations.Add(new CM00206Map());
            modelBuilder.Configurations.Add(new CM00207Map());
            modelBuilder.Configurations.Add(new CM10207Map());
            modelBuilder.Configurations.Add(new CM20207Map());
            modelBuilder.Configurations.Add(new CM00208Map());
            modelBuilder.Configurations.Add(new CM00209Map());
            modelBuilder.Configurations.Add(new CM00210Map());
            modelBuilder.Configurations.Add(new CM00211Map());
            modelBuilder.Configurations.Add(new CM00212Map());
            modelBuilder.Configurations.Add(new CM00213Map());
            modelBuilder.Configurations.Add(new CM00214Map());
            modelBuilder.Configurations.Add(new CM00215Map());
            modelBuilder.Configurations.Add(new CM00216Map());
            modelBuilder.Configurations.Add(new CM00217Map());
            modelBuilder.Configurations.Add(new CM00220Map());
            modelBuilder.Configurations.Add(new CM00230Map());
            modelBuilder.Configurations.Add(new CM00231Map());
            modelBuilder.Configurations.Add(new CM00232Map());
            modelBuilder.Configurations.Add(new CM00233Map());
            modelBuilder.Configurations.Add(new CM00234Map());
            modelBuilder.Configurations.Add(new CM00235Map());
            modelBuilder.Configurations.Add(new CM00307Map());
            modelBuilder.Configurations.Add(new CM00308Map());
            modelBuilder.Configurations.Add(new CM00309Map());
            modelBuilder.Configurations.Add(new CM10100Map());
            modelBuilder.Configurations.Add(new CM10110Map());
            modelBuilder.Configurations.Add(new CM10200Map());
            modelBuilder.Configurations.Add(new CM10201Map());
            modelBuilder.Configurations.Add(new CM20200Map());
            modelBuilder.Configurations.Add(new CM20201Map());
            modelBuilder.Configurations.Add(new CM20202Map());
            modelBuilder.Configurations.Add(new CM20203Map());
            modelBuilder.Configurations.Add(new CM20205Map());
            modelBuilder.Configurations.Add(new CM20206Map());
            modelBuilder.Configurations.Add(new CM20210Map());
            modelBuilder.Configurations.Add(new CM30200Map());
            modelBuilder.Configurations.Add(new CM30201Map());
            modelBuilder.Configurations.Add(new CM30202Map());
            modelBuilder.Configurations.Add(new CM30203Map());
            modelBuilder.Configurations.Add(new CM30204Map());
            modelBuilder.Configurations.Add(new CM30205Map());
            modelBuilder.Configurations.Add(new CM30206Map());
            modelBuilder.Configurations.Add(new CM30207Map());
            modelBuilder.Configurations.Add(new CM30230Map());

            modelBuilder.Configurations.Add(new CM_600200Map());
            modelBuilder.Configurations.Add(new CM_60601Map());
            modelBuilder.Configurations.Add(new CM_60610Map());
            modelBuilder.Configurations.Add(new CM_CaseUploadMissingAgentMap());
            modelBuilder.Configurations.Add(new CM_LetterViewMap());
            modelBuilder.Configurations.Add(new CM_UploadValidateEmail01Map());
            modelBuilder.Configurations.Add(new CM_UploadValidateMissingNationalityIDMap());
            modelBuilder.Configurations.Add(new CM_UploadValidatePnone01Map());
            modelBuilder.Configurations.Add(new CM_UploadValidatePnone02Map());
            modelBuilder.Configurations.Add(new CM_UploadValidatePnone03Map());
            modelBuilder.Configurations.Add(new CM_UploadValidatePnone04Map());
            modelBuilder.Configurations.Add(new CM00203View01Map());
            modelBuilder.Configurations.Add(new CM00203View02Map());
            modelBuilder.Configurations.Add(new CMV00200Map());
            modelBuilder.Configurations.Add(new CMV00201Map());
            modelBuilder.Configurations.Add(new CMV00202Map());

        }
    }
}
