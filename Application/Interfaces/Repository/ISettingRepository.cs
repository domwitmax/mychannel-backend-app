using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface ISettingRepository
    {
        bool AddSetting(Setting setting);
        bool UpdateSetting(Setting setting);
        Setting? GetSetting(int UserId);
    }
}
