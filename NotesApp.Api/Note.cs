namespace NotesApp.Api
{
    public class Note
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
