using Application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;

namespace Application.Repository
{
    public class GetVideoRepository: IGetVideoRepository
    {
        private readonly MyChannelDbContext _context;
        public GetVideoRepository(MyChannelDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Video> Search(string searchKey, int? userId)
        {
            IEnumerable<Video> videos = _context.Videos
                .Where(x => x.Title.IndexOf(searchKey) != -1 && (x.AuthorId == userId || x.VideoPath != null && x.ThumbnailPath != null))
                .OrderBy(x => x.Title.IndexOf(searchKey));
            return videos;
        }

        public IEnumerable<Video> VideoProposing(int? userId)
        {
            IEnumerable<Video> videos = _context.Videos.Where(x => x.AuthorId == userId || x.VideoPath != null && x.ThumbnailPath != null);
            return videos;
        }
    }
}
