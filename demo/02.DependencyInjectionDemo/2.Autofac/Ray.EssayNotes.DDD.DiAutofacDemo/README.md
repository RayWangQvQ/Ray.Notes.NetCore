# .Net Core集成Autofac

## 集成

## Named命名注册

如Test01

MyService和MyOtherService都注册到容器中，MyOtherService标注了命名“other”

在接口中，注入IEnumerable<IMyService>时，不会讲两个实例都注册进，第二个被命名的只能根据命名获取解析到。

## 属性注入

## AOP拦截器