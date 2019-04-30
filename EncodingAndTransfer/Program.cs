using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodingAndTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 字符串与十六进制互转
            string tempStr = "我是中国 人";
            string hexStr = Transfer.BitToString(Encoding.UTF8.GetBytes(tempStr));
            Console.WriteLine(hexStr);
            byte[] byteArray = Transfer.FromBitString(hexStr);
            Console.WriteLine(Encoding.UTF8.GetString(byteArray));
            string str = string.Empty;
            foreach (var b in byteArray)
            {
                str += b.ToString() + "-";
            }
            Console.WriteLine(str);
            #endregion


            Console.ReadKey();
        }
    }
}
