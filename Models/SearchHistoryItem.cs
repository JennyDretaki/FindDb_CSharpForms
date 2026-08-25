namespace FindDb.Models
{
    public class SearchHistoryItem
    {
        public string SearchText { get; set; } = "";

        public string Database { get; set; } = "";

        public bool SearchTables { get; set; }

        public bool SearchColumns { get; set; }

        public bool SearchRecords { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string SearchIn
        {
            get
            {
                List<string> values = new();

                if (SearchTables)
                    values.Add("Tables");

                if (SearchColumns)
                    values.Add("Columns");

                if (SearchRecords)
                    values.Add("Records");

                return string.Join(", ", values);
            }
        }
    }
}