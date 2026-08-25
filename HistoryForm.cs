
using FindDb.Models;
using FindDb.Services;

namespace FindDb;

public partial class HistoryForm : Form
{
    private readonly HistoryService _historyService;

    private List<SearchHistoryItem> _history =
        new();

    public event Action<SearchHistoryItem>?
        SearchRequested;

    public HistoryForm()
    {
        InitializeComponent();

        _historyService =
            new HistoryService();

        Shown +=
            HistoryForm_Shown;
    }

    // ============================================================
    // LOAD
    // ============================================================

    private async void HistoryForm_Shown(
        object? sender,
        EventArgs e)
    {
        await ReloadAsync();
    }

    private async Task ReloadAsync()
    {
        _history =
            await _historyService
                .LoadAsync();

        dgvHistory.DataSource =
            null;

        dgvHistory.DataSource =
            _history;

        lblHistoryCount.Text =
            $"{_history.Count} searches";
    }

    // ============================================================
    // GET SELECTED
    // ============================================================

    private SearchHistoryItem? GetSelected()
    {
        if (dgvHistory.CurrentRow?.DataBoundItem
            is SearchHistoryItem item)
        {
            return item;
        }

        return null;
    }

    // ============================================================
    // SEARCH AGAIN
    // ============================================================

    private void BtnSearchAgain_Click(
        object? sender,
        EventArgs e)
    {
        RunSelectedSearch();
    }

    private void DgvHistory_CellDoubleClick(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            RunSelectedSearch();
        }
    }

    private void RunSelectedSearch()
    {
        SearchHistoryItem? item =
            GetSelected();

        if (item == null)
        {
            MessageBox.Show(
                "Επίλεξε πρώτα μία αναζήτηση.",
                "History",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        SearchRequested?.Invoke(item);

        Close();
    }

    // ============================================================
    // DELETE
    // ============================================================

    private async void BtnDelete_Click(
        object? sender,
        EventArgs e)
    {
        SearchHistoryItem? item =
            GetSelected();

        if (item == null)
        {
            MessageBox.Show(
                "Επίλεξε πρώτα μία αναζήτηση.",
                "History",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        DialogResult confirmation =
            MessageBox.Show(
                $"Να διαγραφεί η αναζήτηση \"{item.SearchText}\";",
                "Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

        if (confirmation !=
            DialogResult.Yes)
        {
            return;
        }

        await _historyService
            .DeleteAsync(item);

        await ReloadAsync();
    }

    // ============================================================
    // CLEAR
    // ============================================================

    private async void BtnClear_Click(
        object? sender,
        EventArgs e)
    {
        if (_history.Count == 0)
        {
            return;
        }

        DialogResult confirmation =
            MessageBox.Show(
                "Να διαγραφεί ολόκληρο το ιστορικό αναζητήσεων;",
                "Clear History",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

        if (confirmation !=
            DialogResult.Yes)
        {
            return;
        }

        await _historyService
            .ClearAsync();

        await ReloadAsync();
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