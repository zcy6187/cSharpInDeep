using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IOStream
{
    /*StreamReader是TextReader的子类；     
     ****************构造函数**************
     *1: StreamReader(Stream stream)
 将stream作为一个参数 放入StreamReader，这样的话StreamReader可以对该stream进行读取操作,Stream对象可以非常广泛，包括所有Stream的派生类对象
    *2: StreamReader(string string, Encoding encoding)
 这里的string对象不是简单的字符串而是具体文件的地址,然后根据用户选择编码去读取流中的数据
    *3: StreamReader(string string，bool detectEncodingFromByteOrderMarks)       
有时候我们希望程序自动判断用何种编码去读取，这时候detectEncodingFromByteOrderMarks这个参数就能起作用了，当设置为true的 时候数通过查看流的前三个字节
来检测编码。如果文件以适当的字节顺序标记开头，该参数自动识别 UTF-8、Little-Endian Unicode 和 Big-Endian Unicode 文本，当为false 时，方法会去使用用户提供
的编码
    *4: StreamReader(string string, Encoding encoding, bool detectEncodingFromByteOrderMarks,int bufferSize)          
这个放提供了4个参数的重载，前3个我们都已经了解，最后个是缓冲区大小的设置，
    *StreamReader 还有其他的一些构造函数，都是上述4个的扩充，所以本例就取上述的4个构造函数来说明      
    * **************************属性***********************
    * 1、BaseStream
  大家对于前一章流的操作应该没什么问题，我就直切主题，最简单的理解就是将上述构造函数的流对象在重新取出来进行一系列的操作，
  可是如果构造函数中是路径怎么办，一样，构造函数能够将路径文件转化成流对象
    * 2、CurrentEncoding:
  获取当前StreamReader的Encoding
    * 3、EndOfStream:
  判断StreamReader是否已经处于当前流的末尾
         */
    class IOStreamReader
    {
        public static void BaseTest()
        {
            string filePath = "D:\\Stream.txt";
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                {
                    Console.WriteLine("编码格式为：{0}", sr.CurrentEncoding);  //默认编码格式utf-8
                    DisplayResultStringByUsingReadLine(sr);
                }
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, Encoding.ASCII, false)) //最后一个参数如果为true,或者没有，会根据流的编码格式修改编码方式；如果为false，则不会，只会按照用户编写的方式
                {
                    Console.WriteLine("编码格式为：{0}", sr.CurrentEncoding);  //编码格式ascii
                    DisplayResultStringByUsingReadLine(sr);
                }
            }
        }

        /// <summary>
        /// 使用StreamReader.Read()方法
        /// </summary>
        /// <param name="reader"></param>
        public static void DisplayResultStringByUsingRead(System.IO.StreamReader reader)
        {
            int readChar = 0;
            string result = string.Empty;
            while ((readChar = reader.Read()) != -1)
            {
                result += (char)readChar;
            }
            Console.WriteLine("使用StreamReader.Read()方法得到Text文件中的数据为 : {0}", result);
        }

        /// <summary>
        /// 使用StreamReader.ReadBlock()方法
        /// </summary>
        /// <param name="reader"></param>
        public static void DisplayResultStringByUsingReadBlock(System.IO.StreamReader reader)
        {
            char[] charBuffer = new char[10];
            string result = string.Empty;
            reader.ReadBlock(charBuffer, 0, 10);
            for (int i = 0; i < charBuffer.Length; i++)
            {
                result += charBuffer[i];
            }
            Console.WriteLine("使用StreamReader.ReadBlock()方法得到Text文件中前10个数据为 : {0}", result);
        }


        /// <summary>
        /// 使用StreamReader.ReadLine()方法
        /// </summary>
        /// <param name="reader"></param>
        public static void DisplayResultStringByUsingReadLine(System.IO.StreamReader reader)
        {
            int i = 1;
            string resultString = string.Empty;
            while ((resultString = reader.ReadLine()) != null)
            {
                Console.WriteLine("使用StreamReader.Read()方法得到Text文件中第{1}行的数据为 : {0}", resultString, i);
                i++;
            }

        }
    }
}
