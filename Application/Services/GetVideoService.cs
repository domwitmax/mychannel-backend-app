using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Video;
using AutoMapper;

namespace Application.Services
{
    public class GetVideoService: IGetVideoService
    {
        private readonly IGetVideoRepository _getVideoRepository;
        private readonly IMapper _mapper;
        public GetVideoService(IGetVideoRepository getVideoRepository, IMapper mapper)
        {
            _getVideoRepository = getVideoRepository;
            _mapper = mapper;
        }

        public IEnumerable<FullVideoDto> Search(string searchKey, int? userId)
        {
            IEnumerable<Video> videos = _getVideoRepository.Search(searchKey, userId);
            return _mapper.Map<IEnumerable<FullVideoDto>>(videos);
        }

        public IEnumerable<FullVideoDto> VideoProposing(int? userid)
        {
            IEnumerable<Video> videos = _getVideoRepository.VideoProposing(userid);
            return _mapper.Map<IEnumerable<FullVideoDto>>(videos);
        }
    }
}
