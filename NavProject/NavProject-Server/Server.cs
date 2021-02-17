using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Mail;

namespace NavProject_Server
{
    /*struct User
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
    */


    //class ClientServe
    //    {
    //        private int id;
    //        private TcpClient client;
    //        //private User? user = null;
    //        private NetworkStream stream;

    //        public ClientServe(int ID, TcpClient CLIENT)
    //        {
    //            id = ID;
    //            client = CLIENT;
    //            ServeUser();
    //        }

    //        private string responseString = "";
    //        private bool isClientAuthorize = false;

    //        private string currentMode = "";
    //        private string buffer = "";

    //        public void RecognizeCommand(ref string[] command)
    //        {
    //            switch (command[0])
    //            {
    //                case "ClientAuthorize":
    //                    {
    //                        isClientAuthorize = true;
    //                        break;
    //                    }
    //                case "Authorize":
    //                    {

    //                        break;
    //                    }
    //                case "InsertUser":
    //                    {

    //                        break;
    //                    }
    //                case "SendConfirmEmailMessage":
    //                    {
    //                        break;
    //                    }
    //                case "ConfirmEmail":
    //                    {
    //                        break;
    //                    }
    //                case "ForgotPassword":
    //                    {
    //                        break;
    //                    }
    //                case "GetPlan":
    //                    {
    //                        break;
    //                    }
    //                case "GetPlansList_Public":
    //                    {
    //                        break;
    //                    }
    //                case "GetUserPlansList":
    //                    {
    //                        if (user == null)
    //                            Response("Authorization required");
    //                        break;
    //                    }
    //                case "UploadPlan":
    //                    {
    //                        Response("Authorization required");
    //                        break;
    //                    }
    //                case "Disconnect":
    //                    {
    //                        break;
    //                    }
    //                case "TestText":
    //                    {

    //                        break;
    //                    }
    //            }
    //        }
    //        void ServeUser()
    //        {
    //            Console.WriteLine($"[{id}]New client was connected");
    //            stream = client.GetStream();
    //            byte[] msg;


    //            while (client.Connected)
    //            {
    //                msg = new byte[1024];
    //                try
    //                {
    //                    stream.ReadTimeout = 1000;
    //                    int count = stream.Read(msg, 0, msg.Length);
    //                    string[] command = Encoding.UTF8.GetString(msg, 0, count).Split();
    //                    Console.WriteLine($"[{id}]Command: {command[0]}");
    //                    RecognizeCommand(ref command);
    //                }
    //                catch (Exception e)
    //                {
    //                    Console.WriteLine($"[{id}]Connection Was aborted - ({e.Message})");
    //                    Response("Connection was aborted");
    //                    CloseConnection();
    //                    return;
    //                }

    //            }
    //            CloseConnection();
    //        }
    //        void Response(string responseString)
    //        {
    //            Byte[] responseData = Encoding.UTF8.GetBytes(responseString);
    //            stream.Write(responseData, 0, responseData.Length);
    //        }
    //        void CloseConnection()
    //        {
    //            stream.Close();
    //            client.Close();
    //        }
    //    }

    class User
    {
        private int id;
        private string name;
        private string email;
        private List<int> userCreations;
        private DateTime lastCommandTime;
        public User(int _id, string _name, string _email, List<int> _userCreations)
        {
            id = _id;
            name = _name;
            email = _email;
            userCreations = _userCreations;
            UpdateLastCommandTime();
        }
        public int ID { get { return id; } }
        public void UpdateLastCommandTime() =>
            lastCommandTime = DateTime.Now;
    }
    struct Command
    {
        public Command(TcpClient _client, string _userCommand)
        {
            client = _client;
            userCommand = _userCommand;
        }
        public TcpClient client;
        public string userCommand;
    }
    class CommandHandler
    {
        private List<Command> commandsToHandle;
        private Dictionary<TcpClient, User> clientList;
        public bool[] handlerExit;
        public CommandHandler(ref List<Command> _commandsToHandle, ref bool[] _handlerExit)
        {
            commandsToHandle = _commandsToHandle;
            handlerExit = _handlerExit;
        }
        private void RecognizeCommand()
        {

        }
        public void StartHandler()
        {
            while (!handlerExit[0])
            {
                while (commandsToHandle.Count > 0)
                {
                }
                Thread.CurrentThread.Suspend();
            }
        }
    }
    class Listener
    {
        List<User> UserList;
        public Listener(ref List<User> _UserList) =>
            UserList = _UserList;
    }
    class Server
    {
        private Dictionary<TcpClient, User> ClientList;
        private Thread[] HandlersList;
        private List<Command>[] buffers;

        public async void ServerManager(int threadsCount)
        {
            int SearchMinLoadedBuffer()
            {
                int minIndex = 0;
                //int minValue = buffers[0].Count;
                for (int i = 1; i < buffers.Length; ++i)
                    if (buffers[i].Count < buffers[minIndex].Count)
                        minIndex = i;
                return minIndex;
            }

            HandlersList = new Thread[threadsCount];//CommandHandler[ThreadsCount];
            buffers = new List<Command>[threadsCount];
            bool[] handlerExitFlag = new bool[] { false };
            for (int i = 0; i < threadsCount; i++)
            {
                buffers[i] = new List<Command>();
                int index = i;
                HandlersList[i] = new Thread(() => { new CommandHandler(ref buffers[index], ref handlerExitFlag).StartHandler(); });
                HandlersList[i].Start();
            }
            DBInitialize.InitDB();
            TcpListener server = null;
            int port = 8020;
            try
            {
                IPAddress local = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(local, port);
                server.Start();
                Console.WriteLine("Server was started");
                TcpClient client;
                while (true)
                {
                    Console.WriteLine("New connection await");
                    client = server.AcceptTcpClient();
                    Console.WriteLine("New client connected");
                    await Task.Run(() =>
                    {
                        NetworkStream stream = client.GetStream();
                        byte[] msg = new byte[client.ReceiveBufferSize];
                        int count = stream.Read(msg, 0, msg.Length);
                        int minLoadBuffer = SearchMinLoadedBuffer();
                        buffers[minLoadBuffer].Add(new Command(client, Encoding.UTF8.GetString(msg, 0, count)));
                        if (buffers[minLoadBuffer].Count == 1)
                            HandlersList[minLoadBuffer].Resume();
                    });

                    #region //oldVersion
                    //if (clientList.Count < 10)
                    //{
                    //    clientList.Add(server.AcceptTcpClient());
                    //    Console.WriteLine($"Есть новое подключение, индекс - {clientList.Count}");
                    //    new Thread(() =>
                    //    {
                    //        TcpClient client = clientList.Last();
                    //        new ClientServe(clientList.Count, client);
                    //        clientList.Remove(client);
                    //    }).Start();
                    //}
                    #endregion
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
