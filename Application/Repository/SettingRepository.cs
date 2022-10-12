using application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private MyChannelDbContext _context;
        public SettingRepository(MyChannelDbContext context)
        {
            _context = context;
        }
        public bool AddSetting(Setting setting)
        {
            try
            {
                _context.Settings.Add(setting);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Setting? GetSetting(int UserId)
        {
            return _context.Settings.SingleOrDefault(x => x.UserId == UserId);
        }

        public bool UpdateSetting(Setting setting)
        {
            try
            {
                _context.Settings.Update(setting);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
