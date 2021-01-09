using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Mail;

namespace NavProject_Server
{
    struct User
    {
        public User(int ID, string NAME, string LOGIN, string PASSWORD, string EMAIL, bool IsCONFIRMED)
        {
            id = ID;
            name = NAME;
            login = LOGIN;
            password = PASSWORD;
            email = EMAIL;
            isConfirmed = IsCONFIRMED;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool isConfirmed { get; set; }
    }
    class ClientServe
    {
        private int id;
        private TcpClient client;
        private User? user = null;
        private NetworkStream stream;

        public ClientServe(int ID, TcpClient CLIENT)
        {
            id = ID;
            client = CLIENT;
            ServeUser();
        }

        private string responseString = "";
        private bool isClientAuthorize = false;

        private string currentMode = "";
        private string buffer = "";

        public void RecognizeCommand(ref string[] command)
        {
            switch (command[0])
            {
                case "ClientAuthorize":
                    {
                        isClientAuthorize = true;
                        break;
                    }
                case "Authorize":
                    {

                        break;
                    }
                case "InsertUser":
                    {

                        break;
                    }
                case "SendConfirmEmailMessage":
                    {
                        break;
                    }
                case "ConfirmEmail":
                    {
                        break;
                    }
                case "ForgotPassword":
                    {
                        break;
                    }
                case "GetPlan": // get plan by id
                    {
                        break;
                    }
                case "GetPlansList_Public":
                    {
                        break;
                    }
                case "GetUserPlansList":
                    {
                        if (user == null)
                            Response("Authorization required");
                        break;
                    }
                case "UploadPlan":
                    {
                        Response("Authorization required");
                        break;
                    }
                case "Disconnect":
                    {
                        break;
                    }
                case "TestText":
                    {

                        break;
                    }
            }
        }
        void ServeUser()
        {
            Console.WriteLine($"[{id}]New client was connected");
            stream = client.GetStream();
            byte[] msg;


            while (client.Connected)
            {
                msg = new byte[1024];
                try
                {
                    stream.ReadTimeout = 1000;
                    int count = stream.Read(msg, 0, msg.Length);
                    string[] command = Encoding.UTF8.GetString(msg, 0, count).Split();
                    Console.WriteLine($"[{id}]Command: {command[0]}");
                    RecognizeCommand(ref command);
                }
                catch (System.IO.IOException)// e)
                {
                    Console.WriteLine($"[{id}]Connection Was aborted - RecieveRuntime");
                    Response("Connection was aborted - RecieveRuntime");
                    CloseConnection();
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[{id}]Connection Was aborted - ({e.Message})");
                    Response("Connection was aborted");
                    CloseConnection();
                    return;
                }

            }
            CloseConnection();
        }
        void Response(string responseString)
        {
            Byte[] responseData = Encoding.UTF8.GetBytes(responseString);
            stream.Write(responseData, 0, responseData.Length);
        }
        void CloseConnection()
        {
            stream.Close();
            client.Close();
        }
    }
    class Server
    {
        List<TcpClient> clientList;
        public void ServerManager()
        {
            DBInitialize.InitDB();
            clientList = new List<TcpClient>();
            TcpListener server = null;
            int port = 8020;
            try
            {
                IPAddress local = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(local, port);
                server.Start();
                Console.WriteLine("Server was started");

                while (true)
                {
                    if (clientList.Count < 10)
                    {
                        clientList.Add(server.AcceptTcpClient());
                        Console.WriteLine($"Есть новое подключение, индекс - {clientList.Count}");
                        new Thread(() =>
                        {
                            TcpClient client = clientList.Last();
                            new ClientServe(clientList.Count, client);
                            clientList.Remove(client);
                        }).Start();
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"Socket Exception {e}");
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("Server was succesfully stopped");
            Console.ReadKey();
        }
    }
}
