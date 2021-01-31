using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static int port = 8020;
        static TcpClient client;
        static NetworkStream stream;
        static void SendCommand(string command)
        {
            byte[] commandByte = Encoding.UTF8.GetBytes(command);
            stream.Write(commandByte, 0, commandByte.Length);
        }

        static void GetResponse()
        {
            Byte[] readingData = new byte[client.ReceiveBufferSize];//new byte[1024];
            StringBuilder completeMessage = new StringBuilder();
            int numberOfBytesRead = 0;
            do
            {
                numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
            }
            while (stream.DataAvailable);
            Console.WriteLine(completeMessage.ToString());
        }

        static void Main(string[] args)
        {
            void Menu()
            {
                Console.WriteLine("_______________________________");
                Console.WriteLine("Выберите необходимую команду");
                Console.WriteLine("1 - добавить нового пользователя");
                Console.WriteLine("2 - аутентифицироваться");
                Console.WriteLine("3 - testText");
                Console.WriteLine("4 - ConfirmEmail_GetMessage");
                Console.WriteLine("5 - ConfirmEmail");
                Console.Write("Команда: ");
            }

            client = new TcpClient("127.0.0.1", port);
            stream = client.GetStream();
            Console.WriteLine("Успешное подключение к серверу");

            string input = "";
            bool flag = false;
            while (input != "0")
            {
                Menu();
                input = Console.ReadLine();
                try
                {
                    switch (Convert.ToInt32(input))
                    {
                        case 0:
                            {
                                SendCommand("Disconnect");
                                break;
                            }
                        case 1:
                            {
                                string WholeCommand = "InsertUser";
                                string localInput = "";
                                Console.WriteLine("Введите ваше имя");
                                localInput = Console.ReadLine();
                                WholeCommand += $" {localInput.Replace(' ', '_')}";
                                Console.WriteLine("Введите ваш логин");
                                localInput = Console.ReadLine();
                                WholeCommand += $" {localInput.Replace(' ', '_')}";
                                Console.WriteLine("Введите ваш пароль");
                                localInput = Console.ReadLine();
                                WholeCommand += $" {localInput.Replace(' ', '_')}";
                                Console.WriteLine("Введите вашу электронную почту");
                                localInput = Console.ReadLine();
                                WholeCommand += $" {localInput.Replace(' ', '_')}";
                                SendCommand(WholeCommand);
                                break;
                            }
                        case 2:
                            {
                                string WholeCommand = "Authorize";
                                string localInput = "";
                                Console.WriteLine("Введите ваш логин");
                                localInput = Console.ReadLine();
                                WholeCommand += $" {localInput.Replace(' ', '_')}";
                                Console.WriteLine("Введите ваш пароль");
                                localInput = Console.ReadLine();
                                WholeCommand += $" {localInput.Replace(' ', '_')}";
                                SendCommand(WholeCommand);
                                break;
                            }
                        case 3:
                            {
                                SendCommand("TestText");
                                break;
                            }
                        case 4:
                            {
                                SendCommand("SendConfirmEmailMessage");
                                flag = true;
                                break;
                            }
                        case 5:
                            {
                                if (flag)
                                {
                                    Console.WriteLine("Введите полученный код");
                                    string code = Console.ReadLine();
                                    SendCommand($"ConfirmEmail {code}");
                                }
                                break;
                            }
                        default:
                            continue;
                    }
                    GetResponse();
                }
                catch
                {
                    continue;
                }
            }
            client.Close();
            Console.WriteLine("Отключение от сервера. Завершение работы программы");
            Console.ReadKey();
        }
    }
}
