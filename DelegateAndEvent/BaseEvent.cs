using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent
{
    /*
     事件的基本用法
     张承宇 2017-09-20
         */

    class BaseEvent
    {
        public delegate void ShowGreaterEventHandler(string name);

        public event ShowGreaterEventHandler ShowGreater;


        public void ExeEvent(string name)
        {
            ShowGreater(name);
        }
    }

    class Greater
    {
        public void GreatInEnglish(string name)
        {
            Console.WriteLine("Hello " + name);
        }

        public void GreatInChinese(string name)
        {
            Console.WriteLine("你好 " + name);
        }
    }
}
