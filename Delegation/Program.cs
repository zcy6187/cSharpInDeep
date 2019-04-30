using System;
using static Delegation.DelegateBase;

namespace Delegation
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 委托基本测试
            DelegateBase delBase = new DelegateBase();
            int ret=delBase.InDelegate(1,3);
            Console.WriteLine(ret);
            // 代理有多种简化方式
            delBase.OutDelegate+=(string ss)=> { Console.WriteLine("main " + ss); };
            delBase.OutDelegate += DelMethod;
            delBase.OutDelegate("hehe");
            delBase.OutDelegate.Invoke("dada");
            // 定义在类内部的代理类，可以通过 using static的方法引用，不过最好不要，代码不够简明。
            // 建议类内部需要的代理就定义在内部，采用event对其操作，而其它对象都可以访问的代理，直接定义在命名空间下。
            DelegateBase.DelegateIn del = new DelegateBase.DelegateIn(Minus);
            // 可以通过+=、-=改变修改其代理内容；用法与代理类似
            delBase.EventDelegateIn += Minus;
            // 无法向调用委托那样，调用事件，执行其内部代理方法
            // delBase.EventDelegateIn(5, 3);
            // 事件只有事件所在类内部的方法才能执行
            delBase.ExeEvent();
            #endregion

            Console.ReadKey();
        }

        public static int Minus(int a, int b)
        {
            return b - a;
        }

        public static void DelMethod(string s)
        {
            Console.WriteLine("static " + s);
        }
    }
}
