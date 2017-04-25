namespace GrepperView
{
	partial class MainUI
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.lblSearchCriteria = new System.Windows.Forms.Label();
            this.cbxRecursive = new System.Windows.Forms.CheckBox();
            this.cbxMatchCase = new System.Windows.Forms.CheckBox();
            this.cbxMatchPhrase = new System.Windows.Forms.CheckBox();
            this.rbLiteral = new System.Windows.Forms.RadioButton();
            this.rbRegular = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBaseSearchPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessages = new System.Windows.Forms.Label();
            this.lvwFileMatches = new System.Windows.Forms.ListView();
            this.colFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMatchCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwLineData = new System.Windows.Forms.ListView();
            this.lineNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lineData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timeCounter = new System.Windows.Forms.Timer(this.components);
            this.grepperTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ddlFileExtensions = new System.Windows.Forms.ComboBox();
            this.lnkExtensions = new System.Windows.Forms.LinkLabel();
            this.ddlSearchCriteria = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearchCriteria
            // 
            this.lblSearchCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchCriteria.AutoSize = true;
            this.lblSearchCriteria.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchCriteria.Location = new System.Drawing.Point(58, 11);
            this.lblSearchCriteria.Name = "lblSearchCriteria";
            this.lblSearchCriteria.Size = new System.Drawing.Size(79, 13);
            this.lblSearchCriteria.TabIndex = 0;
            this.lblSearchCriteria.Text = "Search Criteria:";
            // 
            // cbxRecursive
            // 
            this.cbxRecursive.AutoSize = true;
            this.cbxRecursive.Location = new System.Drawing.Point(3, 49);
            this.cbxRecursive.Name = "cbxRecursive";
            this.cbxRecursive.Size = new System.Drawing.Size(111, 17);
            this.cbxRecursive.TabIndex = 6;
            this.cbxRecursive.Text = "Recursive Search";
            this.cbxRecursive.UseVisualStyleBackColor = true;
            // 
            // cbxMatchCase
            // 
            this.cbxMatchCase.AutoSize = true;
            this.cbxMatchCase.Location = new System.Drawing.Point(3, 3);
            this.cbxMatchCase.Name = "cbxMatchCase";
            this.cbxMatchCase.Size = new System.Drawing.Size(83, 17);
            this.cbxMatchCase.TabIndex = 4;
            this.cbxMatchCase.Text = "Match Case";
            this.cbxMatchCase.UseVisualStyleBackColor = true;
            // 
            // cbxMatchPhrase
            // 
            this.cbxMatchPhrase.AutoSize = true;
            this.cbxMatchPhrase.Location = new System.Drawing.Point(3, 26);
            this.cbxMatchPhrase.Name = "cbxMatchPhrase";
            this.cbxMatchPhrase.Size = new System.Drawing.Size(92, 17);
            this.cbxMatchPhrase.TabIndex = 5;
            this.cbxMatchPhrase.Text = "Match Phrase";
            this.cbxMatchPhrase.UseVisualStyleBackColor = true;
            // 
            // rbLiteral
            // 
            this.rbLiteral.AutoSize = true;
            this.rbLiteral.Checked = true;
            this.rbLiteral.Location = new System.Drawing.Point(123, 2);
            this.rbLiteral.Name = "rbLiteral";
            this.rbLiteral.Size = new System.Drawing.Size(90, 17);
            this.rbLiteral.TabIndex = 7;
            this.rbLiteral.TabStop = true;
            this.rbLiteral.Text = "Literal Search";
            this.rbLiteral.UseVisualStyleBackColor = true;
            // 
            // rbRegular
            // 
            this.rbRegular.AutoSize = true;
            this.rbRegular.Location = new System.Drawing.Point(219, 3);
            this.rbRegular.Name = "rbRegular";
            this.rbRegular.Size = new System.Drawing.Size(116, 17);
            this.rbRegular.TabIndex = 8;
            this.rbRegular.Text = "Regular Expression";
            this.rbRegular.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Location = new System.Drawing.Point(3, 72);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.Search_Click);
            // 
            // txtBaseSearchPath
            // 
            this.txtBaseSearchPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaseSearchPath.Location = new System.Drawing.Point(143, 37);
            this.txtBaseSearchPath.Name = "txtBaseSearchPath";
            this.txtBaseSearchPath.Size = new System.Drawing.Size(451, 20);
            this.txtBaseSearchPath.TabIndex = 2;
            this.txtBaseSearchPath.Click += new System.EventHandler(this.BaseSearchPath_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(41, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Base Search Path:";
            // 
            // lblMessages
            // 
            this.lblMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessages.Location = new System.Drawing.Point(120, 71);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(261, 23);
            this.lblMessages.TabIndex = 13;
            this.lblMessages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvwFileMatches
            // 
            this.lvwFileMatches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFilePath,
            this.colMatchCount});
            this.lvwFileMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwFileMatches.FullRowSelect = true;
            this.lvwFileMatches.Location = new System.Drawing.Point(0, 0);
            this.lvwFileMatches.Name = "lvwFileMatches";
            this.lvwFileMatches.Size = new System.Drawing.Size(1008, 238);
            this.lvwFileMatches.TabIndex = 10;
            this.lvwFileMatches.UseCompatibleStateImageBehavior = false;
            this.lvwFileMatches.View = System.Windows.Forms.View.Details;
            this.lvwFileMatches.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileMatches_MouseClick);
            this.lvwFileMatches.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileMatches_MouseDoubleClick);
            // 
            // colFilePath
            // 
            this.colFilePath.Text = "File Path";
            this.colFilePath.Width = 918;
            // 
            // colMatchCount
            // 
            this.colMatchCount.Text = "Matches";
            this.colMatchCount.Width = 72;
            // 
            // lvwLineData
            // 
            this.lvwLineData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lineNumber,
            this.lineData});
            this.lvwLineData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwLineData.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lvwLineData.FullRowSelect = true;
            this.lvwLineData.Location = new System.Drawing.Point(0, 0);
            this.lvwLineData.Name = "lvwLineData";
            this.lvwLineData.Size = new System.Drawing.Size(1008, 235);
            this.lvwLineData.TabIndex = 11;
            this.lvwLineData.UseCompatibleStateImageBehavior = false;
            this.lvwLineData.View = System.Windows.Forms.View.Details;
            this.lvwLineData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LineData_MouseClick);
            // 
            // lineNumber
            // 
            this.lineNumber.Text = "Line Number";
            this.lineNumber.Width = 100;
            // 
            // lineData
            // 
            this.lineData.Text = "Data";
            this.lineData.Width = 891;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(120, 43);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(264, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 18;
            this.progressBar.Visible = false;
            // 
            // timeCounter
            // 
            this.timeCounter.Interval = 500;
            this.timeCounter.Tick += new System.EventHandler(this.TimeCounter_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cbxMatchCase);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.cbxRecursive);
            this.panel1.Controls.Add(this.lblMessages);
            this.panel1.Controls.Add(this.cbxMatchPhrase);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.rbLiteral);
            this.panel1.Controls.Add(this.rbRegular);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 95);
            this.panel1.TabIndex = 19;
            // 
            // ddlFileExtensions
            // 
            this.ddlFileExtensions.AllowDrop = true;
            this.ddlFileExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlFileExtensions.FormattingEnabled = true;
            this.ddlFileExtensions.Location = new System.Drawing.Point(143, 68);
            this.ddlFileExtensions.Name = "ddlFileExtensions";
            this.ddlFileExtensions.Size = new System.Drawing.Size(451, 21);
            this.ddlFileExtensions.TabIndex = 3;
            // 
            // lnkExtensions
            // 
            this.lnkExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkExtensions.AutoSize = true;
            this.lnkExtensions.BackColor = System.Drawing.Color.Transparent;
            this.lnkExtensions.Location = new System.Drawing.Point(8, 71);
            this.lnkExtensions.Name = "lnkExtensions";
            this.lnkExtensions.Size = new System.Drawing.Size(129, 13);
            this.lnkExtensions.TabIndex = 21;
            this.lnkExtensions.TabStop = true;
            this.lnkExtensions.Text = "File Extensions to Search:";
            this.lnkExtensions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Extensions_LinkClicked);
            // 
            // ddlSearchCriteria
            // 
            this.ddlSearchCriteria.AllowDrop = true;
            this.ddlSearchCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlSearchCriteria.FormattingEnabled = true;
            this.ddlSearchCriteria.Location = new System.Drawing.Point(143, 7);
            this.ddlSearchCriteria.Name = "ddlSearchCriteria";
            this.ddlSearchCriteria.Size = new System.Drawing.Size(451, 21);
            this.ddlSearchCriteria.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(5, 103);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvwFileMatches);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvwLineData);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 477);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.ddlSearchCriteria);
            this.panel2.Controls.Add(this.txtBaseSearchPath);
            this.panel2.Controls.Add(this.lnkExtensions);
            this.panel2.Controls.Add(this.ddlFileExtensions);
            this.panel2.Controls.Add(this.lblSearchCriteria);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(413, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 95);
            this.panel2.TabIndex = 23;
            // 
            // MainUI
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(1017, 580);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(988, 600);
            this.Name = "MainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grepper";
            this.TransparencyKey = System.Drawing.SystemColors.InactiveBorder;
            this.Resize += new System.EventHandler(this.MainUI_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Label lblSearchCriteria;
		private System.Windows.Forms.CheckBox cbxRecursive;
		private System.Windows.Forms.CheckBox cbxMatchCase;
		private System.Windows.Forms.CheckBox cbxMatchPhrase;
		private System.Windows.Forms.RadioButton rbLiteral;
        private System.Windows.Forms.RadioButton rbRegular;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.TextBox txtBaseSearchPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblMessages;
		private System.Windows.Forms.ListView lvwFileMatches;
		private System.Windows.Forms.ColumnHeader colFilePath;
		private System.Windows.Forms.ColumnHeader colMatchCount;
		private System.Windows.Forms.ListView lvwLineData;
		private System.Windows.Forms.ColumnHeader lineNumber;
		private System.Windows.Forms.ColumnHeader lineData;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Timer timeCounter;
		private System.Windows.Forms.ToolTip grepperTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ddlFileExtensions;
        private System.Windows.Forms.LinkLabel lnkExtensions;
        private System.Windows.Forms.ComboBox ddlSearchCriteria;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
    }
}

