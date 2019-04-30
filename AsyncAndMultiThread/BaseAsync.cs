using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncAndMultiThread
{
    /*使用delegate进行异步操作的基本步骤
     */
    delegate void AsyncCaller(int i);
    class BaseAsync
    {
        public static void PostAsyc()
        {
            AsyncCaller caller = new AsyncCaller(Foo);

            caller.BeginInvoke(1000, new AsyncCallback(FooCallBack), caller);
        }

        public static void Foo(int i)
        {
            PrintCurrThreadInfo("Foo");
            Thread.Sleep(i);
        }

        public static void PrintCurrThreadInfo(string name)
        {
            Console.WriteLine("Thread Id of " + name + " is: " + Thread.CurrentThread.ManagedThreadId + ", current thread is "
                + (Thread.CurrentThread.IsThreadPoolThread ? "" : "not ") + "thread pool thread.");
        }

        public static void FooCallBack(IAsyncResult ar)
        {
            PrintCurrThreadInfo("FooCallBack()");
            AsyncCaller caller = (AsyncCaller)ar.AsyncState;
            caller.EndInvoke(ar);
        }
    }
}
