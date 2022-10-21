using Application.Models.Comment;

namespace Application.Interfaces.Services
{
    public interface ICommentService
    {
        bool AddComment(int videoId, CreatedCommentDto commentDto);
        IEnumerable<CommentDto> GetComments(int videoId);
    }
}
