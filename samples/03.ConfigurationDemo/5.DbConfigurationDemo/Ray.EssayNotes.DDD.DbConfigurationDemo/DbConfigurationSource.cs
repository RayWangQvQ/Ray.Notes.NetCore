using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.DbConfigurationDemo
{
    public class DbConfigurationSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _setup;
        private readonly IDictionary<string, string> _initialSettings;

        public DbConfigurationSource(Action<DbContextOptionsBuilder> setup, IDictionary<string, string> initialSettings = null)
        {
            _setup = setup;
            _initialSettings = initialSettings;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(_setup, _initialSettings);
        }
    }
}
