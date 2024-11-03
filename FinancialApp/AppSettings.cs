namespace FinancialApp;

public static class AppSettings
{
    private static readonly string DatabaseName = "database.db";

    private static readonly string DatabaseDirectory = FileSystem.AppDataDirectory;

    public static readonly string DatabasePath = Path.Combine(DatabaseDirectory, DatabaseName);
}
