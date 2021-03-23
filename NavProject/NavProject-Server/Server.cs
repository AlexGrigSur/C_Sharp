using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Net.Mail;

namespace NavProject_Server
{
    class Server
    {
        
        private void Response(ref HttpListenerResponse response)
        {
        }
        //private Dictionary<string,IHandler>

        /// <summary>
        /// Start HTTP server
        /// </summary>
        public async void Start()
        {
            HttpListener server = new HttpListener();

        }
    }
}
#region Obsollette TCP Server
//TcpListener server = null;
//int port = 8060;
//try
//{
//    IPAddress local = IPAddress.Parse("127.0.0.1");
//    server = new TcpListener(local, port);
//    server.Start();
//    Console.WriteLine("Server was started");
//    List<TcpClient> clients = new List<TcpClient>();
//    while (true)
//    {
//        Console.WriteLine("New connection await");
//        clients.Add(server.AcceptTcpClient());
//        Console.WriteLine("New client connected");


//        Console.WriteLine($"Есть новое подключение, индекс - {clients.Count}");
//        new Thread(() =>
//        {
//            TcpClient localClient = clients.Last();
//            TcpClient client = clients.Last();
//            // RunHandle
//            clients.Remove(localClient);
//        }).Start();
//    }
//}
//catch (SocketException e)
//{
//    Console.WriteLine($"Socket Exception {e}");
//}
//finally
//{
//    server.Stop();
//}

//Console.WriteLine("Server was succesfully stopped");
//Console.ReadKey();
#endregion