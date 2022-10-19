using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IFileService
    {
        string? LoadVideo(IFormFile fileStreamVideo, int userId);
        string? LoadThumbnail(IFormFile fileStreamThumbnail, int userId);
        bool AddNewFolder(string userName);
    }
}
