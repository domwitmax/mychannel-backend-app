using Application.Interfaces.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.Repository
{
    public class FileRepository: IFileRepository
    {
        public bool AddNewFolder(string userName)
        {
            string path = Path.Combine("wwwroot", userName);
            if (Directory.Exists(path))
                return false;
            DirectoryInfo root = Directory.CreateDirectory(path);
            root.CreateSubdirectory("Video");
            root.CreateSubdirectory("Thumbnail");
            return true;
        }

        public bool DeleteFile(string path)
        {
            if (path == null)
                return true;
            path = "wwwroot/" + path;
            path = Path.Combine(path.Split("/"));
            if (!File.Exists(path))
                return true;
            try
            {
                File.Delete(path);
            }
            catch
            {
                return false;
            }
            return true;

        }

        public string? SaveFile(IFormFile fileFromUser, string userName, string fileName, string subFolder, string extension)
        {
            string path = $"{Path.Combine("wwwroot",userName, subFolder, fileName)}.{extension}";
            if (!Directory.Exists(Path.Combine("wwwroot", userName)))
                Directory.CreateDirectory(Path.Combine("wwwroot", userName));
            if (!Directory.Exists(Path.Combine("wwwroot", userName, subFolder)))
                Directory.CreateDirectory(Path.Combine("wwwroot", userName, subFolder));
            if (File.Exists(path))
                return null;
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                fileFromUser.CopyTo(fileStream);
                fileStream.Close();
            }
            catch
            {
                return null;
            }
            return $"{userName}/{subFolder}/{fileName}.{extension}";
        }
    }
}
