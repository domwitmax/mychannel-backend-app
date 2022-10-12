using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Comment
{
    public class CreatedCommentDto
    {
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
