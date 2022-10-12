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
        bool AddComment(CommentDto commentDto);
        IEnumerable<CommentDto> GetComments(int videoId);
    }
}
