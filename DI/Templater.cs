using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    /* 模板方法实现IOC
     * 模板方法——将一个算法，分成多个步骤，分别实现
     * 张承宇 2019/03/04
     */
    
    /* 继承基础模板类，重写局部模板方法和装配流程，完成特定的模板算法 */
    public class FoobarTemplater : Templater
    {
        protected override Task<Controller> CreateControllerAsync(Request request)
        {
            
            return null;
        }
    }

    /* 基类中定义局部模板方法及装配流程 */
    public class Templater
    {
        public async Task StartAsync(Uri address)
        {
            await ListenAsync(address);
            while (true)
            {
                var request = await ReceiveAsync();
                var controller = await CreateControllerAsync(request);
                var view = await ExecuteControllerAsync(controller);
                await RenderViewAsync(view);
            }
        }
        protected virtual Task ListenAsync(Uri address){return null;}
        protected virtual Task<Request> ReceiveAsync() { return null; }
        protected virtual Task<Controller> CreateControllerAsync(Request request) { return null; }
        protected virtual Task<View> ExecuteControllerAsync(Controller controller) { return null; }
        protected virtual Task RenderViewAsync(View view) { return null; }
    }

    public class Controller
    {
    }

    public class Request
    {
    }

    public class View
    {
    }
}
