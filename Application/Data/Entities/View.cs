namespace Application.Data.Entities
{
    public class View
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int VideoId { get; set; }
        public DateTime ViewDate { get; set; }
    }
}
