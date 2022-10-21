using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Setting;
using AutoMapper;

namespace Application.Services
{
    public class SettingService: ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        public SettingService(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }
        public bool AddSetting(int UserId)
        {
            Setting newSetting = new Setting();
            newSetting.UserId = UserId;
            newSetting.DarkMode = false;
            return _settingRepository.AddSetting(newSetting);
        }

        public SettingDto? GetSetting(int UserId)
        {
            Setting? setting = _settingRepository.GetSetting(UserId);
            return _mapper.Map<SettingDto>(setting);
        }

        public bool UpdateSetting(int userId, SettingDto settingDto)
        {
            Setting setting = _mapper.Map<Setting>(settingDto);
            setting.UserId = userId;
            return _settingRepository.UpdateSetting(setting);
        }
    }
}
