using Application.Data.Entities;
using Application.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISettingService
    {
        SettingDto? GetSetting(int UserId);
        bool AddSetting(int UserId);
        bool UpdateSetting(int userId, SettingDto settingDto);
    }
}
