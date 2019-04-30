using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpConsole
{
    class RefAndOut
    {
        public void RefMethod(ref int i)
        {
            i = i + 1;
        }

        public void OutMethod(out int i)
        {
            i = 100;
            i++;
        }
    }
}
