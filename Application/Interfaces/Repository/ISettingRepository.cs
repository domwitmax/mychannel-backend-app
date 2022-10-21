using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface ISettingRepository
    {
        bool AddSetting(Setting setting);
        bool UpdateSetting(Setting setting);
        Setting? GetSetting(int UserId);
    }
}
