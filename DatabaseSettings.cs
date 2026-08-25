namespace DatabaseSearchTool
{
    public static class DatabaseSettings
    {
        public static string DevConnectionString =
            @"Server=LAWPRODSRVNEW;
          Database=DEV;
          Trusted_Connection=True;
          TrustServerCertificate=True;";

        public static string CtCollectConnectionString =
            @"Server=LAWPRODSRVNEW;
          Database=CTCOLLECT;
          Trusted_Connection=True;
          TrustServerCertificate=True;";
        public static string GetConnectionString(string database)
        {
            return database.ToUpperInvariant() switch
            {
                "DEV" => DevConnectionString,
                "CTCOLLECT" => CtCollectConnectionString,

                _ => throw new ArgumentException(
                    $"Unknown database: {database}")
            };
        }
    }
}