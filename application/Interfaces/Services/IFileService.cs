using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services
{
    public interface IFileService
    {
        string? LoadVideo(IFormFile fileStreamVideo, int userId);
        string? LoadThumbnail(IFormFile fileStreamThumbnail, int userId);
        bool AddNewFolder(string userName);
    }
}
