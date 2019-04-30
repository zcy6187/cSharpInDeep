using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStandard
{
    /* 常用的类的书写规范
     张承宇 2017-09-20
         */

    class StandardClass
    {
        #region 属性
        //私有成员使用camel，以_开头
        private string _name;
        //只读常量使用Pascal
        private readonly double Pi = 3.1415926;

        //自定义委托以EventHandler结尾(delegate不管是什么访问级别，对外都不可访问)
        public delegate void GreatPeopleEventHandler(object sender, EventArgs e);
        //事件以委托去掉EventHandler来命名（event是delegate的封装，如果event是public，delegate也必须是public）
        public event GreatPeopleEventHandler GreatPeople;

        //属性采用Pascal命名
        public string Name
        {
            get
            {
                return _name + " get";
            }
            private set   //访问器也要自己的修饰符
            {
                _name = value + "set";
            }
        }
        #endregion

        #region 方法
        //方法使用Pascal命名法，采用动宾结构
        //参数使用camel命名法
        public void ShowPeople(string greater)
        {

        }
        #endregion
    }
}
