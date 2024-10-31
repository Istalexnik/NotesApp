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

        public Task AddNoteAsync(string title, string content)
        {
            var note = new Note { Title = title, Content = content };
            return _noteRepository.AddNoteAsync(note);
        }

        public Task UpdateNoteAsync(int id, string title, string content)
        {
            var note = new Note { Id = id, Title = title, Content = content };
            return _noteRepository.UpdateNoteAsync(note);
        }

        public Task DeleteNoteAsync(int id)
        {
            return _noteRepository.DeleteNoteAsync(id);
        }
    }
}
