using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface IHistoryRepository
    {
        public VideoStatus? GetVideoStatus(int videoId);

        public bool SaveVideoStatus(VideoStatus videoStatus);
    }
}
