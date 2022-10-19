using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Entities
{
    public class Video
    {
        public int VideoId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string? VideoPath { get; set; }
        public string? ThumbnailPath { get; set; }
        public DateTime? Created { get; set; }
    }
}
