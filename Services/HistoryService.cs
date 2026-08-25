using FindDb.Models;
using System.Text.Json;

namespace FindDb.Services
{
    public class HistoryService
    {
        private readonly string _filePath;

        public HistoryService()
        {
            string folder =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    "DatabaseSearchTool");

            Directory.CreateDirectory(folder);

            _filePath =
                Path.Combine(
                    folder,
                    "search-history.json");
        }

        public async Task<List<SearchHistoryItem>> LoadAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<SearchHistoryItem>();

                string json =
                    await File.ReadAllTextAsync(_filePath);

                List<SearchHistoryItem>? items =
                    JsonSerializer.Deserialize<
                        List<SearchHistoryItem>>(json);

                return items ??
                       new List<SearchHistoryItem>();
            }
            catch
            {
                return new List<SearchHistoryItem>();
            }
        }

        public async Task AddAsync(
            SearchHistoryItem item)
        {
            List<SearchHistoryItem> history =
                await LoadAsync();

            history.Insert(0, item);

            // Κρατάμε τα τελευταία 200 searches
            if (history.Count > 200)
                history = history.Take(200).ToList();

            await SaveAsync(history);
        }

        public async Task DeleteAsync(
            SearchHistoryItem item)
        {
            List<SearchHistoryItem> history =
                await LoadAsync();

            SearchHistoryItem? found =
                history.FirstOrDefault(
                    x =>
                        x.Date == item.Date &&
                        x.SearchText == item.SearchText &&
                        x.Database == item.Database);

            if (found != null)
                history.Remove(found);

            await SaveAsync(history);
        }

        public async Task ClearAsync()
        {
            await SaveAsync(
                new List<SearchHistoryItem>());
        }

        private async Task SaveAsync(
            List<SearchHistoryItem> history)
        {
            JsonSerializerOptions options =
                new()
                {
                    WriteIndented = true
                };

            string json =
                JsonSerializer.Serialize(
                    history,
                    options);

            await File.WriteAllTextAsync(
                _filePath,
                json);
        }
    }
}