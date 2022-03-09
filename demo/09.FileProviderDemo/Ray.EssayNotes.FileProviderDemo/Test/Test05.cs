using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Ray.EssayNotes.FileProviderDemo.Test
{
    [Description("组合文件资源")]
    public class Test05 : TestBase
    {
        public override void Run()
        {
            //物理文件
            string root = AppDomain.CurrentDomain.BaseDirectory;
            IFileProvider fileProvider1 = new PhysicalFileProvider(root);
            //嵌入文件
            IFileProvider fileProvider2 = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

            //组合
            IFileProvider fileProvider = new CompositeFileProvider(fileProvider1, fileProvider2);

            IDirectoryContents contents = fileProvider.GetDirectoryContents("/");
            foreach (var item in contents)
            {
                Stream stream = item.CreateReadStream();
                Console.WriteLine(item.Name);
            }
        }
    }
}
