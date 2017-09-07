namespace UIServer.Reports
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    public partial class CMS_CaseStatusAdancedSumery : Telerik.Reporting.Report
    {
        public CMS_CaseStatusAdancedSumery()
        {
            InitializeComponent();
        }

        private void CMS_CaseStatusAdancedSumery_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.Report processingReport = (Telerik.Reporting.Processing.Report)sender;
            object processingParameterValue = processingReport.Parameters["DynTableName"].Value;
            string DynTableName = processingParameterValue.ToString();

            object processingParameterValue2 = processingReport.Parameters["DynFieldName"].Value;
            string DynFieldName = processingParameterValue2.ToString();

            object processingParameterCompanyID = processingReport.Parameters["KaizenPublicKey"].Value;
            string KaizenPublicKey = processingParameterCompanyID.ToString();
            Kaizen.Configuration.KaizenSession Usr = Kaizen.BusinessLogic.Master.GetKaizenSessionByConnectionId(KaizenPublicKey);

            SqlDataSource dd = new SqlDataSource();
            dd.ConnectionString = "Password=green123456;Persist Security Info=True;User ID=wael;Initial Catalog=Greenology;Data Source=192.168.15.175";
            dd.Name = "sqlDataSource1";
            this.DS.ConnectionString = dd.ConnectionString;



            string sql = this.DS.SelectCommand;
            sql = sql.Replace("Lookup01", DynFieldName.Trim());
            sql = sql.Replace("292", DynTableName.Trim());
            this.DS.SelectCommand = sql;


        }
    }
}