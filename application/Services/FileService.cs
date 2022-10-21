using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class FileService: IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IAccountRepository _accountRepository;
        public FileService(IFileRepository fileRepository, IAccountRepository accountRepository)
        {
            _fileRepository = fileRepository;
            _accountRepository = accountRepository;
        }
        private string? getUserName(int userId)
        {
            return _accountRepository.GetUser(userId)?.UserName;
        }

        public bool AddNewFolder(string userName)
        {
            bool result = _fileRepository.AddNewFolder(userName);
            if(!result)
                return false;
            return true;
        }

        public string? LoadThumbnail(IFormFile fileStreamThumbnail, int userId)
        {
            string? userName = getUserName(userId);
            if (userName == null)
                return null;
            return _fileRepository.SaveFile(fileStreamThumbnail, userName,Guid.NewGuid().ToString(), "Thumbnail", "png");
        }

        public string? LoadVideo(IFormFile fileStreamVideo, int userId)
        {
            string? userName = getUserName(userId);
            if (userName == null)
                return null;
            return _fileRepository.SaveFile(fileStreamVideo, userName, Guid.NewGuid().ToString(), "Video", "mp4");
        }
    }
}
