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
    public class VideoRepository : IVideoRepository
    {
        private readonly MyChannelDbContext _context;
        public VideoRepository(MyChannelDbContext context)
        {
            _context = context;
        }

        public bool AddVideo(Video video)
        {
            try
            {
                _context.Videos.Add(video);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteVideo(int videoId)
        {
            Video? video = _context.Videos.SingleOrDefault(x => x.VideoId == videoId);
            if (video == null)
                return false;
            _context.Videos.Remove(video);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Video> GetAllUserVideos(int userId)
        {
            return _context.Videos.Where(x => x.AuthorId == userId);
        }

        public Video? GetVideo(int videoId)
        {
            return _context.Videos.SingleOrDefault(x => x.VideoId == videoId);
        }

        public bool UpdateThumbnail(int videoId, string? path)
        {
            Video? video = _context.Videos.SingleOrDefault(x => x.VideoId == videoId);
            if (video == null)
                return false;
            video.ThumbnailPath = path;
            _context.Videos.Update(video);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateVideo(int videoId, string? path)
        {
            Video? video = _context.Videos.SingleOrDefault(x => x.VideoId == videoId);
            if (video == null)
                return false;
            video.VideoPath = path;
            _context.Videos.Update(video);
            _context.SaveChanges();
            return true;
        }
    }
}
