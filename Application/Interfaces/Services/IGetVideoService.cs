using Application.Models.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IGetVideoService
    {
        IEnumerable<FullVideoDto> Search(string searchKey);
        IEnumerable<FullVideoDto> VideoProposing();
    }
}
