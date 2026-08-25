using System.Data;
using DatabaseSearchTool;
using FindDb.Models;
using FindDb.Services;

namespace FindDb;

public partial class MainForm : Form
{
    private readonly DatabaseSearchService _searchService;
    private readonly HistoryService _historyService;

    private CancellationTokenSource? _cancellationTokenSource;

    private List<SearchResult> _currentResults = new();

    public MainForm()
    {
        InitializeComponent();

        _searchService = new DatabaseSearchService();
        _historyService = new HistoryService();
    }

    // ============================================================
    // SEARCH BUTTON
    // ============================================================

    private async void BtnSearch_Click(object? sender, EventArgs e)
    {
        await RunSearchAsync();
    }

    // ============================================================
    // ENTER INSIDE SEARCH BOX
    // ============================================================

    private async void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;

            await RunSearchAsync();
        }
    }

    // ============================================================
    // MAIN SEARCH
    // ============================================================

    private async Task RunSearchAsync()
    {
        string searchText = txtSearch.Text.Trim();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            MessageBox.Show(
                "Γράψε πρώτα κάτι για αναζήτηση.",
                "Search",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtSearch.Focus();

            return;
        }

        if (!chkTables.Checked &&
            !chkColumns.Checked &&
            !chkRecords.Checked)
        {
            MessageBox.Show(
                "Επίλεξε τουλάχιστον Tables, Columns ή Records.",
                "Search",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();

        _cancellationTokenSource =
            new CancellationTokenSource();

        SetSearchingState(true);

        dgvResults.DataSource = null;
        txtPreview.Clear();

        try
        {
            List<SearchResult> allResults = new();

            List<string> selectedDatabases =
                GetSelectedDatabases();

            foreach (string database in selectedDatabases)
            {
                _cancellationTokenSource.Token
                    .ThrowIfCancellationRequested();

                lblStatus.Text =
                    $"Searching {database}...";

                string connectionString =
                    DatabaseSettings
                        .GetConnectionString(database);

                List<SearchResult> databaseResults =
                    await _searchService.SearchAsync(
                        connectionString,
                        database,
                        searchText,
                        chkTables.Checked,
                        chkColumns.Checked,
                        chkRecords.Checked,
                        (int)numSimilarity.Value,
                        _cancellationTokenSource.Token);

                allResults.AddRange(databaseResults);
            }

            _currentResults =
                allResults
                    .OrderByDescending(x => x.Similarity)
                    .ThenBy(x => x.Database)
                    .ThenBy(x => x.Schema)
                    .ThenBy(x => x.Table)
                    .ThenBy(x => x.Column)
                    .ToList();

            dgvResults.DataSource =
                _currentResults;

            lblStatus.Text =
                $"{_currentResults.Count} results found.";

            await SaveHistoryAsync(searchText);

            if (_currentResults.Count == 0)
            {
                MessageBox.Show(
                    "Δεν βρέθηκαν αποτελέσματα.",
                    "Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                dgvResults.ClearSelection();

                if (dgvResults.Rows.Count > 0)
                {
                    dgvResults.Rows[0].Selected = true;
                    dgvResults.CurrentCell =
                        dgvResults.Rows[0].Cells[0];
                }
            }
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text =
                "Search cancelled.";
        }
        catch (Exception ex)
        {
            lblStatus.Text =
                "Search failed.";

            MessageBox.Show(
                ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetSearchingState(false);
        }
    }

    // ============================================================
    // SELECTED DATABASES
    // ============================================================

    private List<string> GetSelectedDatabases()
    {
        if (rbDev.Checked)
        {
            return new List<string>
            {
                "DEV"
            };
        }

        if (rbCtCollect.Checked)
        {
            return new List<string>
            {
                "CTCOLLECT"
            };
        }

        return new List<string>
        {
            "DEV",
            "CTCOLLECT"
        };
    }

    // ============================================================
    // SEARCH STATE
    // ============================================================

    private void SetSearchingState(bool searching)
    {
        btnSearch.Enabled = !searching;
        btnCancel.Enabled = searching;

        txtSearch.Enabled = !searching;

        rbDev.Enabled = !searching;
        rbCtCollect.Enabled = !searching;
        rbBoth.Enabled = !searching;

        chkTables.Enabled = !searching;
        chkColumns.Enabled = !searching;
        chkRecords.Enabled = !searching;

        numSimilarity.Enabled = !searching;

        progressBar.Visible = searching;

        UseWaitCursor = searching;
    }

    // ============================================================
    // CANCEL
    // ============================================================

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();

        lblStatus.Text =
            "Cancelling...";
    }

    // ============================================================
    // SAVE HISTORY
    // ============================================================

    private async Task SaveHistoryAsync(string searchText)
    {
        string database;

        if (rbBoth.Checked)
        {
            database = "BOTH";
        }
        else if (rbDev.Checked)
        {
            database = "DEV";
        }
        else
        {
            database = "CTCOLLECT";
        }

        SearchHistoryItem historyItem =
            new()
            {
                SearchText = searchText,

                Database = database,

                SearchTables =
                    chkTables.Checked,

                SearchColumns =
                    chkColumns.Checked,

                SearchRecords =
                    chkRecords.Checked,

                Date =
                    DateTime.Now
            };

        await _historyService.AddAsync(
            historyItem);
    }

    // ============================================================
    // HISTORY BUTTON
    // ============================================================

    private void BtnHistory_Click(object? sender, EventArgs e)
    {
        using HistoryForm historyForm =
            new();

        historyForm.SearchRequested +=
            HistoryForm_SearchRequested;

        historyForm.ShowDialog(this);
    }

    // ============================================================
    // SEARCH AGAIN FROM HISTORY
    // ============================================================

    private async void HistoryForm_SearchRequested(
        SearchHistoryItem item)
    {
        txtSearch.Text =
            item.SearchText;

        chkTables.Checked =
            item.SearchTables;

        chkColumns.Checked =
            item.SearchColumns;

        chkRecords.Checked =
            item.SearchRecords;

        switch (item.Database.ToUpperInvariant())
        {
            case "DEV":

                rbDev.Checked = true;

                break;

            case "CTCOLLECT":

                rbCtCollect.Checked = true;

                break;

            default:

                rbBoth.Checked = true;

                break;
        }

        await RunSearchAsync();
    }

    // ============================================================
    // RESULT SELECTED
    // ============================================================

    private void DgvResults_SelectionChanged(
        object? sender,
        EventArgs e)
    {
        SearchResult? result =
            GetSelectedResult();

        if (result == null)
        {
            txtPreview.Clear();
            return;
        }

        txtPreview.Text =
            result.PreviewSql;

        lblSelectedObject.Text =
            $"{result.Database} → {result.Schema}.{result.Table}" +
            (string.IsNullOrWhiteSpace(result.Column)
                ? ""
                : $" → {result.Column}");
    }

    // ============================================================
    // DOUBLE CLICK RESULT
    // ============================================================

    private async void DgvResults_CellDoubleClick(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        await PreviewSelectedTableAsync();
    }

    // ============================================================
    // GET SELECTED RESULT
    // ============================================================

    private SearchResult? GetSelectedResult()
    {
        if (dgvResults.CurrentRow?.DataBoundItem
            is SearchResult result)
        {
            return result;
        }

        return null;
    }

    // ============================================================
    // COPY SQL
    // ============================================================

    private void BtnCopySql_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPreview.Text))
        {
            MessageBox.Show(
                "Δεν υπάρχει SQL query για αντιγραφή.",
                "Copy SQL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        Clipboard.SetText(
            txtPreview.Text);

        lblStatus.Text =
            "SQL copied to clipboard.";
    }

    // ============================================================
    // PREVIEW DATA
    // ============================================================

    private async void BtnPreviewData_Click(
        object? sender,
        EventArgs e)
    {
        await PreviewSelectedTableAsync();
    }

    private async Task PreviewSelectedTableAsync()
    {
        SearchResult? result =
            GetSelectedResult();

        if (result == null)
        {
            MessageBox.Show(
                "Επίλεξε πρώτα ένα αποτέλεσμα.",
                "Preview",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        try
        {
            btnPreviewData.Enabled = false;

            lblStatus.Text =
                $"Loading {result.Database} → {result.Schema}.{result.Table}...";

            string connectionString =
                DatabaseSettings
                    .GetConnectionString(
                        result.Database);

            DataTable data =
                await _searchService
                    .GetTablePreviewAsync(
                        connectionString,
                        result.Schema,
                        result.Table,
                        100);

            using DataPreviewForm previewForm =
                new(
                    result.Database,
                    result.Schema,
                    result.Table,
                    data);

            previewForm.ShowDialog(this);

            lblStatus.Text =
                "Ready";
        }
        catch (Exception ex)
        {
            lblStatus.Text =
                "Preview failed.";

            MessageBox.Show(
                ex.Message,
                "Preview Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btnPreviewData.Enabled = true;
        }
    }
}