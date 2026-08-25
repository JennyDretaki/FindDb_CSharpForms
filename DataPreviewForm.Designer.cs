namespace FindDb;

partial class DataPreviewForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlHeader;

    private Label lblTitle;
    private Label lblTableName;
    private Label lblRowCount;

    private DataGridView dgvData;

    private Panel pnlBottom;

    private Label lblStatus;

    private Button btnCopyCell;
    private Button btnCopyRow;
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

        lblTableName =
            new Label();

        lblRowCount =
            new Label();

        dgvData =
            new DataGridView();

        pnlBottom =
            new Panel();

        lblStatus =
            new Label();

        btnCopyCell =
            new Button();

        btnCopyRow =
            new Button();

        btnClose =
            new Button();

        pnlHeader.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)dgvData)
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
            new Size(1300, 750);

        MinimumSize =
            new Size(900, 550);

        StartPosition =
            FormStartPosition.CenterParent;

        Text =
            "Data Preview";

        Font =
            new Font(
                "Segoe UI",
                9.5F);

        // =====================================================
        // HEADER
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
            new Point(20, 12);

        lblTitle.Font =
            new Font(
                "Segoe UI",
                16F,
                FontStyle.Bold);

        lblTitle.Text =
            "Data Preview";

        // =====================================================
        // TABLE NAME
        // =====================================================

        lblTableName.AutoSize =
            true;

        lblTableName.Location =
            new Point(22, 51);

        lblTableName.Font =
            new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

        lblTableName.Text =
            "Database → dbo.Table";

        // =====================================================
        // ROW COUNT
        // =====================================================

        lblRowCount.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        lblRowCount.Location =
            new Point(1080, 45);

        lblRowCount.Size =
            new Size(190, 30);

        lblRowCount.TextAlign =
            ContentAlignment.MiddleRight;

        lblRowCount.ForeColor =
            SystemColors.GrayText;

        lblRowCount.Text =
            "0 rows loaded";

        pnlHeader.Controls.Add(
            lblTitle);

        pnlHeader.Controls.Add(
            lblTableName);

        pnlHeader.Controls.Add(
            lblRowCount);

        // =====================================================
        // DATA GRID
        // =====================================================

        dgvData.AllowUserToAddRows =
            false;

        dgvData.AllowUserToDeleteRows =
            false;

        dgvData.AllowUserToOrderColumns =
            true;

        dgvData.AllowUserToResizeRows =
            false;

        dgvData.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.DisplayedCells;

        dgvData.BackgroundColor =
            SystemColors.Window;

        dgvData.BorderStyle =
            BorderStyle.None;

        dgvData.ColumnHeadersHeight =
            38;

        dgvData.Dock =
            DockStyle.Fill;

        dgvData.ReadOnly =
            true;

        dgvData.RowHeadersVisible =
            false;

        dgvData.RowTemplate.Height =
            32;

        dgvData.SelectionMode =
            DataGridViewSelectionMode.CellSelect;

        dgvData.ClipboardCopyMode =
            DataGridViewClipboardCopyMode
                .EnableAlwaysIncludeHeaderText;

        // =====================================================
        // BOTTOM
        // =====================================================

        pnlBottom.Dock =
            DockStyle.Bottom;

        pnlBottom.Height =
            65;

        pnlBottom.Padding =
            new Padding(15);

        // =====================================================
        // STATUS
        // =====================================================

        lblStatus.Dock =
            DockStyle.Left;

        lblStatus.Width =
            400;

        lblStatus.Text =
            "Ready";

        lblStatus.TextAlign =
            ContentAlignment.MiddleLeft;

        // =====================================================
        // COPY CELL
        // =====================================================

        btnCopyCell.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right;

        btnCopyCell.Location =
            new Point(935, 14);

        btnCopyCell.Size =
            new Size(105, 38);

        btnCopyCell.Text =
            "Copy Cell";

        btnCopyCell.UseVisualStyleBackColor =
            true;

        btnCopyCell.Click +=
            BtnCopyCell_Click;

        // =====================================================
        // COPY ROW
        // =====================================================

        btnCopyRow.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right;

        btnCopyRow.Location =
            new Point(1050, 14);

        btnCopyRow.Size =
            new Size(105, 38);

        btnCopyRow.Text =
            "Copy Row";

        btnCopyRow.UseVisualStyleBackColor =
            true;

        btnCopyRow.Click +=
            BtnCopyRow_Click;

        // =====================================================
        // CLOSE
        // =====================================================

        btnClose.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right;

        btnClose.Location =
            new Point(1165, 14);

        btnClose.Size =
            new Size(105, 38);

        btnClose.Text =
            "Close";

        btnClose.UseVisualStyleBackColor =
            true;

        btnClose.Click +=
            BtnClose_Click;

        pnlBottom.Controls.Add(
            lblStatus);

        pnlBottom.Controls.Add(
            btnCopyCell);

        pnlBottom.Controls.Add(
            btnCopyRow);

        pnlBottom.Controls.Add(
            btnClose);

        // =====================================================
        // FORM CONTROLS
        // =====================================================

        Controls.Add(
            dgvData);

        Controls.Add(
            pnlBottom);

        Controls.Add(
            pnlHeader);

        // =====================================================
        // END INIT
        // =====================================================

        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();

        ((System.ComponentModel.ISupportInitialize)dgvData)
            .EndInit();

        pnlBottom.ResumeLayout(false);

        ResumeLayout(false);
    }
}