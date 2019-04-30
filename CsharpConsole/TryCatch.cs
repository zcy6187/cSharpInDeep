using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpConsole
{
    class TryCatch
    {
        public static int Base()
        {
            try
            {
                return 1;
            }
            catch
            {
                return 2;
            }
            finally
            {
                Console.WriteLine("finally");
            }
            return 3;

            /*
             * finally
             * 1
             */
        }

        public static int Test2()
        {
            int ret = 0;
            try
            {
                int a = 0;
                ret = 8 / a;
                return ret;
            }
            catch
            {
                
            }
            finally
            {
                Console.WriteLine("finally");
                ret +=10;
            }
            return ret;

            /*
             * finally
             * 1
             */
        }
    }
}
