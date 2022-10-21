namespace Application.Data.Entities
{
    public class VideoStatus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public DateTime VideoTime { get; set; }
    }
}
