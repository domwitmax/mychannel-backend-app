using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.History;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HistoryService: IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public HistoryService(IHistoryRepository historyRepository, IMapper mapper)
        {
            _historyRepository = historyRepository;
            _mapper = mapper;
        }

        public VideoStatusDto? GetVideoStatus(int videoId)
        {
            VideoStatus? videoStatus = _historyRepository.GetVideoStatus(videoId);
            return _mapper.Map<VideoStatusDto>(videoStatus);
        }

        public bool SaveVideoStatus(VideoStatusDto videoStatus, int userId)
        {
            VideoStatus status = _mapper.Map<VideoStatus>(videoStatus);
            status.UserId = userId;
            return _historyRepository.SaveVideoStatus(status);
        }
    }
}
