using System;

namespace BasicDataTypeConvertAndParser
{
    class Program
    {
        static void Main(string[] args)
        {
            #region int.parser
            //int a = -1;
            //int.TryParse("12",out a);
            //Console.WriteLine(a); // 12
            //float b = 2.3f;
            //int.TryParse(b.ToString(), out a); // 0
            //Console.WriteLine(a);
            //int.TryParse("2.3", out a); // 0
            //Console.WriteLine(a);
            //Console.WriteLine((int)b); // 2
            // Console.WriteLine((int)"123"); // 报错，无法运行
            #endregion

            #region double
            //double a = 23.37d;
            //double b = 23.13645634d;
            //Console.WriteLine(Math.Round(a,5)); // 2.37(不足5位的，不会补0)
            //Console.WriteLine(Math.Round(b,5)); // 2.13646-四舍五入
            //Console.WriteLine(Math.Floor(b)); // 23-向下取整
            //Console.WriteLine(Math.Ceiling(b)); // 24-向上取整
            //Console.WriteLine(b.ToString("0.00")); // 23.14-四舍五入
            //Console.WriteLine(string.Format("{0:N2}",b)); // 23.14-四舍五入
            #endregion
            Console.ReadKey();
        }
    }
}
