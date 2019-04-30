using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AsyncSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 异步客户端
            //AsynchronousClient.StartClient();

            #endregion

            #region 同步客户端
            SyncClient sClient = new SyncClient("127.0.0.1", 11009);
            sClient.Start();

            //循环发送命令
            while (true)
            {
                string str = Console.ReadLine();
                sClient.SendMsg(str);
            }

            #endregion
        }
    }
}