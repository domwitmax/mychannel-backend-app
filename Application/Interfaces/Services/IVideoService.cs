using Application.Models.Video;

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
