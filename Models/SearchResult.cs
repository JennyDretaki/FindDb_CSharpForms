namespace FindDb.Models
{
    public enum SearchResultType
    {
        Table,
        Column,
        Record
    }

    public class SearchResult
    {
        public string Database { get; set; } = "";

        public string Schema { get; set; } = "";

        public string Table { get; set; } = "";

        public string Column { get; set; } = "";

        public SearchResultType Type { get; set; }

        public string Match { get; set; } = "";

        public int Similarity { get; set; }

        public string PreviewSql { get; set; } = "";

        public string DisplayName
        {
            get
            {
                return Type switch
                {
                    SearchResultType.Table =>
                        $"{Schema}.{Table}",

                    SearchResultType.Column =>
                        $"{Schema}.{Table}.{Column}",

                    SearchResultType.Record =>
                        $"{Schema}.{Table}.{Column}",

                    _ => ""
                };
            }
        }

        public string ResultType => Type.ToString();
    }
}