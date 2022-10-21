using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface IRankingRepository
    {
        int GetViews(int videoId);
        bool AddView(int videoId, int? userId);
        bool Add(VideoLiked videoLiked);
        int GetLiked(int videoId, bool isLike);
        bool? IsLiked(int videoId, int userId);
    }
}
