namespace UIServer.Reports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    public partial class GridViewDynamic : Telerik.Reporting.Report
    {
        private SqlDataSource sqlDataSource1;
        public GridViewDynamic()
        {
            InitializeComponent();
            //ReportParameters["ViewID"].Value = 13533;
            //ReportParameters["SessionID"].Value = 10;
           
            //this.sqlDataSource1 = new SqlDataSource();
            //this.sqlDataSource1.ConnectionString = "Password=green123456;Persist Security Info=True;User ID=wael;Initial Catalog=Greenology;Data Source=192.168.15.175";
            //this.sqlDataSource1.Name = "sqlDataSource1";
        }
        private void GridViewDynamic_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.Report processingReport = (Telerik.Reporting.Processing.Report)sender;
            object processingCompanyIDValue = processingReport.Parameters["CompanyID"].Value;
            string CompanyID = processingCompanyIDValue.ToString();
            this.sqlDataSource1 = new SqlDataSource();
            sqlDataSource1.ConnectionString = Kaizen.BusinessLogic.Master.ReportConnectionString(CompanyID);
            this.sqlDataSource1.Name = "sqlDataSource1";
            //this.DataSource = sqlDataSource1;

            object processingParameterValue = processingReport.Parameters["ViewID"].Value;
            int ViewID = int.Parse(processingParameterValue.ToString());

            Kaizen.BusinessLogic.Configuration.Kaizen00011Services Viewsrv = new Kaizen.BusinessLogic.Configuration.Kaizen00011Services();
            Kaizen.Configuration.Kaizen00011 view = Viewsrv.GetSingle(ViewID);

            Kaizen.BusinessLogic.Configuration.Kaizen00010Services screensrv = new Kaizen.BusinessLogic.Configuration.Kaizen00010Services();
            Kaizen.Configuration.Kaizen00010 screen = screensrv.GetSingle(view.ScreenID);

            Kaizen.BusinessLogic.Configuration.Kaizen00026Services ViewFieldssrv = new Kaizen.BusinessLogic.Configuration.Kaizen00026Services();
            List<Kaizen.Configuration.Kaizen00026> filds = ViewFieldssrv.GetFieldsByView(view.ViewID);
            string strQuery = "SELECT top 100 ";

            groupHeaderSection1.Items.Clear();
            detail.Items.Clear();
            double usedWidth = 0;
            int totalColumnWidth = 0;
            foreach (Kaizen.Configuration.Kaizen00026 fild in filds)
            {
                if (!fild.IsPrintable) continue;
                if (string.IsNullOrEmpty(fild.FieldEquation))
                    strQuery += fild.field + ",";
                else
                    strQuery += fild.FieldEquation.Trim() + "as " + fild.field + ",";
                totalColumnWidth += fild.width;
            }
            strQuery = strQuery.Remove(strQuery.Length - 1, 1);
            strQuery += " From " + view.CompanyID.Trim() + ".dbo." + screen.MainTable.Trim();
            if (!string.IsNullOrEmpty(view.ViewWhereCondition))
                strQuery += " where " + view.ViewWhereCondition;
            this.sqlDataSource1.SelectCommand = strQuery;
            this.sqlDataSource1.ProviderName = "System.Data.SqlClient";
            this.sqlDataSource1.SelectCommandType = SqlDataSourceCommandType.Text;

            this.DataSource = sqlDataSource1;
            txtHeader.Width = this.Width = Unit.Inch(totalColumnWidth / 100);
            //this.Width = Unit.Inch(totalColumnWidth / 100);
            usedWidth = 0;
            foreach (Kaizen.Configuration.Kaizen00026 fild in filds)
            {
                Telerik.Reporting.TextBox item = new Telerik.Reporting.TextBox();
                Telerik.Reporting.TextBox header = new Telerik.Reporting.TextBox();
                Telerik.Reporting.PictureBox items = new Telerik.Reporting.PictureBox();

                item.Value = "= Fields." + fild.field;
                header.Value = fild.field;
                header.Name = fild.field;
                // 1 String 4 bool 
                // 2 Datetime 
                if (fild.FieldTypeID == 2)
                    item.Format = "{0:dd.MM.yyyy}";
                if (fild.FieldTypeID == 3)
                    item.Format = "{0:0.000}";
                item.Value = "= Fields." + fild.field;
                //if (fild.FieldTypeID == 5)
                //    items.Value = "=getPicturePath(Fields." + fild.field + ",'"
                //        + strPath + "')";
                item.Name = "textBox_" + fild.field;

                item.Location = new PointU(Unit.Inch(usedWidth / 100), Unit.Inch(3.9418537198798731E-05D));
                item.Size = new SizeU(Unit.Inch(fild.width / 100), Unit.Inch(1));

                items.Location = new PointU(Unit.Inch(usedWidth / 100), Unit.Inch(3.9418537198798731E-05D));
                items.Size = new SizeU(Unit.Inch(fild.width / 100), Unit.Inch(1));
                header.Location = new PointU(Unit.Inch(usedWidth / 100), groupHeaderSection1.Height - Unit.Inch(0.20000012218952179D));
                header.Size = new SizeU(Unit.Inch(fild.width / 100), Unit.Inch(0.20000012218952179D));
                usedWidth += fild.width;
                BorderType ss = BorderType.Solid;
                item.Style.BorderStyle.Default = ss;
                items.Style.BorderStyle.Default = ss;
                header.Style.BorderStyle.Default = ss;
                header.Style.TextAlign = HorizontalAlign.Center;
                header.Style.VerticalAlign = VerticalAlign.Middle;
                item.Style.TextAlign = HorizontalAlign.Center;
                item.Style.VerticalAlign = VerticalAlign.Middle;
                //groupHeaderSection1.PrintOnFirstPage = true;
                pageHeaderSection1.Visible = true;
                items.Height = Unit.Inch(1);
                items.Sizing = ImageSizeMode.Stretch;
                groupHeaderSection1.Items.AddRange(new ReportItem[] { header });
                if (fild.FieldTypeID == 5)
                    detail.Items.AddRange(new ReportItem[] { items });
                else
                    detail.Items.AddRange(new ReportItem[] { item });
            }

        }
        public static string getPicturePath(string strItemId, string paths)
        {
            string path = @"C:\inetpub\wwwroot\GPPortal\Photo\ItemPhotos\" + strItemId.Trim() + ".png";
            if (File.Exists(path))
            {
                return path;
            }
            else
                return @"C:\inetpub\wwwroot\GPPortal\Photo\ItemPhotos\ItemID.jpg"; ;
        }
    }
}


