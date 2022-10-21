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

        public IEnumerable<Video> Search(string searchKey)
        {
            IEnumerable<Video> videos = _context.Videos
                .Where(x => x.Title.IndexOf(searchKey) != -1)
                .OrderBy(x => x.Title.IndexOf(searchKey));
            return videos;
        }

        public IEnumerable<Video> VideoProposing()
        {
            IEnumerable<Video> videos = _context.Videos;
            return videos;
        }
    }
}
