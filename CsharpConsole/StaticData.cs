
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpConsole
{
    public class StaticData
    {
        public static string Name = "static";

        static StaticData()
        {
            Name = "Struct change";
        }

        public StaticData(string ss)
        {
            Name = ss;
        }
    }


    public class A
    {
        public static int X;
        static A()
        {
            X = B.Y + 1;
        }
    }
    public class B
    {
        public static int Y = A.X + 1;
        B(int c)
        { Y = c; }
    }
}
