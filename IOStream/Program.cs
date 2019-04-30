using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOStream
{
    class Program
    {
        static void Main(string[] args)
        {
            #region memorystream测试
            //IOMemoryStream ims = new IOMemoryStream();
            //ims.BasicTest();
            #endregion

            #region TextReader测试
            //IOTextReader itr = new IOTextReader();
            //itr.BasicTest();
            #endregion

            #region StreamReader测试
            //IOStreamReader.BaseTest();

            #endregion

            #region 字符编解码
            // CharsetEncoderAndDecoder.BasicTest();
            #endregion


            #region 路径测试
            // PathHelper.Tester();
            #endregion

            #region StreamWriter测试
            //string txtFilePath = "d:\\temp.txt";
            //NumberFormatInfo numberFomatProvider = new NumberFormatInfo();
            ////将小数 “.”换成"?" 
            //numberFomatProvider.PercentDecimalSeparator = "?";
            //IOStreamWriter test = new IOStreamWriter(Encoding.UTF8, txtFilePath, numberFomatProvider);
            ////StreamWriter
            //test.WriteSomthingToFile();
            ////TextWriter
            //test.WriteSomthingToFileByUsingTextWriter();
            //Console.ReadLine();
            #endregion

            Console.ReadKey();
        }
    }
}
