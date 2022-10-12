using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface ICommentRepository
    {
        bool AddComment(Comment comment);
        IEnumerable<Comment> GetComments(int videoId);
    }
}
