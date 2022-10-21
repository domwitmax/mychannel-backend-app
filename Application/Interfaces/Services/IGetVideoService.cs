using Application.Models.Video;

namespace Application.Interfaces.Services
{
    public interface IGetVideoService
    {
        IEnumerable<FullVideoDto> Search(string searchKey, int? userId);
        IEnumerable<FullVideoDto> VideoProposing(int? userId);
    }
}
