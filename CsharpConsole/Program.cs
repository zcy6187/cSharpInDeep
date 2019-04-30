using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //静态数据
            /*A有静态初始化函数，所以先初始化A，A在初始化时，碰到B，但B没有初始化函数，所以A取B的默认值*/
            //Console.WriteLine("X={0},Y={1}", A.X, B.Y);  //X=1,Y=2

            //ref和out的区别，ref在方法内不用赋初值，而out在方法中使用时，必须赋初值。
            //int i = 0;
            //RefAndOut ro = new RefAndOut();
            //ro.RefMethod(ref i);
            //Console.WriteLine(i);   //1
            //ro.RefMethod(ref i);
            //Console.WriteLine(i);   //2
            //ro.RefMethod(ref i);
            //Console.WriteLine(i);   //3
            //ro.OutMethod(out i);
            //Console.WriteLine(i);   //101

            #region As和Is测试
            // AsAndIs.BaseTester(); 
            #endregion

            #region  try/catch测试
            int ret = TryCatch.Test2();
            Console.WriteLine($"ret {ret}");

            #endregion


            Console.ReadKey();

        }
    }
}