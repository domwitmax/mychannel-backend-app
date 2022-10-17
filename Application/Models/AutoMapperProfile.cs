using Application.Models.Account;
using Application.Models.Comment;
using Application.Models.Setting;
using Application.Models.Subscription;
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
            #region Account
            CreateMap<Data.Entities.User?, UserDto?>();
            CreateMap<Data.Entities.User, RegisterDto>();
            CreateMap<RegisterDto, Data.Entities.User>()
                .ForMember(dect => dect.Created, act => act.MapFrom(src => DateTime.Now));
            CreateMap<Data.Entities.User, UpdateDto>();
            CreateMap<UpdateDto, Data.Entities.User>();
            CreateMap<Data.Entities.User, GetUserDto>();
            #endregion
            #region Comment
            CreateMap<Data.Entities.Comment, CommentDto>();
            CreateMap<CommentDto, Data.Entities.Comment>();
            CreateMap<CreatedCommentDto, Data.Entities.Comment>()
                .ForMember(x => x.Created, p => p.MapFrom(src => DateTime.Now));
            #endregion
            #region Setting
            CreateMap<Data.Entities.Setting?, SettingDto?>();
            CreateMap<SettingDto, Data.Entities.Setting>();
            #endregion
            #region Subscription
            CreateMap<SubscriptionDto, Data.Entities.Subscription>();
            CreateMap<Data.Entities.Subscription, SubscriptionDto>();
            #endregion
        }
    }
}
