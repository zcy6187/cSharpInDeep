/* 抽象工厂实现IOC
 * 抽象工厂只实现类实例的创建方法，具体的创建过程，由使用者自己完成。
 * 张承宇 2019/03/04
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface ISFactory
    {
        IWebLister GetWebLister();
        IControllerActivator GetControllerActivator();
        IControllerExecutor GetControllerExecutor();
        IViewRender GetViewRender();
    }

    public class MvcEngineFactory : ISFactory
    {
        public IWebLister GetWebLister() { return null; }
        public IControllerActivator GetControllerActivator() { return null; }
        public IControllerExecutor GetControllerExecutor() { return null; }
        public IViewRender GetViewRender() { return null; }
    }

    public class MvcEngine
    {
        public ISFactory EngineFactory { get; }
        public MvcEngine(ISFactory engineFactory = null)
        => EngineFactory = engineFactory ?? new MvcEngineFactory();

        public async Task StartAsync(Uri address)
        {
            var listener = EngineFactory.GetWebLister();
            var activator = EngineFactory.GetControllerActivator();
            var executor = EngineFactory.GetControllerExecutor();
            var render = EngineFactory.GetViewRender();
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

    }
}
