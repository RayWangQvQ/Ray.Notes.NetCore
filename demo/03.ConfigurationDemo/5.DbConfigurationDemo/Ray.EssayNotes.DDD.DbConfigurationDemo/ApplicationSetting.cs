using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Ray.EssayNotes.DDD.DbConfigurationDemo
{
    [Table("ApplicationSettings")]
    public class ApplicationSetting
    {
        private string key;

        [Key]
        public string Key
        {
            get { return key; }
            set { key = value.ToLowerInvariant(); }
        }

        [Required]
        [MaxLength(512)]
        public string Value { get; set; }

        public ApplicationSetting()
        { }

        public ApplicationSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class ApplicationSettingsContext : DbContext
    {
        public ApplicationSettingsContext(DbContextOptions options) : base(options)
        { }

        public DbSet<ApplicationSetting> Settings { get; set; }
    }
}
