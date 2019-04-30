using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAndMultiThread
{
    /*在.net 4.0中，微软给我们提供了一个新的命名空间：System.Threading.Tasks。
     * Parallel 常用 Invoke/For/Foreach
     */
    class ParallelTest
    {
    }


    public class ParallelDemo
    {
        /*stopWatch是System.Diagnostics
         * 
         */
        private Stopwatch stopWatch = new Stopwatch();

        public static void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
        }
        public void Run2()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
        }

        /// <summary>
        /// Invoke 测试
        /// </summary>
        public void ParallelInvokeMethod()
        {
            stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Restart();
            Run1();
            Run2();
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
        }

        /// <summary>
        /// Parallel中的异常捕获
        /// </summary>
        public void ParallelInvokeException()
        {
            //普通的Exception不能够捕获异常，AggregateException才能够捕获异常
            stopWatch.Start();
            try
            {
                Parallel.Invoke(Run1, Run2);
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            try
            {
                Run1();
                Run2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
        }

        /// <summary>
        /// For测试
        /// </summary>
        public void ParallelForMethod()
        {
            stopWatch.Start();

            //当数据量比较小时，正常的for（i=1000，j=6000），for的运行效率>Parallel.for
            //当数据量比较小时，正常的for（i=10000，j=60000），for的运行效率<Parallel.for
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum++;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("Parallel rum " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
              {
                  for (int i = 0; i < 60000; i++)
                  {
                      int sum = 0;
                      sum++;
                  }
              });
            stopWatch.Stop();
            Console.WriteLine("Parallel rum " + stopWatch.ElapsedMilliseconds + " ms.");

        }

        /// <summary>
        /// For测试，遇到需要锁定资源时，For>>Parallel.For
        /// </summary>
        public void ParallelForMethodLock()
        {
            var obj = new Object();
            long num = 0;
            ConcurrentBag<long> bag = new ConcurrentBag<long>();

            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    //int sum = 0;
                    //sum += item;
                    num++;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    //int sum = 0;
                    //sum += item;
                    lock (obj)
                    {
                        num++;
                    }
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");

        }

        /// <summary>
        /// For测试
        /// </summary>
        public void ParallelForOrder()
        {
            //Parallel.For的顺序不可预估
            Parallel.For(0, 100, i =>
              {
                  Console.WriteLine(i);
              });
        }

        /// <summary>
        /// ForEach测试
        /// </summary>
        public void ParallelForEach()
        {
            //Parallel.ForEach的顺序不可预期
            List<int> list = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(i);
            }

            Parallel.ForEach(list, item =>
             {
                 Console.WriteLine(item);
             });
        }

        /// <summary>
        /// 中断Parallel
        /// </summary>
        public void ParallelBreak()
        {
            //线程安全的无序集合
            ConcurrentBag<int> conCount = new ConcurrentBag<int>();

            stopWatch.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (conCount.Count == 300)
                {
                    //state使用的是，Parallel提供的一个ParallelLoopState参数，可以用来退出当前循环
                    //Stop：当前执行完毕，立即退出，所以执行301次，不加return的话，执行302次
                    //Break：如果执行到100，会把100以下的数据全部都执行完才会结束，所以循环次数不固定

                    state.Break();
                    //state.Stop();
                    //return;
                }
                conCount.Add(i);
            });
            stopWatch.Stop();
            Console.WriteLine(conCount.Count);
            Console.WriteLine("共耗时：" + stopWatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// 线程安全的集合
        /// </summary>
        public void ConcurrentBagWithPallel()
        {
            //对于多线程来说，List<T>是线程不安全的，因为任何程序都可以访问，所以并行的结果并不确定，
            //本意加入10000，但List中不一定有10000个（目前测试环境下是10000个）
            //CoucurrentBag<T>是线程安全的，可以Parallel.For中可以保证数据准确添加
            //CoucurrentBag<T>中的数据顺序是随机的

            List<int> commonList = new List<int>();
            Parallel.For(0, 10000, item =>
            {
                commonList.Add(item);
            });
            Console.WriteLine("CommonList's count is {0}", commonList.Count());

            ConcurrentBag<int> conList = new ConcurrentBag<int>();
            Parallel.For(0, 10000, item =>
            {
                conList.Add(item);
            });
            Console.WriteLine("ConcurrentBag's count is {0}", conList.Count());

            int n = 0;
            foreach (var item in conList)
            {
                Console.WriteLine("ConcurrentBag 序号：{0},值为：{1}", n, item);
                if (n > 10)
                {
                    break;
                }
                n++;
            }

            //List<T>中的数据是顺序的
            n = 0;
            foreach (var item in commonList)
            {
                Console.WriteLine("CommonList 序号：{0},值为：{1}", n, item);
                if (n > 100)
                {
                    break;
                }
                n++;
            }

        }

        /// <summary>
        /// 可以对集合类使用AsParallel方法，实现多线程遍历，加快linq查询
        /// </summary>
        public void PLinqAsParallel()
        {
            Stopwatch sw = new Stopwatch();
            List<Custom> customs = new List<Custom>();
            for (int i = 0; i < 2000000; i++)
            {
                customs.Add(new Custom() { Name = "Jack", Age = 21, Address = "NewYork" });
                customs.Add(new Custom() { Name = "Jime", Age = 26, Address = "China" });
                customs.Add(new Custom() { Name = "Tina", Age = 29, Address = "ShangHai" });
                customs.Add(new Custom() { Name = "Luo", Age = 30, Address = "Beijing" });
                customs.Add(new Custom() { Name = "Wang", Age = 60, Address = "Guangdong" });
                customs.Add(new Custom() { Name = "Feng", Age = 25, Address = "YunNan" });
            }

            sw.Start();
            var result = customs.Where<Custom>(c => c.Age > 26).ToList();
            sw.Stop();
            Console.WriteLine("Linq result is {0}.", result.Count);
            Console.WriteLine("Linq time is {0}.", sw.ElapsedMilliseconds);

            sw.Restart();
            sw.Start();
            //AsParallel方法实现并发linq查询
            var result2 = customs.AsParallel().Where<Custom>(c => c.Age > 26).ToList();
            sw.Stop();
            Console.WriteLine("Parallel Linq result is {0}.", result2.Count);
            Console.WriteLine("Parallel Linq time is {0}.", sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// LookUp将结果变为一个只读序列，大数据量时，速度会比较快
        /// </summary>
        public void LookUpByTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            List<Custom> customs = new List<Custom>();
            for (int i = 0; i < 2000000; i++)
            {
                customs.Add(new Custom() { Name = "Jack", Age = 21, Address = "NewYork" });
                customs.Add(new Custom() { Name = "Jime", Age = 26, Address = "China" });
                customs.Add(new Custom() { Name = "Tina", Age = 29, Address = "ShangHai" });
                customs.Add(new Custom() { Name = "Luo", Age = 30, Address = "Beijing" });
                customs.Add(new Custom() { Name = "Wang", Age = 60, Address = "Guangdong" });
                customs.Add(new Custom() { Name = "Feng", Age = 25, Address = "YunNan" });
            }

            stopWatch.Restart();
            var groupByAge = customs.GroupBy(item => item.Age).ToList();
            foreach (var item in groupByAge)
            {
                Console.WriteLine("Age={0},count = {1}", item.Key, item.Count());
            }
            stopWatch.Stop();

            Console.WriteLine("Linq group by time is: " + stopWatch.ElapsedMilliseconds);


            stopWatch.Restart();
            //ToLookU功能和GoupBy类似，但是将集合变成了只读集合，所以速度会快一点
            var lookupList = customs.ToLookup(i => i.Age);
            foreach (var item in lookupList)
            {
                Console.WriteLine("LookUP:Age={0},count = {1}", item.Key, item.Count());
            }
            stopWatch.Stop();
            Console.WriteLine("LookUp group by time is: " + stopWatch.ElapsedMilliseconds);
        }
    }

    public class Custom
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
