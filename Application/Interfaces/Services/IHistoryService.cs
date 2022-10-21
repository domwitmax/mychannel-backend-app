using Application.Models.History;

namespace Application.Interfaces.Services
{
    public interface IHistoryService
    {
        VideoStatusDto? GetVideoStatus(int videoId, int? userId);
        bool SaveVideoStatus(VideoStatusDto videoStatus, int userId);
    }
}
