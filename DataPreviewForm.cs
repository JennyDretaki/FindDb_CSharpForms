using System.Data;

namespace FindDb;

public partial class DataPreviewForm : Form
{
    private readonly DataTable _data;

    public DataPreviewForm(
        string database,
        string schema,
        string table,
        DataTable data)
    {
        InitializeComponent();

        _data = data;

        Text =
            $"{database} - {schema}.{table}";

        lblTableName.Text =
            $"{database} → {schema}.{table}";

        lblRowCount.Text =
            $"{data.Rows.Count} rows loaded";

        dgvData.DataSource =
            data;
    }

    // ============================================================
    // COPY SELECTED CELL
    // ============================================================

    private void BtnCopyCell_Click(
        object? sender,
        EventArgs e)
    {
        if (dgvData.CurrentCell == null)
        {
            return;
        }

        object? value =
            dgvData.CurrentCell.Value;

        if (value == null ||
            value == DBNull.Value)
        {
            return;
        }

        Clipboard.SetText(
            value.ToString() ?? "");

        lblStatus.Text =
            "Cell copied.";
    }

    // ============================================================
    // COPY ROW
    // ============================================================

    private void BtnCopyRow_Click(
        object? sender,
        EventArgs e)
    {
        if (dgvData.CurrentRow == null)
        {
            return;
        }

        List<string> values =
            new();

        foreach (DataGridViewCell cell
                 in dgvData.CurrentRow.Cells)
        {
            string value =
                cell.Value == null ||
                cell.Value == DBNull.Value
                    ? "NULL"
                    : cell.Value.ToString() ?? "";

            values.Add(value);
        }

        Clipboard.SetText(
            string.Join("\t", values));

        lblStatus.Text =
            "Row copied.";
    }

    // ============================================================
    // CLOSE
    // ============================================================

    private void BtnClose_Click(
        object? sender,
        EventArgs e)
    {
        Close();
    }
}