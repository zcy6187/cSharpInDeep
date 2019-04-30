using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace COM
{
    /*DllImport的基本用法
        张承宇 2017-09-20 
    */
    class COMP
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);
    }
}
