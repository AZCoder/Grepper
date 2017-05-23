using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrepperLib.Controller;
using GrepperLib.Model;
using GrepperLib.Domain;
using UMessage = GrepperLib.Utility;

namespace GrepperView
{
    public partial class MainUI : Form
    {
        #region Private Members________

        private readonly FileController _fileController;
        private readonly ISettings _settings;
        private Settings _appSettings;
        private static BackgroundWorker _workerThread;

        #endregion
        #region Constructor____________

        /// <summary>
        /// Sets folder path, tool tips, and loads settings.
        /// </summary>
        /// <param name="path"></param>
        public MainUI(string path)
        {
            if (path.StartsWith("-p")) path = path.Substring(2);

            InitializeComponent();
            timeCounter.Enabled = false;
            progressBar.Visible = false;
            txtBaseSearchPath.Text = path;
            _fileController = new FileController();
            _settings = new SettingsManager();

            ActiveControl = ddlSearchCriteria;

            // set tool tips
            SetToolTips();

            // load user settings
            LoadSettings();
        }

        #endregion
        #region Protected Methods______

        /// <summary>
        /// Custom gradient color background.
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
            {
                base.OnPaintBackground(e);
                return;
            }
            
            using (var brush = new LinearGradientBrush(ClientRectangle, Color.White, Color.LightSteelBlue, 90F))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        /// <summary>
        /// Invalidates the form so that it will resize the controls correctly and repaint.
        /// </summary>
        protected void MainUI_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Doubleclick will attempt to open the file in its default associated application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FileMatches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var itemSelected = lvwFileMatches.SelectedItems[0].Text;
            var fileMatch = from match in _fileController.FileDataList
                            where match.FilePath == itemSelected
                            select match;

            var fileDatas = fileMatch as IList<FileData> ?? fileMatch.ToList();
            if (!fileDatas.Any())
                return;

            using (var proc = new Process() { StartInfo = new ProcessStartInfo(fileDatas.First().FilePath) })
            {
                proc.Start();
            }
        }

        /// <summary>
        /// Delegate method for timer object. Increments the progressBar by a pre-defined step for each tick of the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TimeCounter_Tick(object sender, EventArgs e)
        {
            // reset to 0 if max reached, only need to show user that program is doing something and not frozen
            if (progressBar.Value >= progressBar.Maximum)
                progressBar.Value = 0;
            
            progressBar.PerformStep();
        }

        /// <summary>
        /// Right-click will copy contents to clipboard.
        /// </summary>
        protected void LineData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CopyToClipboard();
            }
        }

        /// <summary>
        /// Clicking the search path pulls up a custom directory dialog box.
        /// </summary>
        protected void BaseSearchPath_Click(object sender, EventArgs e)
        {
            var db = new DirectoryBrowser()
            {
                StartDirectory = txtBaseSearchPath.Text.Trim()
            };

            if (db.ShowDialog() == DialogResult.OK)
            {
                txtBaseSearchPath.Text = db.NodeSelected;
            }
        }

        /// <summary>
        /// Display file contents in lower view section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FileMatches_MouseClick(object sender, MouseEventArgs e)
        {
            lvwLineData.Items.Clear();
            var itemSelected = lvwFileMatches.SelectedItems[0].Text;
            var fileMatch = (from match in _fileController.FileDataList
                             where match.FilePath == itemSelected
                             select match.LineDataList).FirstOrDefault();

            if (fileMatch == null || !fileMatch.Any()) return;
            foreach (var item in fileMatch)
            {
                var lvi = new ListViewItem(new[] { item.Key.ToString(), item.Value });
                lvwLineData.Items.Add(lvi);
            }

            SetAlternateRowColor(lvwLineData);
        }

        protected void Extensions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var extensions = new ExtensionsUI();
            extensions.ShowDialog();
            LoadSettings();
        }

        #endregion
        #region Private Methods________

        private void CopyToClipboard()
        {
            var itemSelected = string.Empty;
            string message;
            var status = UMessage.Message.MessageStatus.Success;

            if (IsLineDataSelectedValid())
                itemSelected = lvwLineData.SelectedItems[0].SubItems[1].Text;

            if (string.IsNullOrEmpty(itemSelected))
            {
                message = "Nothing to copy.";
                status = UMessage.Message.MessageStatus.Warning;
            }
            else
            {
                Clipboard.SetText(itemSelected);
                message = $"Copied line {lvwLineData.SelectedItems[0].Text} to clipboard";
            }
            DisplayMessage(message, status);
        }

        private bool IsLineDataSelectedValid()
        {
            return ((lvwLineData.SelectedItems.Count > 0) && (lvwLineData.SelectedItems[0].SubItems.Count > 1));
        }

        /// <summary>
        /// Sets the popup tips for given controls when mouse hovers over them.
        /// This is meant to aid in the understanding of each feature.
        /// </summary>
        private void SetToolTips()
        {
            grepperTip.SetToolTip(cbxRecursive, "Include all subfolders in search.");
            grepperTip.SetToolTip(cbxMatchPhrase, "Match exact phrase on natural boundaries.");
            grepperTip.SetToolTip(cbxMatchCase, "Match exact case of search pattern (only for literal searches).");
            grepperTip.SetToolTip(ddlFileExtensions, "Enter files to search separated by spaces. Use asterisk for all matches: *.txt *.sql somefile.cs");
            grepperTip.SetToolTip(rbLiteral, "Search for the input pattern as typed in.");
            grepperTip.SetToolTip(rbRegular, "Treat input pattern as a regular expression.");
            grepperTip.SetToolTip(lnkExtensions, "Manage extensions list.");
        }

        /// <summary>
        /// Starts a new thread to search the given folder with the given parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, EventArgs e)
        {
            ResetUI();
            SetUI_Inputs();

            // save the current user option settings
            SaveCurrentSettings();

            // start the search
            EvokeSearchThread();
        }

        private void ResetUI()
        {
            lblMessages.Text = string.Empty;
            lvwFileMatches.Items.Clear();
            lvwLineData.Items.Clear();

            progressBar.Visible = true;
            progressBar.Value = 0;
            timeCounter.Enabled = true;
        }

        private void SetUI_Inputs()
        {
            _fileController.SearchCriteria = ddlSearchCriteria.Text.Trim();
            _fileController.IsMatchCase = cbxMatchCase.Checked;
            _fileController.DoMatchPhrase = cbxMatchPhrase.Checked;
            _fileController.RecursiveSearch = cbxRecursive.Checked;
            _fileController.SetBaseSearchPath(txtBaseSearchPath.Text);
            _fileController.IsLiteralSearch = rbLiteral.Checked;
            _fileController.LoadFileExtensionsFromString(ddlFileExtensions.Text);
        }

        private void EvokeSearchThread()
        {
            // create background worker thread to perform search so that UI does not lock up
            _workerThread = new BackgroundWorker();
            _workerThread.DoWork += WorkerThread_DoWork;
            _workerThread.RunWorkerCompleted += WorkerThread_RunWorkerCompleted;

            // start the thread
            _workerThread.RunWorkerAsync(_fileController);
        }

        /// <summary>
        /// Delegate method called when worker thread is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // obtain results from worker thread and update UI as necessary
            var fc = (FileController)e.Result;
            if (fc.FileDataList == null)
                return;

            // display any errors
            DisplayErrors();
            DisplaySearchResultMessage(fc.FileDataList, fc.TotalMatches);

            foreach (var fd in fc.FileDataList)
            {
                lvwFileMatches.Items.Add(new ListViewItem(new[] { fd.FilePath, fd.LineDataList.Count.ToString() }));
            }

            // set row colors and disable progressBar & timer
            SetAlternateRowColor(lvwFileMatches);
            progressBar.Visible = false;
            timeCounter.Enabled = false;
        }

        private void DisplaySearchResultMessage(IList<FileData> results, int count)
        {
            string message = string.Empty;
            var status = UMessage.Message.MessageStatus.Success;
            if (results == null || results.Count < 1)
            {
                status = UMessage.Message.MessageStatus.Warning;
            }
            
            var matches = count == 1 ? "" : "es";
            var files = results != null && results.Count == 1 ? "" : "s";
            if (results != null) message = $"{count} match{matches} in {results.Count} file{files}";

            DisplayMessage(message, status);
        }

        private void DisplayErrors()
        {
            if (UMessage.Message.MessageList == null)
                return;

            foreach (var error in UMessage.Message.MessageList)
            {
                var item = new ListViewItem(new[] { error, "Error" })
                {
                    ForeColor = Color.Red
                };

                lvwFileMatches.Items.Add(item);
            }
        }

        private void DisplayMessage(string message, UMessage.Message.MessageStatus status)
        {
            if (status == UMessage.Message.MessageStatus.Error)
                lblMessages.ForeColor = Color.Red;
            if (status == UMessage.Message.MessageStatus.Warning)
                lblMessages.ForeColor = Color.Orange;
            if (status == UMessage.Message.MessageStatus.Success)
                lblMessages.ForeColor = Color.Green;

            lblMessages.Text = message;
            lblMessages.Visible = true;
        }

        /// <summary>
        /// Delegate method to perform FileController actions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            var fc = (FileController)e.Argument;
            fc.GenerateFileData();

            e.Result = fc;
        }

        private void SaveExtensions()
        {
            var extensionList = new List<string>();
            if (!ddlFileExtensions.Items.Contains(ddlFileExtensions.Text))
                ddlFileExtensions.Items.Add(ddlFileExtensions.Text);

            foreach (string item in ddlFileExtensions.Items)
            {
                extensionList.Add(item);
            }
            
            _settings.SaveExtensions(extensionList);
        }

        private void ManageSearchList()
        {
            if (ddlSearchCriteria.Text.Length < 1)
                return;

            var searchTerm = ddlSearchCriteria.Text;

            // rebuild ddlSearchCriteria.Items without the search term (in case it already exists), then insert search term into first place
            var itemList = new List<string>();
            foreach (string item in ddlSearchCriteria.Items)
            {
                if (item != searchTerm) itemList.Add(item);
                if (itemList.Count >= 4)
                    break;
            }
            
            ddlSearchCriteria.Items.Clear();
            itemList.Insert(0, searchTerm);
            ddlSearchCriteria.Items.AddRange(itemList.ToArray());
            _settings.SaveSearches(itemList);
        }

        private void SaveControlStates()
        {
            _appSettings.IsLiteral = rbLiteral.Checked;
            _appSettings.IsRecursive = cbxRecursive.Checked;
            _appSettings.LastExtension = ddlFileExtensions.Text;
            _appSettings.MatchCase = cbxMatchCase.Checked;
            _appSettings.MatchPhrase = cbxMatchPhrase.Checked;
            _appSettings.SearchTerm = ddlSearchCriteria.Text;

            if (!_settings.SaveSettings(_appSettings))
                DisplayMessage("Unable to save settings.", UMessage.Message.MessageStatus.Error);
        }

        /// <summary>
        /// Saves current state of settings, including file extensions to search, but not the base path
        /// since that depends on where the user right-clicks in Explorer.
        /// </summary>
        private void SaveCurrentSettings()
        {
            SaveExtensions();
            ManageSearchList();
            SaveControlStates();
        }

        /// <summary>
        /// Loads user settings from the registry.
        /// </summary>
        private void LoadSettings()
        {
            _appSettings = _settings.LoadSettings();

            ddlFileExtensions.Items.Clear();
            ddlFileExtensions.Text = _appSettings.LastExtension;
            if (_appSettings.SavedExtensions != null)
                ddlFileExtensions.Items.AddRange(_appSettings.SavedExtensions.ToArray());

            ddlSearchCriteria.Items.Clear();
            ddlSearchCriteria.Text = _appSettings.SearchTerm;
            if (_appSettings.SavedSearchTerms != null)
                ddlSearchCriteria.Items.AddRange(_appSettings.SavedSearchTerms.ToArray());

            cbxMatchCase.Checked = _appSettings.MatchCase;
            cbxMatchPhrase.Checked = _appSettings.MatchPhrase;
            cbxRecursive.Checked = _appSettings.IsRecursive;
            rbLiteral.Checked = _appSettings.IsLiteral;
            rbRegular.Checked = !rbLiteral.Checked;
            SetTitleBar();
        }

        private void SetTitleBar()
        {
            // set app version on titlebar
            Text =
                $@"GREPPER v{Assembly.GetAssembly(_fileController.GetType()).GetName().Version.Major}.{Assembly.GetAssembly(_fileController.GetType()).GetName().Version.Minor}.{Assembly.GetAssembly(_fileController.GetType()).GetName().Version.Build}";
        }

        /// <summary>
        /// Alternate white & gray to make rows easier to discern.
        /// </summary>
        /// <param name="lv"></param>
        private static void SetAlternateRowColor(ListView lv)
        {
            foreach (ListViewItem item in lv.Items)
            {
                item.BackColor = (item.Index % 2 == 0) ? Color.White : Color.Gainsboro;
            }
        }
        #endregion
    }
}