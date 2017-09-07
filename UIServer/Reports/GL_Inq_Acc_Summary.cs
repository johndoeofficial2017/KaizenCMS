namespace UIServer.Reports
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms; 
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for GL_Inq_Acc_Summary.
    /// </summary>
    public partial class GL_Inq_Acc_Summary : Telerik.Reporting.Report
    {
        public GL_Inq_Acc_Summary()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.SQL.ConnectionString = this.SQL.ConnectionString.Replace("TWO", "Greenology");
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}