using NotesApp.Data;
using NotesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public class NoteService
    {
        private readonly NoteRepository _noteRepository;

        public NoteService(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task<List<Note>> GetAllNotesAsync()
        {
            return _noteRepository.GetNotesAsync();
        }

        public Task<Note?> GetNoteByIdAsync(int id)
        {
            return _noteRepository.GetNoteByIdAsync(id);
        }

        public Task AddNoteAsync(string title, string content)
        {
            var note = new Note
            {
                Title = title,
                Content = content,
                DateCreated = DateTime.Now
            };
            return _noteRepository.AddNoteAsync(note);
        }

        public Task UpdateNoteAsync(int id, string title, string content)
        {
            return _noteRepository.UpdateNoteAsync(new Note
            {
                Id = id,
                Title = title,
                Content = content,
                EditDate = DateTime.Now // Set EditDate to the current date/time
            });
        }

        public Task DeleteNoteAsync(int id)
        {
            return _noteRepository.DeleteNoteAsync(id);
        }
    }
}
