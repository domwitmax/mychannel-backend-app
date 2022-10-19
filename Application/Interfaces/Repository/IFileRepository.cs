using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IFileRepository
    {
        public bool AddNewFolder(string userName);
        public string? SaveFile(IFormFile fileFromUser, string userName, string fileName, string subFolder, string extension);
        public bool DeleteFile(string path);
    }
}
