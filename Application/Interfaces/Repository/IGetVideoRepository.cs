using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IGetVideoRepository
    {
        IEnumerable<Video> Search(string searchKey);
        IEnumerable<Video> VideoProposing();
    }
}
