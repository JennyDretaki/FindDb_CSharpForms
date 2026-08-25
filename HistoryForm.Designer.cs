namespace FindDb;

partial class HistoryForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlHeader;
    private Label lblTitle;
    private Label lblDescription;
    private Label lblHistoryCount;

    private DataGridView dgvHistory;

    private DataGridViewTextBoxColumn colDate;
    private DataGridViewTextBoxColumn colSearch;
    private DataGridViewTextBoxColumn colDatabase;
    private DataGridViewTextBoxColumn colSearchIn;

    private Panel pnlBottom;

    private Button btnSearchAgain;
    private Button btnDelete;
    private Button btnClear;
    private Button btnClose;

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
        components =
            new System.ComponentModel.Container();

        pnlHeader =
            new Panel();

        lblTitle =
            new Label();

        lblDescription =
            new Label();

        lblHistoryCount =
            new Label();

        dgvHistory =
            new DataGridView();

        colDate =
            new DataGridViewTextBoxColumn();

        colSearch =
            new DataGridViewTextBoxColumn();

        colDatabase =
            new DataGridViewTextBoxColumn();

        colSearchIn =
            new DataGridViewTextBoxColumn();

        pnlBottom =
            new Panel();

        btnSearchAgain =
            new Button();

        btnDelete =
            new Button();

        btnClear =
            new Button();

        btnClose =
            new Button();

        pnlHeader.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)dgvHistory)
            .BeginInit();

        pnlBottom.SuspendLayout();

        SuspendLayout();

        // =====================================================
        // FORM
        // =====================================================

        AutoScaleDimensions =
            new SizeF(7F, 15F);

        AutoScaleMode =
            AutoScaleMode.Font;

        ClientSize =
            new Size(1000, 650);

        MinimumSize =
            new Size(800, 500);

        StartPosition =
            FormStartPosition.CenterParent;

        Text =
            "Search History";

        Font =
            new Font(
                "Segoe UI",
                10F);

        // =====================================================
        // HEADER PANEL
        // =====================================================

        pnlHeader.Dock =
            DockStyle.Top;

        pnlHeader.Height =
            95;

        pnlHeader.Padding =
            new Padding(20);

        // =====================================================
        // TITLE
        // =====================================================

        lblTitle.AutoSize =
            true;

        lblTitle.Location =
            new Point(20, 14);

        lblTitle.Font =
            new Font(
                "Segoe UI",
                16F,
                FontStyle.Bold);

        lblTitle.Text =
            "Search History";

        // =====================================================
        // DESCRIPTION
        // =====================================================

        lblDescription.AutoSize =
            true;

        lblDescription.Location =
            new Point(22, 52);

        lblDescription.ForeColor =
            SystemColors.GrayText;

        lblDescription.Text =
            "Double-click an item to run the search again.";

        // =====================================================
        // COUNT
        // =====================================================

        lblHistoryCount.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        lblHistoryCount.Location =
            new Point(800, 20);

        lblHistoryCount.Size =
            new Size(170, 30);

        lblHistoryCount.Text =
            "0 searches";

        lblHistoryCount.TextAlign =
            ContentAlignment.MiddleRight;

        pnlHeader.Controls.Add(
            lblTitle);

        pnlHeader.Controls.Add(
            lblDescription);

        pnlHeader.Controls.Add(
            lblHistoryCount);

        // =====================================================
        // HISTORY GRID
        // =====================================================

        dgvHistory.AllowUserToAddRows =
            false;

        dgvHistory.AllowUserToDeleteRows =
            false;

        dgvHistory.AllowUserToResizeRows =
            false;

        dgvHistory.AutoGenerateColumns =
            false;

        dgvHistory.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

        dgvHistory.BackgroundColor =
            SystemColors.Window;

        dgvHistory.BorderStyle =
            BorderStyle.None;

        dgvHistory.ColumnHeadersHeight =
            38;

        dgvHistory.Dock =
            DockStyle.Fill;

        dgvHistory.MultiSelect =
            false;

        dgvHistory.ReadOnly =
            true;

        dgvHistory.RowHeadersVisible =
            false;

        dgvHistory.RowTemplate.Height =
            34;

        dgvHistory.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        dgvHistory.CellDoubleClick +=
            DgvHistory_CellDoubleClick;

        // =====================================================
        // DATE COLUMN
        // =====================================================

        colDate.Name =
            "colDate";

        colDate.HeaderText =
            "Date";

        colDate.DataPropertyName =
            "Date";

        colDate.FillWeight =
            25;

        colDate.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                Format =
                    "dd/MM/yyyy HH:mm:ss"
            };

        // =====================================================
        // SEARCH COLUMN
        // =====================================================

        colSearch.Name =
            "colSearch";

        colSearch.HeaderText =
            "Search";

        colSearch.DataPropertyName =
            "SearchText";

        colSearch.FillWeight =
            40;

        // =====================================================
        // DATABASE
        // =====================================================

        colDatabase.Name =
            "colDatabase";

        colDatabase.HeaderText =
            "Database";

        colDatabase.DataPropertyName =
            "Database";

        colDatabase.FillWeight =
            20;

        // =====================================================
        // SEARCH IN
        // =====================================================

        colSearchIn.Name =
            "colSearchIn";

        colSearchIn.HeaderText =
            "Search In";

        colSearchIn.DataPropertyName =
            "SearchIn";

        colSearchIn.FillWeight =
            30;

        dgvHistory.Columns.AddRange(
            colDate,
            colSearch,
            colDatabase,
            colSearchIn);

        // =====================================================
        // BOTTOM PANEL
        // =====================================================

        pnlBottom.Dock =
            DockStyle.Bottom;

        pnlBottom.Height =
            70;

        pnlBottom.Padding =
            new Padding(15);

        // =====================================================
        // SEARCH AGAIN
        // =====================================================

        btnSearchAgain.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right;

        btnSearchAgain.Location =
            new Point(575, 17);

        btnSearchAgain.Size =
            new Size(130, 38);

        btnSearchAgain.Text =
            "Search Again";

        btnSearchAgain.UseVisualStyleBackColor =
            true;

        btnSearchAgain.Click +=
            BtnSearchAgain_Click;

        // =====================================================
        // DELETE
        // =====================================================

        btnDelete.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right;

        btnDelete.Location =
            new Point(715, 17);

        btnDelete.Size =
            new Size(85, 38);

        btnDelete.Text =
            "Delete";

        btnDelete.UseVisualStyleBackColor =
            true;

        btnDelete.Click +=
            BtnDelete_Click;

        // =====================================================
        // CLEAR
        // =====================================================

        btnClear.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right;

        btnClear.Location =
            new Point(810, 17);

        btnClear.Size =
            new Size(105, 38);

        btnClear.Text =
            "Clear All";

        btnClear.UseVisualStyleBackColor =
            true;

        btnClear.Click +=
            BtnClear_Click;

        // =====================================================
        // CLOSE
        // =====================================================

        btnClose.Location =
            new Point(15, 17);

        btnClose.Size =
            new Size(90, 38);

        btnClose.Text =
            "Close";

        btnClose.UseVisualStyleBackColor =
            true;

        btnClose.Click +=
            BtnClose_Click;

        pnlBottom.Controls.Add(
            btnSearchAgain);

        pnlBottom.Controls.Add(
            btnDelete);

        pnlBottom.Controls.Add(
            btnClear);

        pnlBottom.Controls.Add(
            btnClose);

        // =====================================================
        // ADD CONTROLS
        // =====================================================

        Controls.Add(
            dgvHistory);

        Controls.Add(
            pnlBottom);

        Controls.Add(
            pnlHeader);

        // =====================================================
        // END INIT
        // =====================================================

        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();

        ((System.ComponentModel.ISupportInitialize)dgvHistory)
            .EndInit();

        pnlBottom.ResumeLayout(false);

        ResumeLayout(false);
    }
}