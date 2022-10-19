﻿using Application.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class FileRepository : IFileRepository
    {
        public static string RootPath;
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