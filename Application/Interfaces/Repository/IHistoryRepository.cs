using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IHistoryRepository
    {
        public VideoStatus? GetVideoStatus(int videoId);

        public bool SaveVideoStatus(VideoStatus videoStatus);
    }
}
