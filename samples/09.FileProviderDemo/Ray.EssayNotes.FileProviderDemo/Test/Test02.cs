using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Ray.EssayNotes.FileProviderDemo.Test
{
    [Description("FileProvider")]
    public class Test02 : TestBase
    {
        public override void Run()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"Root：{root}");

            IFileProvider fileProvider = new PhysicalFileProvider(root);

            Console.WriteLine(fileProvider.AsFormatJsonStr());
        }
    }
}
