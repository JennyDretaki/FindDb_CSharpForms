namespace FindDb;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlTop;
    private Label lblTitle;
    private Label lblSubtitle;

    private Label lblSearch;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnCancel;
    private Button btnHistory;

    private GroupBox gbDatabase;
    private RadioButton rbDev;
    private RadioButton rbCtCollect;
    private RadioButton rbBoth;

    private GroupBox gbSearchType;
    private CheckBox chkTables;
    private CheckBox chkColumns;
    private CheckBox chkRecords;

    private GroupBox gbOptions;
    private Label lblSimilarity;
    private NumericUpDown numSimilarity;
    private Label lblPercent;

    private SplitContainer splitMain;

    private Panel pnlResultsHeader;
    private Label lblResults;

    private DataGridView dgvResults;

    private DataGridViewTextBoxColumn colDatabase;
    private DataGridViewTextBoxColumn colType;
    private DataGridViewTextBoxColumn colSchema;
    private DataGridViewTextBoxColumn colTable;
    private DataGridViewTextBoxColumn colColumn;
    private DataGridViewTextBoxColumn colMatch;
    private DataGridViewTextBoxColumn colScore;

    private Panel pnlPreview;
    private Panel pnlPreviewHeader;
    private Label lblPreviewTitle;
    private Label lblSelectedObject;

    private FlowLayoutPanel pnlPreviewButtons;
    private Button btnCopySql;
    private Button btnPreviewData;

    private RichTextBox txtPreview;

    private Panel pnlStatus;
    private Label lblStatus;
    private ProgressBar progressBar;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlTop = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        lblSearch = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnCancel = new Button();
        btnHistory = new Button();
        gbDatabase = new GroupBox();
        rbDev = new RadioButton();
        rbCtCollect = new RadioButton();
        rbBoth = new RadioButton();
        gbSearchType = new GroupBox();
        chkTables = new CheckBox();
        chkColumns = new CheckBox();
        chkRecords = new CheckBox();
        gbOptions = new GroupBox();
        lblSimilarity = new Label();
        numSimilarity = new NumericUpDown();
        lblPercent = new Label();
        splitMain = new SplitContainer();
        dgvResults = new DataGridView();
        pnlResultsHeader = new Panel();
        lblResults = new Label();
        pnlPreview = new Panel();
        txtPreview = new RichTextBox();
        pnlPreviewButtons = new FlowLayoutPanel();
        btnCopySql = new Button();
        btnPreviewData = new Button();
        pnlPreviewHeader = new Panel();
        lblPreviewTitle = new Label();
        lblSelectedObject = new Label();
        pnlStatus = new Panel();
        lblStatus = new Label();
        progressBar = new ProgressBar();
        pnlTop.SuspendLayout();
        gbDatabase.SuspendLayout();
        gbSearchType.SuspendLayout();
        gbOptions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numSimilarity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
        pnlResultsHeader.SuspendLayout();
        pnlPreview.SuspendLayout();
        pnlPreviewButtons.SuspendLayout();
        pnlPreviewHeader.SuspendLayout();
        pnlStatus.SuspendLayout();
        SuspendLayout();
        // 
        // pnlTop
        // 
        pnlTop.BackColor = SystemColors.ControlLightLight;
        pnlTop.Controls.Add(lblTitle);
        pnlTop.Controls.Add(lblSubtitle);
        pnlTop.Controls.Add(lblSearch);
        pnlTop.Controls.Add(txtSearch);
        pnlTop.Controls.Add(btnSearch);
        pnlTop.Controls.Add(btnCancel);
        pnlTop.Controls.Add(btnHistory);
        pnlTop.Controls.Add(gbDatabase);
        pnlTop.Controls.Add(gbSearchType);
        pnlTop.Controls.Add(gbOptions);
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Location = new Point(0, 0);
        pnlTop.Name = "pnlTop";
        pnlTop.Padding = new Padding(20);
        pnlTop.Size = new Size(1500, 205);
        pnlTop.TabIndex = 2;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Location = new Point(20, 14);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(201, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Database Search";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.ForeColor = SystemColors.GrayText;
        lblSubtitle.Location = new Point(22, 50);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(398, 19);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Search tables, columns and records across DEV and CTCOLLECT";
        // 
        // lblSearch
        // 
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblSearch.Location = new Point(22, 84);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(75, 19);
        lblSearch.TabIndex = 2;
        lblSearch.Text = "Keywords";
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(110, 80);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "e.g. payment code, customer, invoice...";
        txtSearch.Size = new Size(550, 25);
        txtSearch.TabIndex = 3;
        txtSearch.KeyDown += TxtSearch_KeyDown;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(675, 77);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(120, 36);
        btnSearch.TabIndex = 4;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += BtnSearch_Click;
        // 
        // btnCancel
        // 
        btnCancel.Enabled = false;
        btnCancel.Location = new Point(805, 77);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 36);
        btnCancel.TabIndex = 5;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += BtnCancel_Click;
        // 
        // btnHistory
        // 
        btnHistory.Location = new Point(915, 77);
        btnHistory.Name = "btnHistory";
        btnHistory.Size = new Size(110, 36);
        btnHistory.TabIndex = 6;
        btnHistory.Text = "History";
        btnHistory.UseVisualStyleBackColor = true;
        btnHistory.Click += BtnHistory_Click;
        // 
        // gbDatabase
        // 
        gbDatabase.Controls.Add(rbDev);
        gbDatabase.Controls.Add(rbCtCollect);
        gbDatabase.Controls.Add(rbBoth);
        gbDatabase.Location = new Point(20, 125);
        gbDatabase.Name = "gbDatabase";
        gbDatabase.Size = new Size(330, 65);
        gbDatabase.TabIndex = 7;
        gbDatabase.TabStop = false;
        gbDatabase.Text = "Database";
        // 
        // rbDev
        // 
        rbDev.AutoSize = true;
        rbDev.Location = new Point(20, 29);
        rbDev.Name = "rbDev";
        rbDev.Size = new Size(53, 23);
        rbDev.TabIndex = 0;
        rbDev.Text = "DEV";
        // 
        // rbCtCollect
        // 
        rbCtCollect.AutoSize = true;
        rbCtCollect.Location = new Point(100, 29);
        rbCtCollect.Name = "rbCtCollect";
        rbCtCollect.Size = new Size(99, 23);
        rbCtCollect.TabIndex = 1;
        rbCtCollect.Text = "CTCOLLECT";
        // 
        // rbBoth
        // 
        rbBoth.AutoSize = true;
        rbBoth.Checked = true;
        rbBoth.Location = new Point(225, 29);
        rbBoth.Name = "rbBoth";
        rbBoth.Size = new Size(62, 23);
        rbBoth.TabIndex = 2;
        rbBoth.TabStop = true;
        rbBoth.Text = "BOTH";
        // 
        // gbSearchType
        // 
        gbSearchType.Controls.Add(chkTables);
        gbSearchType.Controls.Add(chkColumns);
        gbSearchType.Controls.Add(chkRecords);
        gbSearchType.Location = new Point(365, 125);
        gbSearchType.Name = "gbSearchType";
        gbSearchType.Size = new Size(380, 65);
        gbSearchType.TabIndex = 8;
        gbSearchType.TabStop = false;
        gbSearchType.Text = "Search in";
        // 
        // chkTables
        // 
        chkTables.AutoSize = true;
        chkTables.Checked = true;
        chkTables.CheckState = CheckState.Checked;
        chkTables.Location = new Point(20, 29);
        chkTables.Name = "chkTables";
        chkTables.Size = new Size(64, 23);
        chkTables.TabIndex = 0;
        chkTables.Text = "Tables";
        // 
        // chkColumns
        // 
        chkColumns.AutoSize = true;
        chkColumns.Checked = true;
        chkColumns.CheckState = CheckState.Checked;
        chkColumns.Location = new Point(120, 29);
        chkColumns.Name = "chkColumns";
        chkColumns.Size = new Size(82, 23);
        chkColumns.TabIndex = 1;
        chkColumns.Text = "Columns";
        // 
        // chkRecords
        // 
        chkRecords.AutoSize = true;
        chkRecords.Location = new Point(230, 29);
        chkRecords.Name = "chkRecords";
        chkRecords.Size = new Size(76, 23);
        chkRecords.TabIndex = 2;
        chkRecords.Text = "Records";
        // 
        // gbOptions
        // 
        gbOptions.Controls.Add(lblSimilarity);
        gbOptions.Controls.Add(numSimilarity);
        gbOptions.Controls.Add(lblPercent);
        gbOptions.Location = new Point(760, 125);
        gbOptions.Name = "gbOptions";
        gbOptions.Size = new Size(300, 65);
        gbOptions.TabIndex = 9;
        gbOptions.TabStop = false;
        gbOptions.Text = "Options";
        // 
        // lblSimilarity
        // 
        lblSimilarity.AutoSize = true;
        lblSimilarity.Location = new Point(18, 30);
        lblSimilarity.Name = "lblSimilarity";
        lblSimilarity.Size = new Size(129, 19);
        lblSimilarity.TabIndex = 0;
        lblSimilarity.Text = "Minimum similarity:";
        // 
        // numSimilarity
        // 
        numSimilarity.Location = new Point(160, 25);
        numSimilarity.Name = "numSimilarity";
        numSimilarity.Size = new Size(70, 25);
        numSimilarity.TabIndex = 1;
        numSimilarity.Value = new decimal(new int[] { 45, 0, 0, 0 });
        // 
        // lblPercent
        // 
        lblPercent.AutoSize = true;
        lblPercent.Location = new Point(237, 30);
        lblPercent.Name = "lblPercent";
        lblPercent.Size = new Size(20, 19);
        lblPercent.TabIndex = 2;
        lblPercent.Text = "%";
        // 
        // splitMain
        // 
        splitMain.Dock = DockStyle.Fill;
        splitMain.Location = new Point(0, 205);
        splitMain.Name = "splitMain";
        // 
        // splitMain.Panel1
        // 
        splitMain.Panel1.Controls.Add(dgvResults);
        splitMain.Panel1.Controls.Add(pnlResultsHeader);
        splitMain.Panel1MinSize = 550;
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(pnlPreview);
        splitMain.Panel2MinSize = 300;
        splitMain.Size = new Size(1500, 650);
        splitMain.SplitterDistance = 1196;
        splitMain.TabIndex = 0;
        // 
        // dgvResults
        // 
        dgvResults.AllowUserToAddRows = false;
        dgvResults.AllowUserToDeleteRows = false;
        dgvResults.AllowUserToResizeRows = false;
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvResults.BackgroundColor = SystemColors.Window;
        dgvResults.BorderStyle = BorderStyle.None;
        dgvResults.ColumnHeadersHeight = 38;
        dgvResults.Dock = DockStyle.Fill;
        dgvResults.Location = new Point(0, 45);
        dgvResults.MultiSelect = false;
        dgvResults.Name = "dgvResults";
        dgvResults.ReadOnly = true;
        dgvResults.RowHeadersVisible = false;
        dgvResults.RowTemplate.Height = 32;
        dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvResults.Size = new Size(1196, 605);
        dgvResults.TabIndex = 0;
        dgvResults.CellDoubleClick += DgvResults_CellDoubleClick;
        dgvResults.SelectionChanged += DgvResults_SelectionChanged;
        // 
        // pnlResultsHeader
        // 
        pnlResultsHeader.Controls.Add(lblResults);
        pnlResultsHeader.Dock = DockStyle.Top;
        pnlResultsHeader.Location = new Point(0, 0);
        pnlResultsHeader.Name = "pnlResultsHeader";
        pnlResultsHeader.Padding = new Padding(12, 10, 0, 0);
        pnlResultsHeader.Size = new Size(1196, 45);
        pnlResultsHeader.TabIndex = 1;
        // 
        // lblResults
        // 
        lblResults.AutoSize = true;
        lblResults.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblResults.Location = new Point(0, 0);
        lblResults.Name = "lblResults";
        lblResults.Size = new Size(110, 20);
        lblResults.TabIndex = 0;
        lblResults.Text = "Search Results";
        // 
        // pnlPreview
        // 
        pnlPreview.Controls.Add(txtPreview);
        pnlPreview.Controls.Add(pnlPreviewButtons);
        pnlPreview.Controls.Add(pnlPreviewHeader);
        pnlPreview.Dock = DockStyle.Fill;
        pnlPreview.Location = new Point(0, 0);
        pnlPreview.Name = "pnlPreview";
        pnlPreview.Size = new Size(300, 650);
        pnlPreview.TabIndex = 0;
        // 
        // txtPreview
        // 
        txtPreview.BackColor = SystemColors.Window;
        txtPreview.BorderStyle = BorderStyle.None;
        txtPreview.Dock = DockStyle.Fill;
        txtPreview.Font = new Font("Consolas", 10F);
        txtPreview.Location = new Point(0, 125);
        txtPreview.Name = "txtPreview";
        txtPreview.ReadOnly = true;
        txtPreview.Size = new Size(300, 525);
        txtPreview.TabIndex = 0;
        txtPreview.Text = "";
        txtPreview.WordWrap = false;
        // 
        // pnlPreviewButtons
        // 
        pnlPreviewButtons.Controls.Add(btnCopySql);
        pnlPreviewButtons.Controls.Add(btnPreviewData);
        pnlPreviewButtons.Dock = DockStyle.Top;
        pnlPreviewButtons.Location = new Point(0, 70);
        pnlPreviewButtons.Name = "pnlPreviewButtons";
        pnlPreviewButtons.Padding = new Padding(10, 8, 0, 8);
        pnlPreviewButtons.Size = new Size(300, 55);
        pnlPreviewButtons.TabIndex = 1;
        // 
        // btnCopySql
        // 
        btnCopySql.Location = new Point(13, 11);
        btnCopySql.Name = "btnCopySql";
        btnCopySql.Size = new Size(110, 35);
        btnCopySql.TabIndex = 0;
        btnCopySql.Text = "Copy SQL";
        btnCopySql.UseVisualStyleBackColor = true;
        btnCopySql.Click += BtnCopySql_Click;
        // 
        // btnPreviewData
        // 
        btnPreviewData.Location = new Point(129, 11);
        btnPreviewData.Name = "btnPreviewData";
        btnPreviewData.Size = new Size(130, 35);
        btnPreviewData.TabIndex = 1;
        btnPreviewData.Text = "Preview Data";
        btnPreviewData.UseVisualStyleBackColor = true;
        btnPreviewData.Click += BtnPreviewData_Click;
        // 
        // pnlPreviewHeader
        // 
        pnlPreviewHeader.Controls.Add(lblPreviewTitle);
        pnlPreviewHeader.Controls.Add(lblSelectedObject);
        pnlPreviewHeader.Dock = DockStyle.Top;
        pnlPreviewHeader.Location = new Point(0, 0);
        pnlPreviewHeader.Name = "pnlPreviewHeader";
        pnlPreviewHeader.Padding = new Padding(12, 10, 10, 5);
        pnlPreviewHeader.Size = new Size(300, 70);
        pnlPreviewHeader.TabIndex = 2;
        // 
        // lblPreviewTitle
        // 
        lblPreviewTitle.AutoSize = true;
        lblPreviewTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblPreviewTitle.Location = new Point(12, 9);
        lblPreviewTitle.Name = "lblPreviewTitle";
        lblPreviewTitle.Size = new Size(95, 20);
        lblPreviewTitle.TabIndex = 0;
        lblPreviewTitle.Text = "SQL Preview";
        // 
        // lblSelectedObject
        // 
        lblSelectedObject.AutoSize = true;
        lblSelectedObject.ForeColor = SystemColors.GrayText;
        lblSelectedObject.Location = new Point(12, 39);
        lblSelectedObject.Name = "lblSelectedObject";
        lblSelectedObject.Size = new Size(93, 19);
        lblSelectedObject.TabIndex = 1;
        lblSelectedObject.Text = "Select a result";
        // 
        // pnlStatus
        // 
        pnlStatus.Controls.Add(lblStatus);
        pnlStatus.Controls.Add(progressBar);
        pnlStatus.Dock = DockStyle.Bottom;
        pnlStatus.Location = new Point(0, 855);
        pnlStatus.Name = "pnlStatus";
        pnlStatus.Padding = new Padding(12, 7, 12, 7);
        pnlStatus.Size = new Size(1500, 45);
        pnlStatus.TabIndex = 1;
        // 
        // lblStatus
        // 
        lblStatus.Dock = DockStyle.Fill;
        lblStatus.Location = new Point(12, 7);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(1236, 31);
        lblStatus.TabIndex = 0;
        lblStatus.Text = "Ready";
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // progressBar
        // 
        progressBar.Dock = DockStyle.Right;
        progressBar.Location = new Point(1248, 7);
        progressBar.MarqueeAnimationSpeed = 25;
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(240, 31);
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.TabIndex = 1;
        progressBar.Visible = false;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1500, 900);
        Controls.Add(splitMain);
        Controls.Add(pnlStatus);
        Controls.Add(pnlTop);
        Font = new Font("Segoe UI", 10F);
        MinimumSize = new Size(1100, 700);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Database Search Tool";
        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        gbDatabase.ResumeLayout(false);
        gbDatabase.PerformLayout();
        gbSearchType.ResumeLayout(false);
        gbSearchType.PerformLayout();
        gbOptions.ResumeLayout(false);
        gbOptions.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numSimilarity).EndInit();
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlPreview.ResumeLayout(false);
        pnlPreviewButtons.ResumeLayout(false);
        pnlPreviewHeader.ResumeLayout(false);
        pnlPreviewHeader.PerformLayout();
        pnlStatus.ResumeLayout(false);
        ResumeLayout(false);
    }
}