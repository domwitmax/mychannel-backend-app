using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Models.Video;
using AutoMapper;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class VideoService: IVideoService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

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

        public bool DeleteVideo(int videoId, int? userId)
        {
            Video? video = _videoRepository.GetVideo(videoId, userId);
            if(video == null)
                return false;
            if(video.VideoPath != null)
                _fileRepository.DeleteFile(video.VideoPath);
            if(video.ThumbnailPath != null)
                _fileRepository.DeleteFile(video.ThumbnailPath);
            return _videoRepository.DeleteVideo(videoId);
        }

        public bool Exist(int videoId, int? userId)
        {
            Video? video = _videoRepository.GetVideo(videoId, userId);
            return video != null;
        }

        public IEnumerable<FullVideoDto> GetAllVideo(string userName, int? userId)
        {
            int? authorId = _accountRepository.GetUser(userName)?.UserId;
            if(authorId == null)
                return Enumerable.Empty<FullVideoDto>();
            IEnumerable<Video> videos = _videoRepository.GetAllUserVideos(authorId.Value, userId);
            IEnumerable<FullVideoDto> result = _mapper.Map<IEnumerable<FullVideoDto>>(videos);
            return result.Select(data =>
            {
                string? userName = _accountRepository.GetUser(data.AuthorId)?.UserName;
                data.AuthorName = userName != null ? userName : "";
                return data;
            });
        }

        public FullVideoDto? GetVideo(int videoId, int? userId)
        {
            Video? video = _videoRepository.GetVideo(videoId, userId);
            FullVideoDto? result = _mapper.Map<FullVideoDto?>(video);
            if (result != null)
            {
                string? userName = _accountRepository.GetUser(result.AuthorId)?.UserName;
                if(userName == null) 
                    return null;
                result.AuthorName = userName;
            }
            return result;
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
