using Application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Comment;
using AutoMapper;

namespace Application.Services
{
    public class CommentService: ICommentService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService(IAccountRepository accountRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public bool AddComment(int videoId, CreatedCommentDto commentDto)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);
            comment.VideoId = videoId;
            return _commentRepository.AddComment(comment);
        }

        public IEnumerable<CommentDto> GetComments(int videoId)
        {
            IEnumerable<Comment> comments = _commentRepository.GetComments(videoId);
            return _mapper.Map<IEnumerable<CommentDto>>(comments).Select((comment) =>
            {
                User? user = _accountRepository.GetUser(comment.UserId);
                comment.UserName = user != null ? user.UserName : string.Empty;
                return comment;
            });
        }
    }
}
