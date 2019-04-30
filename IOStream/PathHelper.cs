using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOStream
{
    class PathHelper
    {
        public static void Tester()
        {
            /*
             *  System.IO.Path.GetFullPath(openFileDialog1.FileName);                             //绝对路径
                System.IO.Path.GetExtension(openFileDialog1.FileName);                          //文件扩展名
                System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);//文件名没有扩展名
                System.IO.Path.GetFileName(openFileDialog1.FileName);                          //得到文件
                System.IO.Path.GetDirectoryName(openFileDialog1.FileName);                  //得到路径
             */

            string curDicrectory = System.IO.Directory.GetCurrentDirectory();
            // 当前exe所在的根目录，无\结尾
            Console.WriteLine("{0}---{1}", "System.IO.Directory.GetCurrentDirectory()",curDicrectory);
            // 获取最后一个\前的内容与Path.GetFileName正好相反
            Console.WriteLine("{0}---{1}", "Path.GetDirectoryName(curDicrectory)", Path.GetDirectoryName(curDicrectory));

            // 无论是绝对路径，还是相对路径，获去的都是最后一个/后的内容：133.png或者debug
            string filePath = "/path/132/133.png";
            // filePath = curDicrectory; // debug
            string ret=Path.GetFileName(filePath);
            Console.WriteLine("{0}---{1}", "Path.GetFileName(filePath)", ret);

            string directoryPath = @"G:\x私有项目\阿宣\AXuan\AXuan\bin\Debug\img";
            DirectoryInfo dirInfo= new DirectoryInfo(directoryPath);
            int fileCount=dirInfo.GetFiles().Count();
            Console.WriteLine("{0}---{1}", "dirInfo.GetFiles().Count()",fileCount);


        }
    }
}
