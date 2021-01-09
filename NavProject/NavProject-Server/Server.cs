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

        public string CodeGenerator(int length)
        {
            Random rand = new Random();
            string result = "";
            int value;
            for (int i = 0; i < length; ++i)
            {
                value = rand.Next(0, 36);
                result += (value < 26) ? Convert.ToChar(value + 65).ToString() : (value - 26).ToString();
            }
            return result;
        }
        void ServeUser()
        {
            Console.WriteLine($"[{id}]New client was connected");
            stream = client.GetStream();
            byte[] msg;


            string responseString = "";
            bool isClientAuthorize = false;

            string currentMode = "";
            string buffer = "";

            while (client.Connected)
            {
                msg = new byte[1024];
                try
                {
                    //count = await //msg, 0, msg.Length);//
                    stream.ReadTimeout = 10000000;
                    int count = stream.Read(msg, 0, msg.Length);
                    string[] command = Encoding.UTF8.GetString(msg, 0, count).Split();

                    Console.WriteLine($"[{id}]Command: {command[0]}");
                    switch (command[0])
                    {
                        case "ClientAuthorize":
                            {
                                isClientAuthorize = true;
                                break;
                            }
                        case "Authorize":
                            {
                                user = DBInteraction.GetUser(command[1], command[2]);
                                responseString = (user == null) ? "No matches found" : $"Welcome back,{user.Value.name}";
                                Response(responseString);
                                break;
                            }
                        case "InsertUser":
                            {
                                DBInteraction.InsertUser(command[1], command[2], command[3], command[4]);
                                user = DBInteraction.GetUser(command[2], command[3]);
                                Response($"Регистрация успешно пройдена");
                                break;
                            }
                        case "SendConfirmEmailMessage":
                            {
                                if (user == null)
                                {
                                    Response("Authorization required");
                                    continue;
                                }
                                if(user.Value.isConfirmed)
                                {
                                    Response("Email is already confirmed");
                                    continue;
                                }
                                currentMode = "ConfirmEmail";
                                buffer = CodeGenerator(6);
                                var smtpClient = new SmtpClient("smtp.gmail.com")
                                {
                                    Port = 587,
                                    UseDefaultCredentials = false,
                                };
                                smtpClient.Credentials = new NetworkCredential("navproject.kubsu@gmail.com", "146923785Alex");
                                smtpClient.EnableSsl = true;

                                var mailMessage = new MailMessage
                                {
                                    From = new MailAddress("navproject.kubsu@gmail.com"),
                                    Subject = "NavProject - регистрация нового пользователя",
                                    Body = "<p></p>\n" +
                                    "<h2> NavProject</h2>\n" +
                                    "<p></p>\n" +
                                    $"<p> Здравствуйте, {user.Value.name}.</p>\n" +
                                    "<p> Этот адрес электронной почты был указан для регистрации нового пользователя в самом странном проекте по созданию планов зданий.</p>\n" +
                                    "<p> Ваш код регистрации:</p>\n" +
                                    $"<h3><strong>{buffer}</strong><strong></strong></h3>\n" +
                                    "<p> Учтите, что код действителен в течение 60 минут.</p>\n" +
                                    "<p></p>\n" +
                                    "<p> С уважением,</p>\n" +
                                    "<p> команда NavProject </p>",
                                    IsBodyHtml = true
                                };
                                mailMessage.To.Add(user.Value.email);
                                smtpClient.Send(mailMessage);
                                Response("Message was sent");
                                break;
                            }
                        case "ConfirmEmail":
                            {
                                if (user == null)
                                {
                                    Response("No user signing in");
                                    continue;
                                }
                                if (currentMode != "ConfirmEmail")
                                {
                                    Response("No current email confirmation messages");
                                    continue;
                                }
                                if (command[1] == buffer)
                                {
                                    user = new User(user.Value.id, user.Value.name, user.Value.login, user.Value.password, user.Value.email, true);
                                    DBInteraction.ConfirmUser(user.Value.id);
                                }
                                else
                                {
                                    Response("Wrong code");
                                    continue;
                                }
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
                                if (user == null)
                                    Response("Authorization required");
                                break;
                            }
                        case "Disconnect":
                            {
                                Console.WriteLine($"[{id}]Client disconnected");
                                stream.Close();
                                client.Close();
                                break;
                            }
                        case "TestText":
                            {
                                DBInteraction.InsertLotRows(400);
                                break;
                            }
                    }
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
                        //new Thread(() =>
                        //{
                        TcpClient client = clientList.Last();
                        new ClientServe(clientList.Count, client);
                        clientList.Remove(client);
                        //}).Start();
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
