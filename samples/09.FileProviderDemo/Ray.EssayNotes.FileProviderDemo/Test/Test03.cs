using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Ray.EssayNotes.FileProviderDemo.Test
{
    [Description("DirectoryContents")]
    public class Test03 : TestBase
    {
        public override void Run()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"Root：{root}");

            IFileProvider fileProvider = new PhysicalFileProvider(root);

            IDirectoryContents contents = fileProvider.GetDirectoryContents("/");

            Console.WriteLine(contents.AsFormatJsonStr());
        }
    }
}
