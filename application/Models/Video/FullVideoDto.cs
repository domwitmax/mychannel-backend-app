namespace Application.Models.Video
{
    public class FullVideoDto
    {
        public int VideoId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string? VideoPath { get; set; }
        public string? ThumbnailPath { get; set; }
        public DateTime? Created { get; set; }
    }
}
