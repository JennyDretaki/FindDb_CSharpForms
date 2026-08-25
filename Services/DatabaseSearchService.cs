using FindDb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FindDb.Services
{
    public class DatabaseSearchService
    {
        // Δεν θέλουμε να γεμίζει το UI με χιλιάδες
        // αποτελέσματα.
        public int MaximumMetadataResults { get; set; } = 500;

        public int MaximumRecordResults { get; set; } = 100;

        public int ResultsPerColumn { get; set; } = 5;

        public int RecordSearchTimeoutSeconds { get; set; } = 10;

        // ========================================================
        // SEARCH
        // ========================================================

        public async Task<List<SearchResult>> SearchAsync(
            string connectionString,
            string databaseName,
            string searchText,
            bool searchTables,
            bool searchColumns,
            bool searchRecords,
            int minimumSimilarity = 45,
            CancellationToken cancellationToken = default)
        {
            List<SearchResult> results = new();

            await using SqlConnection connection =
                new(connectionString);

            await connection.OpenAsync(
                cancellationToken);

            if (searchTables)
            {
                List<SearchResult> tableResults =
                    await SearchTablesAsync(
                        connection,
                        databaseName,
                        searchText,
                        minimumSimilarity,
                        cancellationToken);

                results.AddRange(tableResults);
            }

            if (searchColumns)
            {
                List<SearchResult> columnResults =
                    await SearchColumnsAsync(
                        connection,
                        databaseName,
                        searchText,
                        minimumSimilarity,
                        cancellationToken);

                results.AddRange(columnResults);
            }

            if (searchRecords)
            {
                List<SearchResult> recordResults =
                    await SearchRecordsAsync(
                        connection,
                        databaseName,
                        searchText,
                        cancellationToken);

                results.AddRange(recordResults);
            }

            return results
                .OrderByDescending(x => x.Similarity)
                .ThenBy(x => x.Table)
                .Take(
                    MaximumMetadataResults +
                    MaximumRecordResults)
                .ToList();
        }

        // ========================================================
        // TABLES
        // ========================================================

        private async Task<List<SearchResult>>
            SearchTablesAsync(
                SqlConnection connection,
                string databaseName,
                string searchText,
                int minimumSimilarity,
                CancellationToken cancellationToken)
        {
            List<SearchResult> results = new();

            const string sql = """
        SELECT
            s.name AS SchemaName,
            t.name AS TableName
        FROM sys.tables t
        INNER JOIN sys.schemas s
            ON t.schema_id = s.schema_id
        WHERE t.is_ms_shipped = 0
        ORDER BY s.name, t.name;
        """;

            await using SqlCommand command =
                new(sql, connection);

            await using SqlDataReader reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                string schema =
                    reader.GetString(0);

                string table =
                    reader.GetString(1);

                int similarity =
                    GetBestSimilarity(
                        searchText,
                        table,
                        $"{schema}.{table}");

                if (similarity < minimumSimilarity)
                    continue;

                SearchResult result =
                    new()
                    {
                        Database = databaseName,
                        Schema = schema,
                        Table = table,
                        Column = "",
                        Type = SearchResultType.Table,
                        Match = table,
                        Similarity = similarity,

                        PreviewSql =
                            $"""
                             SELECT TOP (100) *
                         FROM {Quote(schema)}.{Quote(table)};
                         """
                    };

                results.Add(result);

                if (results.Count >=
                    MaximumMetadataResults)
                {
                    break;
                }
            }

            return results;
        }

        // ========================================================
        // COLUMNS
        // ========================================================

        private async Task<List<SearchResult>>
            SearchColumnsAsync(
                SqlConnection connection,
                string databaseName,
                string searchText,
                int minimumSimilarity,
                CancellationToken cancellationToken)
        {
            List<SearchResult> results = new();

            const string sql = """
        SELECT
            s.name AS SchemaName,
            t.name AS TableName,
            c.name AS ColumnName,
            ty.name AS DataType
        FROM sys.columns c
        INNER JOIN sys.tables t
            ON c.object_id = t.object_id
        INNER JOIN sys.schemas s
            ON t.schema_id = s.schema_id
        INNER JOIN sys.types ty
            ON c.user_type_id = ty.user_type_id
        WHERE t.is_ms_shipped = 0
        ORDER BY
            s.name,
            t.name,
            c.column_id;
        """;

            await using SqlCommand command =
                new(sql, connection);

            await using SqlDataReader reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                string schema =
                    reader.GetString(0);

                string table =
                    reader.GetString(1);

                string column =
                    reader.GetString(2);

                int similarity =
                    GetBestSimilarity(
                        searchText,
                        column,
                        $"{table} {column}",
                        $"{schema} {table} {column}");

                if (similarity < minimumSimilarity)
                    continue;

                results.Add(
                    new SearchResult
                    {
                        Database = databaseName,
                        Schema = schema,
                        Table = table,
                        Column = column,

                        Type =
                            SearchResultType.Column,

                        Match = column,

                        Similarity = similarity,

                        PreviewSql =
                            $"""
                             SELECT TOP (100)
                             {Quote(column)}
                         FROM {Quote(schema)}.{Quote(table)};
                         """
                    });

                if (results.Count >=
                    MaximumMetadataResults)
                {
                    break;
                }
            }

            return results;
        }

        // ========================================================
        // RECORDS
        // ========================================================

        private async Task<List<SearchResult>>
            SearchRecordsAsync(
                SqlConnection connection,
                string databaseName,
                string searchText,
                CancellationToken cancellationToken)
        {
            List<SearchResult> results = new();

            // ----------------------------------------------------
            // Πρώτα βρίσκουμε μόνο text columns.
            // ----------------------------------------------------

            List<TextColumnInfo> columns =
                await GetTextColumnsAsync(
                    connection,
                    cancellationToken);

            foreach (TextColumnInfo column in columns)
            {
                cancellationToken
                    .ThrowIfCancellationRequested();

                if (results.Count >=
                    MaximumRecordResults)
                {
                    break;
                }

                try
                {
                    List<SearchResult> columnResults =
                        await SearchColumnValuesAsync(
                            connection,
                            databaseName,
                            column,
                            searchText,
                            cancellationToken);

                    results.AddRange(
                        columnResults);

                    if (results.Count >=
                        MaximumRecordResults)
                    {
                        break;
                    }
                }
                catch (SqlException)
                {
                    // Κάποιο table μπορεί να είναι τεράστιο
                    // ή να timeoutάρει.
                    // Δεν σταματάμε ολόκληρο το search.
                }
            }

            return results
                .Take(MaximumRecordResults)
                .ToList();
        }

        private async Task<List<TextColumnInfo>>
            GetTextColumnsAsync(
                SqlConnection connection,
                CancellationToken cancellationToken)
        {
            List<TextColumnInfo> columns = new();

            const string sql = """
        SELECT
            s.name AS SchemaName,
            t.name AS TableName,
            c.name AS ColumnName,
            ty.name AS DataType
        FROM sys.columns c
        INNER JOIN sys.tables t
            ON c.object_id = t.object_id
        INNER JOIN sys.schemas s
            ON t.schema_id = s.schema_id
        INNER JOIN sys.types ty
            ON c.user_type_id = ty.user_type_id
        WHERE
            t.is_ms_shipped = 0
            AND ty.name IN
            (
                'varchar',
                'nvarchar',
                'char',
                'nchar',
                'text',
                'ntext'
            )
        ORDER BY
            s.name,
            t.name,
            c.column_id;
        """;

            await using SqlCommand command =
                new(sql, connection);

            await using SqlDataReader reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                columns.Add(
                    new TextColumnInfo
                    {
                        Schema =
                            reader.GetString(0),

                        Table =
                            reader.GetString(1),

                        Column =
                            reader.GetString(2)
                    });
            }

            return columns;
        }

        private async Task<List<SearchResult>>
            SearchColumnValuesAsync(
                SqlConnection connection,
                string databaseName,
                TextColumnInfo info,
                string searchText,
                CancellationToken cancellationToken)
        {
            List<SearchResult> results =
                new();

            string sql =
                $"""
                 SELECT TOP ({ResultsPerColumn})
                 CONVERT(NVARCHAR(4000), {Quote(info.Column)})
                     AS MatchedValue
             FROM {Quote(info.Schema)}.{Quote(info.Table)}
             WHERE CONVERT(NVARCHAR(4000), {Quote(info.Column)})
                   LIKE @SearchText;
             """;

            await using SqlCommand command =
                new(sql, connection);

            command.CommandTimeout =
                RecordSearchTimeoutSeconds;

            command.Parameters.Add(
                    "@SearchText",
                    SqlDbType.NVarChar,
                    4000)
                .Value =
                $"%{searchText}%";

            await using SqlDataReader reader =
                await command.ExecuteReaderAsync(
                    cancellationToken);

            while (await reader.ReadAsync(
                       cancellationToken))
            {
                string value =
                    reader.IsDBNull(0)
                        ? ""
                        : reader.GetString(0);

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                results.Add(
                    new SearchResult
                    {
                        Database =
                            databaseName,

                        Schema =
                            info.Schema,

                        Table =
                            info.Table,

                        Column =
                            info.Column,

                        Type =
                            SearchResultType.Record,

                        Match =
                            TrimDisplayValue(value),

                        Similarity =
                            CalculateRecordSimilarity(
                                searchText,
                                value),

                        PreviewSql =
                            $"""
                             SELECT TOP (100) *
                         FROM {Quote(info.Schema)}.{Quote(info.Table)}
                         WHERE CONVERT(NVARCHAR(4000), {Quote(info.Column)})
                               LIKE N'%{EscapeSqlPreview(searchText)}%';
                         """
                    });
            }

            return results;
        }

        // ========================================================
        // TABLE PREVIEW
        // ========================================================

        public async Task<DataTable> GetTablePreviewAsync(
            string connectionString,
            string schema,
            string table,
            int top = 100)
        {
            DataTable dataTable =
                new();

            await using SqlConnection connection =
                new(connectionString);

            await connection.OpenAsync();

            string sql =
                $"""
                 SELECT TOP ({top}) *
             FROM {Quote(schema)}.{Quote(table)};
             """;

            await using SqlCommand command =
                new(sql, connection);

            command.CommandTimeout = 15;

            await using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            dataTable.Load(reader);

            return dataTable;
        }

        // ========================================================
        // HELPERS
        // ========================================================

        private static int GetBestSimilarity(
            string search,
            params string[] values)
        {
            int max = 0;

            foreach (string value in values)
            {
                int score =
                    FuzzySearch.Similarity(
                        search,
                        value);

                if (FuzzySearch.ContainsAllKeywords(
                        value,
                        search))
                {
                    score =
                        Math.Max(score, 97);
                }

                max =
                    Math.Max(max, score);
            }

            return max;
        }

        private static int CalculateRecordSimilarity(
            string search,
            string value)
        {
            if (value.Equals(
                    search,
                    StringComparison.OrdinalIgnoreCase))
            {
                return 100;
            }

            if (value.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase))
            {
                return 98;
            }

            return FuzzySearch.Similarity(
                search,
                value);
        }

        private static string Quote(
            string identifier)
        {
            return
                $"[{identifier.Replace("]", "]]")}]";
        }

        private static string EscapeSqlPreview(
            string value)
        {
            return value.Replace(
                "'",
                "''");
        }

        private static string TrimDisplayValue(
            string value)
        {
            const int max = 250;

            value =
                value
                    .Replace("\r", " ")
                    .Replace("\n", " ");

            if (value.Length <= max)
                return value;

            return value[..max] + "...";
        }

        private class TextColumnInfo
        {
            public string Schema { get; set; } = "";

            public string Table { get; set; } = "";

            public string Column { get; set; } = "";
        }
    }
}