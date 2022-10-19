using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IVideoRepository
    {
        bool AddVideo(Video video);
        bool UpdateVideo(int videoId, string? path);
        bool UpdateThumbnail(int videoId, string? path);
        bool DeleteVideo(int videoId);
        Video? GetVideo(int videoId);
        IEnumerable<Video> GetAllUserVideos(int userId);
    }
}
