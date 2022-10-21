namespace Application.Models.Account
{
    public class UpdateDto
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
