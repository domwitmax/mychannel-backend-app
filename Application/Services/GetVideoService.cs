using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Video;
using Application.Repository;
using AutoMapper;

namespace Application.Services
{
    public class GetVideoService: IGetVideoService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGetVideoRepository _getVideoRepository;
        private readonly IMapper _mapper;
        public GetVideoService(IGetVideoRepository getVideoRepository, IMapper mapper, IAccountRepository accountRepository)
        {
            _getVideoRepository = getVideoRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public IEnumerable<FullVideoDto> Search(string searchKey, int? userId)
        {
            IEnumerable<Video> videos = _getVideoRepository.Search(searchKey, userId);
            IEnumerable<FullVideoDto> result = _mapper.Map<IEnumerable<FullVideoDto>>(videos);
            return result.Select(data =>
            {
                string? userName = _accountRepository.GetUser(data.AuthorId)?.UserName;
                data.AuthorName = userName != null ? userName : "";
                return data;
            });
        }

        public IEnumerable<FullVideoDto> VideoProposing(int? userid)
        {
            IEnumerable<Video> videos = _getVideoRepository.VideoProposing(userid);
            IEnumerable<FullVideoDto> result = _mapper.Map<IEnumerable<FullVideoDto>>(videos);
            return result.Select(data =>
            {
                string? userName = _accountRepository.GetUser(data.AuthorId)?.UserName;
                data.AuthorName = userName != null ? userName : "";
                return data;
            });
        }
    }
}
