﻿using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface IVideoRepository
    {
        bool AddVideo(Video video);
        bool UpdateVideo(int videoId, string? path);
        bool UpdateThumbnail(int videoId, string? path);
        bool DeleteVideo(int videoId);
        Video? GetVideo(int videoId);
        IEnumerable<Video> GetAllUserVideos(int userId);
    }
}
