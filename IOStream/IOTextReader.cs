using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOStream
{
    class IOTextReader
    {
        /*TextReader是StreamReader的父类，你没有看错。
         * TextReader的所有读取都是基于char的，char是读取的最小单位
         */
        public void BasicTest()
        {
            string tempStr = "abc中国\n123";

            using (TextReader tr = new StringReader(tempStr))
            {
                while (tr.Peek() != -1)
                {
                    Console.WriteLine("Peak={0}", (Char)tr.Peek()); //Peak获取下一个字符的int值，但指针停留在当前字符
                    Console.WriteLine("Read={0}", (Char)tr.Read()); //Peak获取下一个字符的int值，但指针指向下个字符
                }
            }

            using (TextReader tr = new StringReader(tempStr))
            {
                Console.WriteLine("——————————————————————————————Read(buffer,offset,length)");
                char[] bufferChar = new char[8];
                tr.Read(bufferChar, 0, 8);
                Console.WriteLine("Read={0}", new string(bufferChar)); //Peak获取下一个字符的int值，但指针指向下个字符

            }

            using (TextReader tr = new StringReader(tempStr))
            {
                /*ReadBlock与Read方法功能相同，ReadBlock更高效一些*/
                Console.WriteLine("——————————————————————————————ReadBlock(buffer,offset,length)");

                char[] bufferChar = new char[8];
                tr.ReadBlock(bufferChar, 0, 8);
                Console.WriteLine("ReadBlock={0}", new string(bufferChar));
            }

            using (TextReader tr = new StringReader(tempStr))
            {
                Console.WriteLine("——————————————————————————————ReadLine() 读取单行");
                string str = tr.ReadLine();
                Console.WriteLine("ReadLine={0}", str); //
                str = tr.ReadLine();
                Console.WriteLine("ReadLine={0}", str); //
            }

            using (TextReader tr = new StringReader(tempStr))
            {
                Console.WriteLine("——————————————————————————————ReadToEnd()  读取全部");
                string str = tr.ReadToEnd();
                Console.WriteLine("ReadToEnd={0}", str);
            }


        }
    }
}
