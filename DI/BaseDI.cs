using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    /* 容器的简单使用方式
     * 假如有一个Cat容器，其中定义了各种类及其实例的生成方式，及类之间的依赖关系；
     * 业务逻辑中，只使用该容器即可，用该容器生成各种实例。
     */

    public class MvcUseDI
    {
        public Cat Cat { get; }
        public MvcUseDI(Cat cat) => Cat = cat;

        public async Task StartAsync(Uri address)
        {
            var listener = Cat.GetService<IWebLister>();
            var activator = Cat.GetService<IControllerActivator>();
            var executor = Cat.GetService<IControllerExecutor>();
            var render = Cat.GetService<IViewRender>();
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

    public class Cat
    {
        public T GetService<T>()
        {
            return default(T);
        }
    }
}
