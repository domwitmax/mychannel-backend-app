using Application.Models.Setting;

namespace Application.Interfaces.Services
{
    public interface ISettingService
    {
        SettingDto? GetSetting(int UserId);
        bool AddSetting(int UserId);
        bool UpdateSetting(int userId, SettingDto settingDto);
    }
}
