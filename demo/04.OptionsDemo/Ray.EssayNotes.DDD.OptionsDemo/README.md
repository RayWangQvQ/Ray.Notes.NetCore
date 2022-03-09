# Options基础用法

Options模式是对依赖注入模式和配置模式的整合，其作用是使我们的项目更好的实现模块化和松耦合。

## 利用IOptions服务读取非具名Options

* AddOptions()

该方法将Options模型中的几个核心类型作为服务注册到了指定的IServiceCollection对象之中，使可以使用IOptions<TOptions>、IOptionsMonitor<TOptions>以及IOptionsSnapshot<TOptions>等功能。

由于它们都是调用TryAdd方法进行服务注册的，所以我们可以在需要Options模式支持的情况下调用AddOptions方法，而不需要担心是否会添加太多重复服务注册的问题。

比如下面的Configure方法内部也会调用AddOptions()，所以这里可以省略这句。

```
    public static IServiceCollection AddOptions(this IServiceCollection services)
    {
      if (services == null)
        throw new ArgumentNullException(nameof (services));
      services.TryAdd(ServiceDescriptor.Singleton(typeof (IOptions<>), typeof (OptionsManager<>)));
      services.TryAdd(ServiceDescriptor.Scoped(typeof (IOptionsSnapshot<>), typeof (OptionsManager<>)));
      services.TryAdd(ServiceDescriptor.Singleton(typeof (IOptionsMonitor<>), typeof (OptionsMonitor<>)));
      services.TryAdd(ServiceDescriptor.Transient(typeof (IOptionsFactory<>), typeof (OptionsFactory<>)));
      services.TryAdd(ServiceDescriptor.Singleton(typeof (IOptionsMonitorCache<>), typeof (OptionsCache<>)));
      return services;
    }
```

* Configure<T>()

其实就是注册了一个单例的IConfigureOptions<TOptions>对象，其实现类型为ConfigureNamedOptions<TOptions>。

```
    public static IServiceCollection Configure<TOptions>(
      this IServiceCollection services,
      string name,
      Action<TOptions> configureOptions)
      where TOptions : class
    {
      if (services == null)
        throw new ArgumentNullException(nameof (services));
      if (configureOptions == null)
        throw new ArgumentNullException(nameof (configureOptions));
      services.AddOptions();
      services.AddSingleton<IConfigureOptions<TOptions>>((IConfigureOptions<TOptions>) new ConfigureNamedOptions<TOptions>(name, configureOptions));
      return services;
    }
```

## IOptions与IOptionsSnapshot

* IOptions<T>为Singleton
* IOptionsSnapshot为Scoped

## 利用IOptionsSnapshot服务读取具名Options

当读取的名称不存在时，也会实例化一个所有属性为默认值的对象并缓存。

## 结合配置系统

