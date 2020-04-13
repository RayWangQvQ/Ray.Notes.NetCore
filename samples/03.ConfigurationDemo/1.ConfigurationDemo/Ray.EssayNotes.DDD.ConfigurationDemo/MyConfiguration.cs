using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.ConfigurationDemo
{
    public static class MyConfiguration
    {
        public static IConfigurationRoot Root { get; set; }
    }
}
