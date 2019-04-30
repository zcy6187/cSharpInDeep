using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncAndMultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 异步
            //for (int i = 0; i < 5; i++)
            //{
            //    BaseAsync.PostAsyc();
            //}


            #endregion

            #region Parallel测试
            ParallelDemo pd = new ParallelDemo();
            //pd.ParallelInvokeMethod();
            //pd.ParallelForMethod();
            //pd.ParallelForEach();
            //pd.ParallelBreak();
            //pd.ConcurrentBagWithPallel();
            //pd.PLinqAsParallel();
            // pd.LookUpByTest();

            #endregion

            #region task测试
            TaskTest tt = new TaskTest();
            //tt.CreateNewTask();
            //tt.ShowTaskStatus();

            //tt.WaitAllTask();
            //tt.ConinueWith();
            //tt.MultiContinueWith();
            //tt.MultiLayerTask();
            //tt.MultiLayerReturnVal();
            tt.CascadeTasks();
            //tt.DealLockUseSpinLock();

            tt.CancelSingleTask();
            //tt.CancelMultiTask();

            #endregion

            #region Thread测试
            ThreadTest thT = new ThreadTest();
            //thT.TreadStartNoPram();
            //thT.ThreadStartHasPrams();
            //thT.UseThreadPool();
            //ThreadTest.DelegateBeginInvoke();

            #endregion

            #region winform多线程测试
            //MainForm mf = new MainForm();
            //mf.ShowDialog();

            #endregion

            Console.ReadKey();
        }
    }
}
