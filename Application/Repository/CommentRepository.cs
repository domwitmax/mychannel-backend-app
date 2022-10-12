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
    public class CommentRepository: ICommentRepository
    {
        MyChannelDbContext _channelDbContext;
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
