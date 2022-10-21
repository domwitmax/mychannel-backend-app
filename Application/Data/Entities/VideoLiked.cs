namespace Application.Data.Entities
{
    public class VideoLiked
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public bool IsLiked { get; set; }
    }
}
