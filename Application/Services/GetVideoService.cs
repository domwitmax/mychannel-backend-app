using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Video;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetVideoService : IGetVideoService
    {
        private readonly IGetVideoRepository _getVideoRepository;
        private readonly IMapper _mapper;
        public GetVideoService(IGetVideoRepository getVideoRepository, IMapper mapper)
        {
            _getVideoRepository = getVideoRepository;
            _mapper = mapper;
        }

        public IEnumerable<FullVideoDto> Search(string searchKey)
        {
            IEnumerable<Video> videos = _getVideoRepository.Search(searchKey);
            return _mapper.Map<IEnumerable<FullVideoDto>>(videos);
        }

        public IEnumerable<FullVideoDto> VideoProposing()
        {
            IEnumerable<Video> videos = _getVideoRepository.VideoProposing();
            return _mapper.Map<IEnumerable<FullVideoDto>>(videos);
        }
    }
}
