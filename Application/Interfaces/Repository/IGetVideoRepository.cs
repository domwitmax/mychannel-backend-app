using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface IGetVideoRepository
    {
        IEnumerable<Video> Search(string searchKey);
        IEnumerable<Video> VideoProposing();
    }
}
