using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent
{
    /*
     * 1、代理本身是一个类，定义的类内部的话，其他类也是可以使用该代理的，但必须使用 using static Deleagetion.DelegateBase的方式。
     * 2、建议其他类，不要使用类内部定义的代理；
     * 3、类内部定义的代理，必须实例化后才能被使用，即 new DelegateIn(MathOper);
     * 4、代理的实例化对象，用起来和普通的属性相似；而且可以通过+=，-=增加方法，非本类中的方法，也可以增加；调用者也可以直接使用代理的实体调用代理队列;
     * 5、事件是对代理的封装，使用方式代理的实体类，其不用实体化，直接通过+=，-=增加方法，非本类中的方法，也可以增加；调用者不能调用事件完成代理的执行，只有类本身的方法才可以。
     */

    public delegate void DelegateOut1(string s);
    public delegate int DelegateOut2(string s);
    public class DelegateBase
    {
        public delegate int DelegateIn(int x, int y);
        public DelegateIn InDelegate;
        public DelegateOut1 OutDelegate;
        public event DelegateIn EventDelegateIn;

        public DelegateBase()
        {
            this.InDelegate = new DelegateIn(MathOper1);
            this.OutDelegate = new DelegateOut1(MathOper3);
            this.OutDelegate += new DelegateOut1(MathOper4);
            this.OutDelegate += MathOper4;
            this.OutDelegate += (string str) => { Console.WriteLine("anonymity " + str); };

            EventDelegateIn += MathOper1;
        }

        public void ExeEvent()
        {
            Console.WriteLine(EventDelegateIn(10, 9));
        }

        private int MathOper1(int x, int y)
        {
            return x + y;
        }

        private int MathOper2(string str)
        {
            return str.Length;
        }

        private void MathOper3(string str)
        {
            Console.WriteLine(str);
        }

        private void MathOper4(string str)
        {
            Console.WriteLine(str.Length);
        }


    }
}
