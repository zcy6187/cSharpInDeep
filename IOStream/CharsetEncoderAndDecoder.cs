using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOStream
{
    /*字符的编解码测试
     */
    class CharsetEncoderAndDecoder
    {
        public static void BasicTest()
        {
            //英文字符串
            string str_en = "Welcome to the Encoding world.";
            //简体中文
            string str_cn = "欢迎来到编码世界！";
            //繁体中文
            string str_tw = "歡迎來到編碼世界";

            Encoding defaultEncoding = Encoding.Default;
            Console.WriteLine("默认编码：{0}", defaultEncoding.BodyName);


            Encoding dstEncoding = null;
            //ASCII码
            Console.WriteLine("----ASCII编码----");
            dstEncoding = Encoding.ASCII;
            OutputByEncoding(defaultEncoding, dstEncoding, str_en);
            OutputByEncoding(defaultEncoding, dstEncoding, str_cn);
            OutputByEncoding(defaultEncoding, dstEncoding, str_tw);

            OutputBoundary();

            //GB2312
            Console.WriteLine("----GB2312编码----");
            dstEncoding = Encoding.GetEncoding("GB2312");
            OutputByEncoding(defaultEncoding, dstEncoding, str_en);
            OutputByEncoding(defaultEncoding, dstEncoding, str_cn);
            OutputByEncoding(defaultEncoding, dstEncoding, str_tw);

            OutputBoundary();

            //BIG5
            Console.WriteLine("----BIG5编码----");
            dstEncoding = Encoding.GetEncoding("BIG5");
            OutputByEncoding(defaultEncoding, dstEncoding, str_en);
            OutputByEncoding(defaultEncoding, dstEncoding, str_cn);
            OutputByEncoding(defaultEncoding, dstEncoding, str_tw);

            OutputBoundary();

            //Unicode
            Console.WriteLine("----Unicode编码----");
            dstEncoding = Encoding.Unicode;
            OutputByEncoding(defaultEncoding, dstEncoding, str_en);
            OutputByEncoding(defaultEncoding, dstEncoding, str_cn);
            OutputByEncoding(defaultEncoding, dstEncoding, str_tw);

            OutputBoundary();

            //UTF8
            Console.WriteLine("----UTF8编码----");
            dstEncoding = Encoding.UTF8;
            OutputByEncoding(defaultEncoding, dstEncoding, str_en);
            OutputByEncoding(defaultEncoding, dstEncoding, str_cn);
            OutputByEncoding(defaultEncoding, dstEncoding, str_tw);

            Console.ReadKey();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcEncoding">原编码</param>
        /// <param name="dstEncoding">目标编码</param>
        /// <param name="srcBytes">原</param>
        public static void OutputByEncoding(Encoding srcEncoding, Encoding dstEncoding, string srcStr)
        {
            byte[] srcBytes = srcEncoding.GetBytes(srcStr);
            Console.WriteLine("Encoding.GetBytes: {0}", BitConverter.ToString(srcBytes));
            byte[] bytes = Encoding.Convert(srcEncoding, dstEncoding, srcBytes);
            Console.WriteLine("Encoding.GetBytes: {0}", BitConverter.ToString(bytes));
            string result = dstEncoding.GetString(bytes);
            Console.WriteLine("Encoding.GetString: {0}", result);
        }
        /// <summary>
        /// 分割线
        /// </summary>
        public static void OutputBoundary()
        {
            Console.WriteLine("------------------------------------");
        }
    }
}
