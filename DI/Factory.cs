using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    /* 工厂方法实现IOC
     * 张承宇 2019/03/04
     * 1. 定义接口；
     * 2. 实现接口为具体的类；
     * 3. 工厂类中，用具体的方法，创建接口类；再定义一个方法，用于创建有相互依赖关系的实例；
     * 4. 继承工厂类，实现各种类型的类实例；
     */
    

    public class FoobarFactory : Factory
    {
        protected override IControllerActivator GetControllerActivator() { return null; }
    }


    public class Factory
    {
        public async Task StartAsync(Uri address)
        {
            var listener = GetWebLister();
            var activator = GetControllerActivator();
            var executor = GetControllerExecutor();
            var render = GetViewRender();
            await listener.ListenAsync(address);
            while (true)
            {
                var httpContext = await listener.ReceiveAsync();
                var controller = await activator.CreateControllerAsync(httpContext);
                try
                {
                    var view = await executor.ExecuteAsync(controller, httpContext);
                    await render.RendAsync(view, httpContext);
                }
                finally

                {
                    await activator.ReleaseAsync(controller);
                }
            }
        }
        protected virtual IWebLister GetWebLister() { return null; }
        protected virtual IControllerActivator GetControllerActivator() { return null; }
        protected virtual IControllerExecutor GetControllerExecutor() { return null; }
        protected virtual IViewRender GetViewRender() { return null; }
    }

    public interface IWebLister
    {
        Task ListenAsync(Uri address);
        Task<HttpContext> ReceiveAsync();
    }

    public interface IControllerActivator
    {
        Task<Controller> CreateControllerAsync(HttpContext httpContext);
        Task ReleaseAsync(Controller controller);
    }

    public class SingletonControllerActivator : IControllerActivator
    {
        public Task<Controller> CreateControllerAsync(HttpContext httpContext)
        {
            return null;
        }
        public Task ReleaseAsync(Controller controller) => Task.CompletedTask;
    }

    public interface IControllerExecutor
    {
        Task<View> ExecuteAsync(Controller controller, HttpContext httpContext);
    }

    public interface IViewRender
    {
        Task RendAsync(View view, HttpContext httpContext);
    }


    public class HttpContext
    {
    }
}
