using application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class RankingRepository: IRankingRepository
    {
        private readonly MyChannelDbContext _context;
        public RankingRepository(MyChannelDbContext context)
        {
            _context = context;
        }

        public bool Add(VideoLiked videoLiked)
        {
            try
            {
                VideoLiked? liked = _context.VideoLikeds.SingleOrDefault(x => x.VideoId == videoLiked.VideoId && x.UserId == videoLiked.UserId);
                if(liked == null)
                    _context.VideoLikeds.Add(videoLiked);
                else
                {
                    liked.IsLiked = videoLiked.IsLiked;
                }
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool AddView(int videoId, int? userId)
        {
            try
            {
                View view = new View()
                {
                    UserId = userId,
                    VideoId = videoId,
                    ViewDate = DateTime.Now
                };
                _context.Add(view);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public int GetLiked(int videoId, bool isLike)
        {
            return _context.VideoLikeds.Count(x => x.VideoId == videoId && x.IsLiked == isLike);
        }

        public int GetViews(int videoId)
        {
            return _context.Views.Count(x => x.VideoId == videoId);
        }

        public bool? IsLiked(int videoId, int userId)
        {
            return _context.VideoLikeds.SingleOrDefault(x => x.VideoId == videoId && x.UserId == userId)?.IsLiked;
        }
    }
}
