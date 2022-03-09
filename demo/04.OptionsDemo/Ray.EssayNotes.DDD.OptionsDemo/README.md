# Options�����÷�

Optionsģʽ�Ƕ�����ע��ģʽ������ģʽ�����ϣ���������ʹ���ǵ���Ŀ���õ�ʵ��ģ�黯������ϡ�

## ����IOptions�����ȡ�Ǿ���Options

* AddOptions()

�÷�����Optionsģ���еļ�������������Ϊ����ע�ᵽ��ָ����IServiceCollection����֮�У�ʹ����ʹ��IOptions<TOptions>��IOptionsMonitor<TOptions>�Լ�IOptionsSnapshot<TOptions>�ȹ��ܡ�

�������Ƕ��ǵ���TryAdd�������з���ע��ģ��������ǿ�������ҪOptionsģʽ֧�ֵ�����µ���AddOptions������������Ҫ�����Ƿ�����̫���ظ�����ע������⡣

���������Configure�����ڲ�Ҳ�����AddOptions()�������������ʡ����䡣

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

��ʵ����ע����һ��������IConfigureOptions<TOptions>������ʵ������ΪConfigureNamedOptions<TOptions>��

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

## IOptions��IOptionsSnapshot

* IOptions<T>ΪSingleton
* IOptionsSnapshotΪScoped

## ����IOptionsSnapshot�����ȡ����Options

����ȡ�����Ʋ�����ʱ��Ҳ��ʵ����һ����������ΪĬ��ֵ�Ķ��󲢻��档

## �������ϵͳ

