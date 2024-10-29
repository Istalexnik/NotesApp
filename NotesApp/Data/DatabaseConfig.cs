using System.IO;
using Microsoft.Maui.Storage;

namespace NotesApp.Data;

public static class DatabaseConfig
{
    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "NotesApp.db");

    public static void InitializeDatabase()
    {
        using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={DatabasePath}");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Notes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Content TEXT,
                DateCreated TEXT NOT NULL
            );";
        command.ExecuteNonQuery();
    }
}
