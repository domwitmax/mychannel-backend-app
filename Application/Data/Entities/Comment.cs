namespace Application.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
