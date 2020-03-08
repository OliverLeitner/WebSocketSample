using System;
using WebSocketSharp.Server;

namespace websocket
{
    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer(11000, false);
            wssv.AddWebSocketService<ALPHA>("/alpha");
            wssv.Start();
            Console.WriteLine("hit any key to stop");
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
