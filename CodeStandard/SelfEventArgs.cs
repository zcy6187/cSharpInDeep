using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStandard
{
    /*自定义参数最好以EventArgs结尾
     张承宇 2017-09-20
         */
    class SelfEventArgs : EventArgs
    {
        public string Pram1 { get; set; }

        public SelfEventArgs(string pram)
        {
            Pram1 = pram;
        }
    }
}
