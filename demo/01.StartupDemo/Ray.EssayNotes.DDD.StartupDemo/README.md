# ����

��demo���ڲ���ASP.NET Core�����������

## ����IHostBuilder������

���ù�������Ϊ5�����֣�

### PartOne��ָ��Host��Ԥ�����Ĭ������

* ConfigureWebHostDefaults

���磺Kestrel�����м����

΢��ٷ����ͣ�

> Initializes a new instance of the Microsoft.AspNetCore.Hosting.IWebHostBuilder class with pre-configured defaults.

> The following defaults are applied to the Microsoft.AspNetCore.Hosting.IWebHostBuilder:
use Kestrel as the web server and configure it using the application's configuration providers, 
adds the HostFiltering middleware, 
adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true, and enable IIS integration.

### PartTwo��ָ��Buider���������

* ConfigureHostConfiguration

���磺IHostEnvironment��

΢��ٷ����ͣ�

> Set up the configuration for the builder itself.
This will be used to initialize the Microsoft.Extensions.Hosting.IHostEnvironment for use later in the build process.
This can be called multiple times and the results will be additive.

### PartThree����������ʣ�µ�����

* ConfigureAppConfiguration

//���磺Ƕ���Լ������������ļ�

΢��ٷ����ͣ�

> Sets up the configuration for the remainder of the build process and application.
This can be called multiple times and the results will be additive.
The results will be available at Microsoft.Extensions.Hosting.HostBuilderContext.Configuration for subsequent operations, as well as in Microsoft.Extensions.Hosting.IHost.Services.

### PartFour����������ע�����

* ConfigureServices��ע�������������

΢��ٷ����ͣ�

> Adds services to the container. 
This can be called multiple times and the results will be additive.

* ConfigureLogging
* Startup
* Startup.ConfigureServices

### PartFive���ܵ�

* Startup.Configure���м����

## ����ִ��˳��

5������˳������˵ʽ�����е�4����ע������У�ConfigureServices��˳������б仯��

��ConfigureServicesд��ConfigureWebHostDefaultsǰ��ʱ������UseStartup<Startup>()֮ǰʱ��ConfigureServices����Startup.ConfigureServices֮ǰִ�У���Test02������ʾ

��ConfigureServicesд��ConfigureWebHostDefaults����ʱ������UseStartup<Startup>()֮��ʱ��ConfigureServices����Startup.ConfigureServices֮��ִ�У���Test01������ʾ

## Startup�಻�Ǳ����

����ʹ��ί�д���Startup�࣬��Test03������ʾ