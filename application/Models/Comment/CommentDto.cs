namespace Application.Models.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int LikedByUsers { get; set; }
        public int DislikedByUsers { get; set; }
        public bool? UserLiked { get; set; }
    }
}
