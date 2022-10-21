using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface IGetVideoRepository
    {
        IEnumerable<Video> Search(string searchKey, int? userId);
        IEnumerable<Video> VideoProposing(int? userId);
    }
}
