using Application.Models.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IVideoService
    {
        int? AddVideo(VideoDto videoDto);
        bool DeleteVideo(int videoId);
        FullVideoDto? GetVideo(int videoId);
        IEnumerable<FullVideoDto> GetAllVideo(string userName);
        bool UpdateVideo(string videoPath, int videoId);
        bool UpdateThumbnail(string thumbnailPath, int videoId);
    }
}
