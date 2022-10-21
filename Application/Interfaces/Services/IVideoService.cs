using Application.Models.Video;

namespace Application.Interfaces.Services
{
    public interface IVideoService
    {
        bool Exist(int videoId, int? userId);
        int? AddVideo(VideoDto videoDto);
        bool DeleteVideo(int videoId, int? userId);
        FullVideoDto? GetVideo(int videoId, int? userId);
        IEnumerable<FullVideoDto> GetAllVideo(string userName, int? userId);
        bool UpdateVideo(string videoPath, int videoId);
        bool UpdateThumbnail(string thumbnailPath, int videoId);
    }
}
