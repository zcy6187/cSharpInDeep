using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AsyncSocketServer
{
    class Program
    {
        public static int Main(String[] args)
        {
            #region 异步测试
            //AsynchronousSocketListener.StartListening();
            #endregion

            #region 同步测试
            SyncServer sServer = new SyncServer("127.0.0.1", 11009);
            sServer.StartServer();
            Console.ReadLine();

            #endregion

            return 0;
        }
    }
}
