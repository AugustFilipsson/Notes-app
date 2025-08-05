using System.Collections.Concurrent;

namespace NotesApp.Api
{
    public class NotesRepository
    {
        private readonly ConcurrentDictionary<string, Note> _notes = new();

        public IEnumerable<Note> GetAll() => _notes.Values.OrderByDescending(n => n.Updated);
        public Note? Get(string id) => _notes.TryGetValue(id, out var note) ? note : null;
        public void Add(Note note) => _notes.TryAdd(note.Id, note);
        public bool Update(string id, Note updated)
        {
            if (!_notes.ContainsKey(id)) return false;
            updated.Updated = DateTime.UtcNow;
            _notes[id] = updated;
            return true;
        }
        public bool Delete(string id) => _notes.TryRemove(id, out _);
    }
}
