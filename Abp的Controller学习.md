# Abp的Controller学习

使用了ABP后，WebApiApplication继承的类发生了变化，所以初始化的资源要换地方写，下面我们创建一个使用ABP的项目看看变化。

创建MVC项目AbpMvcController。

然后引用Abp.Web.Mvc。

![1584601323644](D:\GitHub\AbpMvcController\1584601323644.png)

然后修改Global.asax如下：

```C#
using Abp.Web;
using AbpMvcController;
using System;
using System.Web;
[assembly: PreApplicationStartMethod(typeof(PreStarter), "Start")]
namespace AbpMvcController
{
    public class WebApiApplication : Abp.Web.AbpWebApplication<StartServiceModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
        } 
    }

    public static class PreStarter
    {
        public static void Start()
        { 
            WebApiApplication.AbpBootstrapper.PlugInSources.AddToBuildManager(); 
        }
    } 
}
```

然后在App_Start文件夹新建StartModule，StartModule继承 AbpModule。

因为创建的是MVC项目，所以我们添加相应的依赖。

```C#
[DependsOn(typeof(Abp.Web.AbpWebModule),typeof(AbpWebMvcModule))]
public class StartModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
        Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true; 
    }
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        AreaRegistration.RegisterAllAreas();
        RouteConfig.RegisterRoutes(RouteTable.Routes); 
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
    public override void PostInitialize()
    {
		GlobalConfiguration.Configuration.Filters.Add(new ExceptionFilter());
    }
    public override void Shutdown()
    {
    }
}
```

由于我们在Global.asa中删除了路由注册等资源，所以我们需要在StartModule中，再重写出来，如上述代码。

其中Filter采用在PostInitialize里直接添加的模式。

现在运行项目，项目成功启动。

即，ABP并没有改变MVC的逻辑。

ABP提供了一个AbpController，相当于对对MVC的Controller的扩展。

我们修改继承，项目依然可以启动运行，如下图：

![1584603000445](D:\GitHub\AbpMvcController\1584603000445.png)

编写一个AuthorizeAttribute，然后放到Controller上，测试断点AuthorizeAttribute的AuthorizeCore可以被断点命中，即授权特性还可以使用MVC的，不过ABP也提供了AbpMvcAuthorize和一系列ABP的授权模式。

```C#
[CustomAuthorize]
public class HomeController : AbpController
{
    public ActionResult Index()
    {
        ViewBag.Title = "Home Page";

        return View();
    }
}
```

总体上来说，ABP对WebApi提供了比较高级的功能，动态创建WebApi，简化了代码。

但对Controller并没有提供太高级的功能，就是一些简单的扩展和再封装。

那这种学习MVC的基础上，再去学习ABP的方言，说实话有点累，所以，完全可以使用大家自己项目原有的MVC结构+ABP的WebApi来开发；这样效果是最好的。

ABP的日志：ABP的日志因为没办法扩展到没引用ABP的类库里，所以意义不大，还是自己封装日志比较好。

ABP提供的EF：ABP提供的EF因其使用和配置太过繁琐，支持的功能又不是特别全面；且设计理念上，只有部分DDD的思想，然后还很难扩展，所以基本上可以抛弃了，因为根本没法和业务做最完美的结合。

ABP的依赖注入：ABP的依赖注入也是依赖Castle，那就是说我们完全可以直接使用Castle，效果比用他封装的好，如果项目可以使用Core框架开发，那直接使用Core的依赖注入就可以了，比ABP的强太多了。