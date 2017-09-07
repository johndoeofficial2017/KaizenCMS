namespace UIServer.Reports
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for CMS_Letters.
    /// </summary>
    public partial class CMS_Letters : Telerik.Reporting.Report
    {
        public CMS_Letters()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.sqlDataSource1 = new SqlDataSource();
            this.sqlDataSource1.ConnectionString = "Password=green123456;Persist Security Info=True;User ID=wael;Initial Catalog=Greenology;Data Source=192.168.15.175";
        }

        private void CMS_Letters_ItemDataBinding(object sender, EventArgs e)
        {
            //Telerik.Reporting.Processing.Report processingReport = (Telerik.Reporting.Processing.Report)sender;
            //object processingParameterValue = processingReport.Parameters["ViewID"].Value;
            //int ViewID = int.Parse(processingParameterValue.ToString());

            //this.sqlDataSource1.SelectCommand = "";
            //this.DataSource = sqlDataSource1;
        }
    }
}