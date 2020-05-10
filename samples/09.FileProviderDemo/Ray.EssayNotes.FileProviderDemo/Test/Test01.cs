using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Ray.EssayNotes.FileProviderDemo.Test
{
    [Description("利用PhysicalFileProvider读取物理文件")]
    public class Test01 : TestBase
    {
        public override void Run()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"Root：{root}");

            IFileProvider fileProvider = new PhysicalFileProvider(root);

            IDirectoryContents contents = fileProvider.GetDirectoryContents("/");
            foreach (var item in contents)
            {
                Stream stream = item.CreateReadStream();
                Console.WriteLine(item.Name);
            }
        }
    }
}
