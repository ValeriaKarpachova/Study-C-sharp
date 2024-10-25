using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Створення клієнтського сокета
            Socket cliSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipServ = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndP = new IPEndPoint(ipServ, 8000);

            try
            {
                // Підключення до сервера
                cliSock.Connect(ipEndP);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Console.WriteLine("Введіть частоту повідомлень (кількість повідомлень за секунду):");
            if (!int.TryParse(Console.ReadLine(), out int frequency) || frequency <= 0)
            {
                Console.WriteLine("Неправильна частота.");
                return;
            }

            int delay = 1000 / frequency;  // Затримка між повідомленнями в мілісекундах

            // Відправлення потоку повідомлень
            while (true)
            {
                byte[] pack = Encoding.UTF8.GetBytes("gettime");
                cliSock.Send(pack);

                // Чекаємо отримання відповіді від сервера
                byte[] responseBuffer = new byte[1024];
                int bytesRead = cliSock.Receive(responseBuffer);

                if (bytesRead > 0)
                {
                    string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                    Console.WriteLine("Отримано відповідь від сервера: " + response);
                }
                else
                {
                    Console.WriteLine("Сервер не відповідає.");
                }

                Thread.Sleep(delay);  // Затримка між відправками повідомлень
            }

            // Закриття з'єднання (цей код ніколи не досягнеться через нескінченний цикл)
            // cliSock.Close();
        }
    }
}

