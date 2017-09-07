using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using UDL;
using Kaizen.Configuration;
using Kaizen.BusinessLogic;
using KaizenSetup.Token_Setup;

namespace KaizenSetup
{
    public partial class Main : Telerik.WinControls.UI.ShapedForm
    {
        public Main()
        {
            InitializeComponent();
            this.radWizard1.SelectedPageChanged += new SelectedPageChangedEventHandler(radWizard1_SelectedPageChanged);
            this.radWizard1.Cancel += new EventHandler(radWizard1_Cancel);
            this.radWizard1.Finish += new EventHandler(radWizard1_Finish);
            this.radWizard1.Next += new WizardCancelEventHandler(radWizard1_Next);

            if (SystemScriptFile_BrowseEditor.DialogType == BrowseEditorDialogType.OpenFileDialog)
            {
                OpenFileDialog dialog = (OpenFileDialog)SystemScriptFile_BrowseEditor.Dialog;
                dialog.Filter = "(*.sql)|*.sql|(*.txt)|*.txt";
            }
            if (CompanyScriptFile_BrowseEditor.DialogType == BrowseEditorDialogType.OpenFileDialog)
            {
                OpenFileDialog dialog = (OpenFileDialog)CompanyScriptFile_BrowseEditor.Dialog;
                dialog.Filter = "(*.sql)|*.sql|(*.txt)|*.txt";
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            foreach (WizardPage page in this.radWizard1.Pages)
            {
                page.Icon = this.radWizard1.PageHeaderIcon;
            }
            this.radWizard1.PageHeaderElement.IconElement.Margin = new Padding(0, 20, 18, 0);
            this.radWizard1.NextButton.Enabled = false;
        }
        private void radWizard1_Cancel(object sender, EventArgs e)
        {
            this.Close();
        }
        private void radWizard1_Finish(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void radWizard1_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {
            this.radWizard1.HelpButton.Visibility = ElementVisibility.Hidden;

            #region Welcome Page
            if (e.SelectedPage == this.wizardWelcomePage1)
            {
                this.Confirm_CHKBX.CheckStateChanged += new EventHandler(Confirm_CHKBX_CheckStateChanged);
            }
            #endregion

            #region Server Connection Page
            if (e.SelectedPage == this.wizardServerConnectionPage)
            {
                if (!string.IsNullOrWhiteSpace(Provider_TXT.Text) && !string.IsNullOrWhiteSpace(ConnectionString_RTXT.Text))
                    this.radWizard1.NextButton.Enabled = true;
                else
                    this.radWizard1.NextButton.Enabled = false;

                this.radWizard1.BackButton.Enabled = true;
                //this.radWaitingBarSearch.StartWaiting();
                //this.timerSearch.Start();
            }
            #endregion

            #region System DB Page
            if (e.SelectedPage == this.wizardSystemDBPage)
            {
                if (!string.IsNullOrWhiteSpace(SystemScriptFile_BrowseEditor.Value)
                    && !string.IsNullOrWhiteSpace(System_TXT.Text))
                    this.radWizard1.NextButton.Enabled = true;
                else
                    this.radWizard1.NextButton.Enabled = false;

                this.radWizard1.BackButton.SetDefaultValueOverride(RadElement.VisibilityProperty, ElementVisibility.Collapsed);
            }
            #endregion

            #region Company DB Page
            else if (e.SelectedPage == this.wizardCompanyDBPage)
            {
                if (!string.IsNullOrWhiteSpace(CompanyScriptFile_BrowseEditor.Value)
                    && !string.IsNullOrWhiteSpace(CompanyCode_TXT.Text)
                    && !string.IsNullOrWhiteSpace(CompanyName_TXT.Text)
                    && !string.IsNullOrWhiteSpace(SegmentMark_TXT.Text))
                    this.radWizard1.NextButton.Enabled = true;
                else
                    this.radWizard1.NextButton.Enabled = false;

                this.radWizard1.BackButton.SetDefaultValueOverride(RadElement.VisibilityProperty, ElementVisibility.Collapsed);
            }
            #endregion

            #region System Admin Page
            else if (e.SelectedPage == this.wizardSystemAdminPage)
            {
                if (!string.IsNullOrWhiteSpace(Username_TXT.Text)
                   && !string.IsNullOrWhiteSpace(UserPassword_TXT.Text))
                    this.radWizard1.NextButton.Enabled = true;
                else
                    this.radWizard1.NextButton.Enabled = false;

                this.radWizard1.BackButton.SetDefaultValueOverride(RadElement.VisibilityProperty, ElementVisibility.Collapsed);
            }
            #endregion

            #region Complete Page
            else if (e.SelectedPage == this.wizardCompletionPage1)
            {
                this.radWizard1.NextButton.SetDefaultValueOverride(RadElement.VisibilityProperty, ElementVisibility.Collapsed);
                this.radWizard1.FinishButton.SetDefaultValueOverride(RadElement.VisibilityProperty, ElementVisibility.Visible);
            }
            #endregion
        }
        private void radWizard1_Next(object sender, WizardCancelEventArgs e)
        {
            #region System DB Page
            if (this.radWizard1.SelectedPage == this.wizardSystemDBPage)
            {
                FileInfo file = new FileInfo(SystemScriptFile_BrowseEditor.Value);
                string script = File.ReadAllText(SystemScriptFile_BrowseEditor.Value);
                KaizenReplacer kr = new KaizenReplacer("[", "]", false);
                kr.Append(script);
                kr.Replace("[Kaizen]", System_TXT.Text.Trim());
                SqlConnection conn = new SqlConnection(ConnectionString_RTXT.Text);
                conn.Open();
                SqlCommand com = new SqlCommand(kr.ToString(), conn);
                com.ExecuteNonQuery();
                conn.Close();
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.DoWork += new DoWorkEventHandler(worker_SystemDBDoWork);
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerAsync();
            }
            #endregion

            #region Company DB Page
            if (this.radWizard1.SelectedPage == this.wizardCompanyDBPage)
            {
                FileInfo file = new FileInfo(CompanyScriptFile_BrowseEditor.Value);
                string script = File.ReadAllText(CompanyScriptFile_BrowseEditor.Value);
                KaizenReplacer kr = new KaizenReplacer("[", "]", false);
                kr.Append(script);
                kr.Replace("[Greenology]", CompanyName_TXT.Text.Trim());
                SqlConnection conn = new SqlConnection(ConnectionString_RTXT.Text);
                conn.Open();
                SqlCommand com = new SqlCommand(kr.ToString(), conn);
                com.ExecuteNonQuery();
                conn.Close();
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.DoWork += new DoWorkEventHandler(worker_CompanyDBDoWork);
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerAsync();
                //Kaizen.Configuration.Repository.CompanyRepository rep = new Kaizen.Configuration.Repository.CompanyRepository(System_TXT.Text.Trim());
                //var result = rep.AddKaizenObject(new Company()
                //{
                //    CompanyID = CompanyCode_TXT.Text,
                //    CompanyName = CompanyName_TXT.Text,
                //    SegmentMark = SegmentMark_TXT.Text
                //});
                //if (result.Status)
                //{

                //}
            }
            #endregion

            #region System Admin Page
            if (this.radWizard1.SelectedPage == this.wizardSystemAdminPage)
            {
                Kaizen.Configuration.Repository.UserRepository rep = new Kaizen.Configuration.Repository.UserRepository();
                var result = rep.AddKaizenObject(new Kaizen.Configuration.User()
                {
                    UserName = Username_TXT.Text,
                    //UserPassword = BCrypt.Net.BCrypt.HashPassword(UserPassword_TXT.Text),
                    IsAgent = true,
                    IsClient = true,
                    IsCustomer = true,
                    IsDebtor = true,
                    IsDisabled = false,
                    IsEmployee = true,
                    IsPartner = true,
                    IsVendor = true
                });
                if (result.Status)
                {
                    //Kaizen.Configuration.Repository.CompanyAccessRepository repo = new Kaizen.Configuration.Repository.CompanyAccessRepository(UserContext.UserName, UserContext.Password);
                    //repo.AddKaizenObject(new CompanyAccess()
                    //{
                    //    //CompanyID = CompanyCode_TXT.Text,
                    //    UserName = Username_TXT.Text
                    //}
                    //);
                }
            }
            #endregion
        }

        private void Connect_BTN_Click(object sender, EventArgs e)
        {
            ConnectionStringDialog fd = new ConnectionStringDialog();
            if (fd.ShowDialog(this) != DialogResult.OK)
                return;

            Provider_TXT.Text = fd.Provider;
            ConnectionString_RTXT.Text = fd.ConnectionString;
            if (!string.IsNullOrWhiteSpace(Provider_TXT.Text) && !string.IsNullOrWhiteSpace(ConnectionString_RTXT.Text))
            {
                this.radWizard1.NextButton.Enabled = true;

            }
        }

        void worker_SystemDBDoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo dbdir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Kaizen DB Objects"));
            int totalsteps = dbdir.GetFiles().Length;
            var progress = 0;
            foreach (var currentfile in dbdir.GetFiles())
            {
                progress++;
                string script = File.ReadAllText(currentfile.FullName);
                KaizenReplacer kr = new KaizenReplacer("[", "]", false);
                kr.Append(script);
                kr.Replace("[Kaizen]", System_TXT.Text.Trim());
                SqlConnection conn = new SqlConnection(ConnectionString_RTXT.Text);
                Server db = new Server(new ServerConnection(conn));
                db.ConnectionContext.ExecuteNonQuery(kr.ToString());
                (sender as BackgroundWorker).ReportProgress(1 / totalsteps + progress);
            }
            e.Result = totalsteps;
        }
        void worker_CompanyDBDoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo dbdir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Company DB Objects"));
            int totalsteps = dbdir.GetFiles().Length;
            var progress = 0;
            foreach (var currentfile in dbdir.GetFiles())
            {
                progress++;
                string script = File.ReadAllText(currentfile.FullName);
                KaizenReplacer kr = new KaizenReplacer("[", "]", false);
                kr.Append(script);
                kr.Replace("[Greenology]", CompanyName_TXT.Text.Trim());
                SqlConnection conn = new SqlConnection(ConnectionString_RTXT.Text);
                Server db = new Server(new ServerConnection(conn));
                db.ConnectionContext.ExecuteNonQuery(kr.ToString());
                (sender as BackgroundWorker).ReportProgress(1 / totalsteps + progress);
            }
            e.Result = totalsteps;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value1 = e.ProgressPercentage;
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.radDesktopAlert1.CaptionText = "Proccess Completed";
            this.radDesktopAlert1.ContentText = "Completed processing " + e.Result.ToString() + " items";
            this.radDesktopAlert1.Show();
        }


        #region Form Events
        private void Confirm_CHKBX_CheckStateChanged(object sender, EventArgs e)
        {
            if (Confirm_CHKBX.Checked)
                this.radWizard1.NextButton.Enabled = true;
            else
                this.radWizard1.NextButton.Enabled = false;
        }
        private void CompanyCode_TXT_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompanyScriptFile_BrowseEditor.Value)
                && !string.IsNullOrWhiteSpace(CompanyCode_TXT.Text)
                && !string.IsNullOrWhiteSpace(CompanyName_TXT.Text)
                && !string.IsNullOrWhiteSpace(SegmentMark_TXT.Text))
                this.radWizard1.NextButton.Enabled = true;
            else
                this.radWizard1.NextButton.Enabled = false;
        }
        private void System_TXT_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SystemScriptFile_BrowseEditor.Value)
                && !string.IsNullOrWhiteSpace(System_TXT.Text))
                this.radWizard1.NextButton.Enabled = true;
            else
                this.radWizard1.NextButton.Enabled = false;
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dlg = RadMessageBox.Show(this, "Are you sure, you want to cancel setup ?", "Setup Cancel", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1, RightToLeft.No);
            if (dlg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void Username_TXT_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username_TXT.Text)
              && !string.IsNullOrWhiteSpace(UserPassword_TXT.Text))
                this.radWizard1.NextButton.Enabled = true;
            else
                this.radWizard1.NextButton.Enabled = false;
        }
        #endregion
    }
}
