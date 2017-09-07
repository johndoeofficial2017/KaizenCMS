namespace UIServer.Reports
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    public partial class CMLetterrpt : Telerik.Reporting.Report
    {
        public CMLetterrpt()
        {
            InitializeComponent();
        }
        private void CMLetterrpt_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.Report processingReport = (Telerik.Reporting.Processing.Report)sender;
            object processingParameterValue = processingReport.Parameters["CompanyID"].Value;
            string CompanyID = processingParameterValue.ToString();
            sqlDataSource1.ConnectionString = Kaizen.BusinessLogic.Master.ReportConnectionString(CompanyID);
            this.DataSource = sqlDataSource1;
        }

        //private void CMLetterrpt_NeedDataSource(object sender, EventArgs e)
        //{
        //    Telerik.Reporting.Processing.Report processingReport = (Telerik.Reporting.Processing.Report)sender;
        //    object processingParameterValue = processingReport.Parameters["CompanyID"].Value;
        //    string CompanyID = processingParameterValue.ToString();
        //    sqlDataSource1.ConnectionString = Kaizen.BusinessLogic.Master.ReportConnectionString(CompanyID);
        //    this.DataSource = sqlDataSource1;
        //}
    }
}
