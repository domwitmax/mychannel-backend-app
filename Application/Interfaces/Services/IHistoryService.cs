using Application.Models.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IHistoryService
    {
        VideoStatusDto? GetVideoStatus(int videoId);
        bool SaveVideoStatus(VideoStatusDto videoStatus, int userId);
    }
}
