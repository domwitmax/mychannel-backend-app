using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Models.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Services
{
    public class VideoService : Interfaces.Services.IVideoService
    {
        IAccountRepository _accountRepository;
        IVideoRepository _videoRepository;
        IFileRepository _fileRepository;
        IMapper _mapper;

        public VideoService(IAccountRepository accountRepository, IVideoRepository videoRepository,IFileRepository fileRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _videoRepository = videoRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public int? AddVideo(VideoDto videoDto)
        {
            Video video = _mapper.Map<Video>(videoDto);
            bool result = _videoRepository.AddVideo(video);
            if (!result)
                return null;
            return video.VideoId;
        }

        public bool DeleteVideo(int videoId)
        {
            Video? video = _videoRepository.GetVideo(videoId);
            if(video == null)
                return false;
            if(video.VideoPath != null)
                _fileRepository.DeleteFile(video.VideoPath);
            if(video.ThumbnailPath != null)
                _fileRepository.DeleteFile(video.ThumbnailPath);
            return _videoRepository.DeleteVideo(videoId);
        }

        public IEnumerable<FullVideoDto> GetAllVideo(string userName)
        {
            int? userId = _accountRepository.GetUser(userName)?.UserId;
            if(userId == null)
                return Enumerable.Empty<FullVideoDto>();
            IEnumerable<Video> videos = _videoRepository.GetAllUserVideos(userId.Value);
            return _mapper.Map<IEnumerable<FullVideoDto>>(videos);
        }

        public FullVideoDto? GetVideo(int videoId)
        {
            Video? video = _videoRepository.GetVideo(videoId);
            return _mapper.Map<FullVideoDto?>(video);
        }

        public bool UpdateThumbnail(string thumbnailPath, int videoId)
        {
            return _videoRepository.UpdateThumbnail(videoId, thumbnailPath);
        }

        public bool UpdateVideo(string videoPath, int videoId)
        {
            return _videoRepository.UpdateVideo(videoId, videoPath);
        }
    }
}
