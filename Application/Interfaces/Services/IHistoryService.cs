using Application.Models.History;

namespace Application.Interfaces.Services
{
    public interface IHistoryService
    {
        VideoStatusDto? GetVideoStatus(int videoId);
        bool SaveVideoStatus(VideoStatusDto videoStatus, int userId);
    }
}
