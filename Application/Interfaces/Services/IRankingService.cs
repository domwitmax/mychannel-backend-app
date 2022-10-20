using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IRankingService
    {
        int GetViews(int videoId);
        bool AddView(int videoId, int? userId);
        int GetLikes(int videoId);
        bool AddLike(int videoId, int userId);
        int GetDislikes(int videoId);
        bool AddDislike(int videoId, int userId);
        bool? IsLiked(int videoId, int userId);
    }
}
