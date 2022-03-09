# 使用命令行参数作为配置数据源

## 命令行

cmd切换目录到项目编译后目录（Debug模式一般在\bin\Debug\netcoreapp3.1）

执行 `dotnet Ray.EssayNotes.DDD.ConfigurationCommandLineDemo.dll --TestKey1=value1` 

快捷方式：

可以先在win窗体访问编译后目录，然后在窗体地址栏输入`cmd`，可以快捷的进入该目录下的cmd模式。

## 可视化界面

项目-属性-调试-应用程序参数

写在这里的参数，vs调试时会附加执行，就等同于上面的写在命令行里。

## 支持的命令格式

* 无前缀的key=value模式
* --模式：--key=value 或 --key value
* 正斜杠模式：/key=value 或 /key value

注：等号分隔符和空格分隔符不能混用

## 命令替换模式

* 必须以单划线（-）或双划线（--）开头
* 映射字典不能包含重复key

程序会将简写的-xx替换为全名称--xxxx

这就是我们cmd执行`dotnet -h`就等于执行`dotnet --help`的原因。
