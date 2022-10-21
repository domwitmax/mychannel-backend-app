using Application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;

namespace Application.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly MyChannelDbContext _channelDbContext;
        public CommentRepository(MyChannelDbContext context)
        {
            _channelDbContext = context;
        }
        public bool AddComment(Comment comment)
        {
            try
            {
                _channelDbContext.Comments.Add(comment);
                _channelDbContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public IEnumerable<Comment> GetComments(int videoId)
        {
            return _channelDbContext.Comments.Where(c => c.VideoId == videoId);
        }
    }
}
