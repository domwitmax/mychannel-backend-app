using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.History;
using AutoMapper;

namespace Application.Services
{
    public class HistoryService: IHistoryService
    {
        private readonly IAccountService _accountService;
        private readonly IVideoService _videoService;
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public HistoryService(IAccountService accountService, IHistoryRepository historyRepository, IVideoService videoService,  IMapper mapper)
        {
            _accountService = accountService;
            _videoService = videoService;
            _historyRepository = historyRepository;
            _mapper = mapper;
        }

        public VideoStatusDto? GetVideoStatus(int videoId, int? userId)
        {
            if (_videoService.Exist(videoId, userId))
                return null;
            VideoStatus? videoStatus = _historyRepository.GetVideoStatus(videoId);
            return _mapper.Map<VideoStatusDto>(videoStatus);
        }

        public bool SaveVideoStatus(VideoStatusDto videoStatus, int userId)
        {
            if(_videoService.Exist(videoStatus.VideoId, userId) && _accountService.Exist(userId))
                return false;
            VideoStatus status = _mapper.Map<VideoStatus>(videoStatus);
            status.UserId = userId;
            return _historyRepository.SaveVideoStatus(status);
        }
    }
}
