using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncAndMultiThread
{
    class ThreadTest
    {
        #region Thread的基本使用方法
        /// <summary>
        /// 无参调用
        /// </summary>
        public void TreadStartNoPram()
        {
            /*ThreadStart是一个没有参数，没有返回值的委托
             *如果想传递参数，可以将委托的方法封装在类中，类的属性当做方法参数。通过 类.方法（ThreadTest.ThreadStartNoPram） 调用
             */
            ThreadStart ts = new ThreadStart(Calculate);
            Thread td = new Thread(ts);
            td.Start();
        }

        /// <summary>
        /// 有参调用
        /// </summary>
        public void ThreadStartHasPrams()
        {
            /*ParameterizedThreadStart 是一个有Object类型参数的委托，无返回值
             *如果需要传递多个参数，可以将参数封装到类中
             */
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(CalculateHasPram);
            Thread thread = new Thread(threadStart);
            thread.Start(0.9);
        }

        /// <summary>
        /// 无参计算式
        /// </summary>
        /// <param name="arg"></param>
        private void Calculate()
        {
            double diameter = 1.5;
            Console.Write("The perimeter Of Circle with a Diameter of {0} is {1}", diameter, diameter * Math.PI);
        }

        /// <summary>
        /// 有参计算式
        /// </summary>
        /// <param name="arg"></param>
        public void CalculateHasPram(object arg)
        {
            double diameter = (double)arg;
            Console.Write("The perimeter Of Circle with a Diameter of {0} is {1}", diameter, diameter * Math.PI);
        }

        #endregion

        #region 委托的调用方法
        /*委托的BeginInvoke和EndInvoke可以获取参数，但是委托本身必须被EndInvoke方法可见
         * BeginInvoke和EndInvoke在同一个线程中，可以用Thread.CurrentThread.ManagedThreadId或
         * AppDomain.GetCurrentThreadId().ToString获取当前线程的ID
         */

        delegate double CalculateMethod(double obj);
        static CalculateMethod cm;

        public static void DelegateBeginInvoke()
        {
            cm = new CalculateMethod(Calculate);
            Console.WriteLine("委托开始执行");
            cm.BeginInvoke(5, new AsyncCallback(TaskFinished), null);
            cm.BeginInvoke(7, new AsyncCallback(TaskFinished), null);
            cm.BeginInvoke(11, new AsyncCallback(TaskFinished), null);
            Thread.Sleep(2000);
            Console.WriteLine("委托执行结束");
        }

        public static void TaskFinished(IAsyncResult ret)
        {
            double val = cm.EndInvoke(ret);
            Console.WriteLine("当前返回线程ID：" + Thread.CurrentThread.ManagedThreadId + "委托的返回结果：" + val);
        }

        public static double Calculate(double diameter)
        {
            double ret = diameter * Math.PI;
            Thread.Sleep(2000);
            Console.WriteLine("当前执行线程ID：" + Thread.CurrentThread.ManagedThreadId + "The perimeter Of Circle with a Diameter of {0} is {1}", diameter, ret);
            return ret;
        }
        #endregion

        #region 线程池的用法
        /*线程很耗费资源，线程池则可以通过调度，减少资源的耗费
         *每一个进程都有一个线程池,线程池的默认大小是25,我们可以通过SetMaxThreads方法来设置其最大值.
         */
        public void UseThreadPool()
        {
            WaitCallback wc = new WaitCallback(CalculateNoReturn);

            ThreadPool.QueueUserWorkItem(wc, 5.0);
            ThreadPool.QueueUserWorkItem(wc, 3.0);
            ThreadPool.QueueUserWorkItem(wc, 2.0);

        }


        public void CalculateNoReturn(object diameter)
        {
            double ret = (double)diameter * Math.PI;
            Console.WriteLine("The perimeter Of Circle with a Diameter of {0} is {1}", diameter, ret);
        }

        #endregion
    }
}
