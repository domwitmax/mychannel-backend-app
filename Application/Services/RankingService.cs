using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class RankingService: IRankingService
    {
        private readonly IAccountService _accountService;
        private readonly IRankingRepository _rankingRepository;
        private readonly IVideoService _videoService;
        public RankingService( IRankingRepository rankingRepository, IAccountService accountService, IVideoService videoService)
        {
            _rankingRepository = rankingRepository;
            _accountService = accountService;
            _videoService = videoService;
        }
        private bool videoAndUserExist(int videoId, int? userId)
        {
            if(userId == null)
                return false;
            return _accountService.Exist(userId.Value) && _videoService.Exist(videoId, userId);
        }
        public bool AddDislike(int videoId, int userId)
        {
            if (!videoAndUserExist(videoId, userId))
                return false;
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
            if (!videoAndUserExist(videoId, userId))
                return false;
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
            if(userId != null && !_accountService.Exist(userId.Value))
                userId = null;
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
            if (!videoAndUserExist(videoId, userId))
                return null;
            return _rankingRepository.IsLiked(videoId, userId);
        }
    }
}
