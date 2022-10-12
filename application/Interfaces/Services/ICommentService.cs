using Application.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICommentService
    {
        bool AddComment(int videoId, CreatedCommentDto commentDto);
        IEnumerable<CommentDto> GetComments(int videoId);
    }
}
