using Microsoft.Data.Sqlite;
using NotesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesApp.Data;

public class NoteRepository
{
    public NoteRepository()
    {
        DatabaseConfig.InitializeDatabase();
    }

    public async Task<List<Note>> GetNotesAsync()
    {
        var notes = new List<Note>();
        using var connection = new SqliteConnection($"Data Source={DatabaseConfig.DatabasePath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, Content, DateCreated FROM Notes";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            notes.Add(new Note
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Content = reader.IsDBNull(2) ? "" : reader.GetString(2),
                DateCreated = DateTime.Parse(reader.GetString(3))
            });
        }
        return notes;
    }

    public async Task AddNoteAsync(Note note)
    {
        using var connection = new SqliteConnection($"Data Source={DatabaseConfig.DatabasePath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Notes (Title, Content, DateCreated) 
            VALUES ($title, $content, $dateCreated)";
        command.Parameters.AddWithValue("$title", note.Title);
        command.Parameters.AddWithValue("$content", note.Content);
        command.Parameters.AddWithValue("$dateCreated", note.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateNoteAsync(Note note)
    {
        using var connection = new SqliteConnection($"Data Source={DatabaseConfig.DatabasePath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Notes SET Title = $title, Content = $content WHERE Id = $id";
        command.Parameters.AddWithValue("$title", note.Title);
        command.Parameters.AddWithValue("$content", note.Content);
        command.Parameters.AddWithValue("$id", note.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteNoteAsync(int id)
    {
        using var connection = new SqliteConnection($"Data Source={DatabaseConfig.DatabasePath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Notes WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        await command.ExecuteNonQueryAsync();
    }
}
