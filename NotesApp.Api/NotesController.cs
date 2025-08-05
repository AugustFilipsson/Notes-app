using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesRepository _repo;
        public NotesController(NotesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Note> Get(string id)
        {
            var note = _repo.Get(id);
            return note == null ? NotFound() : Ok(note);
        }

        [HttpPost]
        public ActionResult<Note> Create(Note note)
        {
            note.Id = Guid.NewGuid().ToString();
            note.Created = note.Updated = DateTime.UtcNow;
            _repo.Add(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Note note)
        {
            if (id != note.Id) return BadRequest();
            var updated = _repo.Update(id, note);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var deleted = _repo.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
