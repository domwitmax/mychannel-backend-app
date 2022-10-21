using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Setting;
using AutoMapper;

namespace Application.Services
{
    public class SettingService: ISettingService
    {
        private readonly IAccountService _accountService;
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        public SettingService(IAccountService accountService, ISettingRepository settingRepository, IMapper mapper)
        {
            _accountService = accountService;
            _settingRepository = settingRepository;
            _mapper = mapper;
        }
        public bool AddSetting(int userId)
        {
            if(!_accountService.Exist(userId))
                return false;
            Setting newSetting = new Setting();
            newSetting.UserId = userId;
            newSetting.DarkMode = false;
            return _settingRepository.AddSetting(newSetting);
        }

        public SettingDto? GetSetting(int userId)
        {
            if (!_accountService.Exist(userId))
                return null;
            Setting? setting = _settingRepository.GetSetting(userId);
            return _mapper.Map<SettingDto>(setting);
        }

        public bool UpdateSetting(int userId, SettingDto settingDto)
        {
            if (!_accountService.Exist(userId))
                return false;
            Setting setting = _mapper.Map<Setting>(settingDto);
            setting.UserId = userId;
            return _settingRepository.UpdateSetting(setting);
        }
    }
}
