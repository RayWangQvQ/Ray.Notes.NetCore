using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Ray.EssayNotes.FileProviderDemo.Test
{
    [Description("利用EmbeddedFileProvider读取嵌入的资源")]
    public class Test04 : TestBase
    {
        public override void Run()
        {
            IFileProvider fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

            //目录
            IDirectoryContents contents = fileProvider.GetDirectoryContents("/");
            foreach (var item in contents)
            {
                Stream stream = item.CreateReadStream();
                Console.WriteLine(item.Name);
            }

            //文件
            IFileInfo fileInfo = fileProvider.GetFileInfo("emb.html");

            Console.WriteLine(fileInfo.AsFormatJsonStr());
        }
    }
}
