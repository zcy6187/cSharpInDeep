using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AppConfigOper
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 简单自定义对象
            //SimpleConfig sc = ConfigurationManager.GetSection("simple") as SimpleConfig;
            //Console.WriteLine("MinValue:" + sc.MinValue);
            //Console.WriteLine("Enable:" + sc.Enable);

            //此种用法，不能直接修改config文件
            //sc.MinValue = 1000;

            #endregion

            #region 系统内置结点读取
            //读写结点
            //CommonOper.ReadConfigNode();
            CommonOper.WriteConfigNode();
            //CommonOper.ReadConfigNode();

            //添加结点
            //CommonOper.AddConfigNode();
            //Console.WriteLine("OK");

            //读写自定义结点
            //CommonOper.ReadSelfDefNode();

            #endregion

            #region xml读写
            ////读
            //string xServerIp = XmlOper.ReadAppSettings("ServerIp");
            //Console.WriteLine(xServerIp);
            ////写
            //XmlOper.ModifyAppSettings("ServerIp", "alalala");
            //xServerIp = XmlOper.ReadAppSettings("ServerIp");
            //Console.WriteLine(xServerIp);

            //XmlOper.ModifyOrAddAppSettings("test","vall");
            #endregion

            Console.ReadKey();
        }
    }
}
