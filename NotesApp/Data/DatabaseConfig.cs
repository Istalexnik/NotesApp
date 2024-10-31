using System.IO;
using Microsoft.Maui.Storage;
using Microsoft.Data.Sqlite;

namespace NotesApp.Data
{
    public static class DatabaseConfig
    {
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "NotesApp1.db");

        public static void InitializeDatabase()
        {
            // Open a connection to the SQLite database
            using var connection = new SqliteConnection($"Data Source={DatabasePath}");
            connection.Open();

            // Create the Notes table if it doesn't exist, including the new EditDate column
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Content TEXT,
                    DateCreated TEXT NOT NULL,
                    EditDate TEXT
                );";
            command.ExecuteNonQuery();
        }
    }
}
