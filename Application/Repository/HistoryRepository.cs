using Application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;

namespace Application.Repository
{
    public class HistoryRepository: IHistoryRepository
    {
        private readonly MyChannelDbContext _context;
        public HistoryRepository(MyChannelDbContext context)
        {
            _context = context;
        }

        public VideoStatus? GetVideoStatus(int videoId)
        {
            return _context.VideoStatuses.SingleOrDefault(x => x.VideoId == videoId);
        }

        public bool SaveVideoStatus(VideoStatus videoStatus)
        {
            try
            {
                VideoStatus? status = _context.VideoStatuses.SingleOrDefault(x => x.VideoId == videoStatus.VideoId);
                if (status != null)
                {
                    status.VideoTime = videoStatus.VideoTime;
                }
                else
                    _context.VideoStatuses.Add(videoStatus);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
