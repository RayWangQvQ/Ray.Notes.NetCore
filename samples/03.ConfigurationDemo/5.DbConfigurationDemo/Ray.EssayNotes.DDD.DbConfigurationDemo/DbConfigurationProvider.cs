using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.DbConfigurationDemo
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private readonly IDictionary<string, string> _initialSettings;
        private readonly Action<DbContextOptionsBuilder> _setup;

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> setup, IDictionary<string, string> initialSettings)
        {
            _setup = setup;
            _initialSettings = initialSettings ?? new Dictionary<string, string>();
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationSettingsContext>();
            _setup(builder);

            using (var dbContext = new ApplicationSettingsContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                Data = dbContext.Settings.Any()
                    ? dbContext.Settings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase)
                    : Initialize(dbContext);
            }
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private IDictionary<string, string> Initialize(ApplicationSettingsContext dbContext)
        {
            foreach (var item in _initialSettings)
            {
                dbContext.Settings.Add(new ApplicationSetting(item.Key, item.Value));
            }
            dbContext.SaveChanges();
            return _initialSettings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
