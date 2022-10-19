using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Video
{
    public class VideoDto
    {
        public int VideoId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
    }
}
