using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Models.Comment;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService: Interfaces.Services.ICommentService
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public bool AddComment(CommentDto commentDto)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);
            return _commentRepository.AddComment(comment);
        }

        public IEnumerable<CommentDto> GetComments(int videoId)
        {
            IEnumerable<Comment> comments = _commentRepository.GetComments(videoId);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }
    }
}
