using Application.Models.Comment;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Comment, CommentDto>();
            CreateMap<CommentDto, Data.Entities.Comment>();
        }
    }
}
