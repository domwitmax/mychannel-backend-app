using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RankingService : IRankingService
    {
        IRankingRepository _rankingRepository;
        public RankingService(IRankingRepository rankingRepository)
        {
            _rankingRepository = rankingRepository;
        }

        public bool AddDislike(int videoId, int userId)
        {
            VideoLiked videoLiked = new VideoLiked()
            {
                VideoId = videoId,
                UserId = userId,
                IsLiked = false
            };
            return _rankingRepository.Add(videoLiked);
        }

        public bool AddLike(int videoId, int userId)
        {
            VideoLiked videoLiked = new VideoLiked()
            {
                VideoId = videoId,
                UserId = userId,
                IsLiked = true
            };
            return _rankingRepository.Add(videoLiked);
        }

        public bool AddView(int videoId, int? userId)
        {
            return _rankingRepository.AddView(videoId, userId);
        }

        public int GetDislikes(int videoId)
        {
            return _rankingRepository.GetLiked(videoId, false);
        }

        public int GetLikes(int videoId)
        {
            return _rankingRepository.GetLiked(videoId, true);
        }

        public int GetViews(int videoId)
        {
            return _rankingRepository.GetViews(videoId);
        }

        public bool? IsLiked(int videoId, int userId)
        {
            return _rankingRepository.IsLiked(videoId, userId);
        }
    }
}
