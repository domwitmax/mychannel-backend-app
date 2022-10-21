using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Repository
{
    public interface IFileRepository
    {
        public bool AddNewFolder(string userName);
        public string? SaveFile(IFormFile fileFromUser, string userName, string fileName, string subFolder, string extension);
        public bool DeleteFile(string path);
    }
}
