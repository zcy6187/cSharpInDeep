using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAndMultiThread
{
    /*一个Task 不一定是一个线程，这个类似于线程池的概念
     *可能会创建一个新的线程，也可能不会
     */
    class TaskTest
    {
        #region 创建Task
        /*Factory创建的Task直接运行
         *构造函数创建的Task，需要用Start重新运行
         */
        public void CreateNewTask()
        {
            var task1 = new Task(() =>
            {
                Console.WriteLine("Task1 运行！");
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task2 运行！");
            });

            task1.Start();
        }

        /*Task在不同的运行阶段有不同的状态*/
        public void ShowTaskStatus()
        {
            var task1 = new Task(() =>
            {
                Console.WriteLine("Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Finish");
            });

            Console.WriteLine("Before Start:" + task1.Status);

            task1.Start();
            Console.WriteLine("After Start:" + task1.Status);

            task1.Wait();
            Console.WriteLine("After Wait:" + task1.Status);


        }

        #endregion

        #region Task调度
        /// <summary>
        /// 等所有的task执行完毕
        /// </summary>
        public void WaitAllTask()
        {
            var task1 = new Task(() =>
            {
                Console.WriteLine("Task 1 Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 1 Finish");
            });
            var task2 = new Task(() =>
            {
                Console.WriteLine("Task 2 Begin");
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Task 2 Finish");
            });

            task1.Start();
            task2.Start();
            Task.WaitAll(task1, task2);  //等待task1和task2都完成
            Console.WriteLine("All task finished!");
        }

        /// <summary>
        /// 创建一个在Task结束后的延时任务，多用来传递一个返回值
        /// </summary>
        public void ConinueWith()
        {
            var task1 = new Task(() =>
            {
                Console.WriteLine("Task 1 Begin");
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Task 1 Finish");
            });
            var task2 = new Task(() =>
            {
                Console.WriteLine("Task 2 Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 2 Finish");
            });


            task1.Start();
            task2.Start();
            var result = task1.ContinueWith<string>(task =>
            {
                Console.WriteLine("task1 finished!");
                return "This is task result!";
            });

            Console.WriteLine(result.Result.ToString());

        }

        /// <summary>
        /// ContinueWith任务可以有多个,ContinueWith的返回值，作为result可以传递到下个ContinueWith中
        /// </summary>
        public void MultiContinueWith()
        {
            var SendFeedBackTask = Task.Factory.StartNew(() => { Console.WriteLine("Get some Data!"); })
                            .ContinueWith<bool>(s => { return false; })
                            .ContinueWith<string>(r =>
                            {
                                if (r.Result)
                                {
                                    return "Finished";
                                }
                                else
                                {
                                    return "Error";
                                }
                            });
            Console.WriteLine(SendFeedBackTask.Result);
        }

        #endregion

        #region 多层Task
        public void MultiLayerTask()
        {
            var pTask = Task.Factory.StartNew(() =>
            {
                var cTask = Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Childen task finished!");
                });
                Console.WriteLine("Parent task finished!");
            });
            pTask.Wait();  //等待线程执行完毕
            //由于父线程中创建的线程 与父线程是独立的，所以pTask执行完后，就会执行下边的语句
            Console.WriteLine("Flag");
        }

        /**
         任务创建选项
         TaskCreationOptions  枚举项如下，此处应用了 AttachedToParent
            // 默认1.None 
            // 将任务放入全局队列中（任务将以先到先出的原则被执行） 2.PreferFairness
            // 告诉TaskScheduler，线程可能要“长时间运行” 3.LongRunning
            // 将一个Task与它的父Task关联  4.AttachedToParent
            // Task以分离的子任务执行   5.DenyChildAttach
            // 创建任务的执行操作将被视为TaskScheduler.Default默认计划程序   6.HideScheduler
            // 强制异步执行添加到当前任务的延续任务 7.RunContinuationsAsynchronously
         * **/

        public void MultiLayerAttachedTask()
        {
            var pTask = Task.Factory.StartNew(() =>
            {
                var cTask = Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Childen task finished!");
                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine("Parent task finished!");
            });
            pTask.Wait();  //等待线程执行完毕
            //使用TaskCreationOptions.AttachedToParent使得父线程和子线程合并，所以必须父子线程都执行完后，才会执行下边的语句
            Console.WriteLine("Flag");
        }

        /// <summary>
        /// 多Task运行，并调用Task的返回值
        /// </summary>
        public void MultiLayerReturnVal()
        {
            /*任务2和任务3要等待任务1完成后，取得任务1的结果，然后开始执行。
             * 任务4要等待任务2完成，取得其结果才能执行，
             * 最终任务3和任务4都完成了，合并结果，任务完成。
             *
             * 多个task之间有顺序依赖时，需要使用Task.Wait()，先等依赖的Task执行完毕
             */

            Task.Factory.StartNew(() =>
            {
                var t1 = Task.Factory.StartNew<int>(() =>
                {
                    Console.WriteLine("Task 1 running...");
                    return 1;
                });
                t1.Wait(); //等待任务一完成
                var t3 = Task.Factory.StartNew<int>(() =>
                {
                    Console.WriteLine("Task 3 running...");
                    return t1.Result + 3;
                });
                var t4 = Task.Factory.StartNew<int>(() =>
                {
                    Console.WriteLine("Task 2 running...");
                    return t1.Result + 2;
                }).ContinueWith<int>(task =>
                {
                    Console.WriteLine("Task 4 running...");
                    return task.Result + 4;
                });
                Task.WaitAll(t3, t4);  //等待任务三和任务四完成
                var result = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Task Finished! The result is {0}", t3.Result + t4.Result);
                });
            });
        }

        /* TaskContinuationOptions（任务延续选项）
         * 如果要指定不同任务之间的级联关系，则需要使用 TaskContinuationOptions
         * // 默认   1.None
            // 将任务放入全局队列中（任务将以先到先出的原则被执行）  2.PreferFairness
            // 告诉TaskScheduler，线程可能要“长时间运行”，需要为任务创建一个专用线程，而不是排队让线程池线程来处理
            3.LongRunning
            // 将一个Task与它的父Task关联  4.AttachedToParent
            // 希望执行第一个Task的线程，执行ContinueWith任务  5.ExecuteSynchronously
            // 第一个任务没有完成，执行后续任务  6.NotOnRanToCompletion
            // 第一个任务没有失败，执行后续任务  7.NotOnFaulted
            // 第一个任务没有取消，执行后续任务  8.NotOnCanceled
            // 只有当第一个任务取消，执行后续任务  9.OnlyOnCanceled
            // 只有当第一个任务失败，执行后续任务  10.OnlyOnFaulted
            // 只有当第一个任务完成，执行后续任务11.OnlyOnRanToCompletion
             */

        /// <summary>
        /// 串联任务，任务1执行后，任务2才执行
        /// </summary>
        public void CascadeTasks()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = new Task<int>(num =>
            {
                return (int)num + 1;
            }, 100, cts.Token);

            Task taskContinue = task.ContinueWith(c =>
            {
                Console.WriteLine("任务Task被取消了");
            }, TaskContinuationOptions.OnlyOnCanceled);  //只有当第一个任务取消了，才会执行该任务。

            task.Start();
            cts.Cancel();
            taskContinue.Wait();
            Console.ReadKey();
        }

        #endregion

        #region 取消Task
        /*CancellationTokenSource 专门用于取消线程
        *创建Task时，为Task赋予一个CancellationTokenSource.Token 
        *在task中，询问Token.IsCancellationRequested，如果为true，则改task已经被取消
        */
        public void CancelSingleTask()
        {
            //一旦将该任务设置为toukenSource.Cancel()，任务就会取消；
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var task = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    Thread.Sleep(1000);
                    //如果为true，则task被取消，就用return返回，不再执行
                    //如果不加这个判断，task将会被一直执行
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Mission Canceled！");
                        return;
                    }
                    Console.WriteLine("当前执行到：" + i);
                }
            }, token);

            token.Register(() => { Console.WriteLine("Task Canceled"); });

            //Console.WriteLine("按任意键取消任务");
            //Console.ReadKey();
            //tokenSource.Cancel();

            string inputChar = Console.ReadKey().KeyChar.ToString();
            if (inputChar == "1")
            {
                tokenSource.Cancel();
                Console.WriteLine("task1的状态：" + task.Status);
            }
        }

        public void CancelMultiTask()
        {
            var tokenSource1 = new CancellationTokenSource();
            var token1 = tokenSource1.Token;
            var task1 = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    Thread.Sleep(1000);
                    if (token1.IsCancellationRequested)
                    {
                        Console.WriteLine("Abort mission success!");
                        return;
                    }
                    Console.WriteLine("task1正在执行:" + i);
                }
            }, token1);

            var tokenSource2 = new CancellationTokenSource();
            var token2 = tokenSource2.Token;
            var task2 = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    if (token2.IsCancellationRequested)
                    {
                        Console.WriteLine("Abort mission success!");
                        return;
                    }
                    Console.WriteLine("task2正在执行:" + i);
                }
            }, token2);

            token1.Register(() =>
            {
                Console.WriteLine("Press 1 to Cancel task1");
            });

            token2.Register(() =>
            {
                Console.WriteLine("Press 2 to Cancel task2");
            });

            Console.WriteLine("按键取消任务！");
            string inputChar = Console.ReadKey().KeyChar.ToString();
            while (true)
            {
                if (inputChar != "1" && inputChar != "2")
                {
                    break;
                }
                switch (inputChar)
                {
                    case "1":
                        tokenSource1.Cancel();
                        Console.WriteLine("task1 的状态：" + task1.Status);
                        break;
                    case "2":
                        tokenSource2.Cancel();
                        Console.WriteLine("task2 的状态：" + task2.Status);
                        break;
                }
                inputChar = Console.ReadKey().KeyChar.ToString();
            }

        }

        #endregion

        #region 异常处理
        /// <summary>
        /// 多线程中的异常处理
        /// </summary>
        public void DealException()
        {
            /*AggregateException 是异常的集合
             *多个线程就会有多个异常，此处需要遍历异常的集合，对特定异常进行处理
             *处理线程异常，还可以通过线程的状态，如：IsCompleted, IsFaulted, IsCancelled,Exception等等
             */
            try
            {
                var pTask = Task.Factory.StartNew(() =>
                {
                    var cTask = Task.Factory.StartNew(() =>
                    {
                        System.Threading.Thread.Sleep(2000);
                        throw new Exception("cTask Error!");
                        Console.WriteLine("Childen task finished!");
                    });
                    throw new Exception("pTask Error!");
                    Console.WriteLine("Parent task finished!");
                });

                pTask.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (Exception inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.Message);
                }
            }
            Console.WriteLine("Flag");
        }

        #endregion

        #region 处理死锁
        /*WaitAll中可以设定最大等待时间，可以在时间耗尽后处理异常
         * Lock和Monitor可以处理
         * .NET4.0后，加入了SpinLock，是代码更容易写
         */
        public void DealLock()
        {
            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 1 Start running...");
                while (true) //此处会造成死锁，永远执行不完
                {
                    System.Threading.Thread.Sleep(1000);
                }
                Console.WriteLine("Task 1 Finished!");
            });
            tasks[1] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 2 Start running...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 2 Finished!");
            });

            Task.WaitAll(tasks, 5000);  //使用最大执行时间，可以在时间耗尽时，对死锁问题进行状态处理
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i].Status != TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Task {0} Error!", i + 1);
                }
            }
        }

        public void DealLockUseSpinLock()
        {
            //创建一个互斥锁，并且循环，直到改锁可用为止
            SpinLock slock = new SpinLock(false);
            long sum1 = 0;
            long sum2 = 0;
            Parallel.For(0, 100000, i =>
            {
                sum1 += i;
            });

            Parallel.For(0, 100000, i =>
            {
                bool lockTaken = false;
                try
                {
                    //尝试获取锁，获取到锁后，将ref项设为true
                    slock.Enter(ref lockTaken);
                    sum2 += i;
                }
                finally
                {
                    if (lockTaken)
                    {
                        //释放锁
                        slock.Exit(false);
                    }
                }
            });

            Console.WriteLine("Num1的值为:{0}", sum1);
            Console.WriteLine("Num2的值为:{0}", sum2);
        }

        #endregion

        #region  TaskFactory
        /*
         Task.Factory.StartNew()是对TaskFactory的简写
             */
        public void TestTaskFactory()
        {
            TaskFactory<DateTime> factory = new TaskFactory<DateTime>();

            Task<DateTime>[] tasks = new Task<DateTime>[]
            {
             factory.StartNew(() =>
            {
                return DateTime.Now.ToUniversalTime();
            }),

            factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return DateTime.Now.ToUniversalTime();
            }),

            factory.StartNew(() =>
            {
                return DateTime.Now.ToUniversalTime();
            })
            };

            StringBuilder sb = new StringBuilder();
            foreach (Task<DateTime> task in tasks)
                sb.AppendFormat("{0}\t", task.Result);

            Console.WriteLine(sb.ToString());
        }
        #endregion
    }
}
