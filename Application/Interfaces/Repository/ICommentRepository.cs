using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface ICommentRepository
    {
        bool AddComment(Comment comment);
        IEnumerable<Comment> GetComments(int videoId);
    }
}
