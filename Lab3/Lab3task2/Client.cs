using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab3task2
{
    internal class Client
    {
        static void Main(string[] args)
        {
            TcpClient clientTcp = new TcpClient();
            clientTcp.Connect(IPAddress.Parse("127.0.0.1"), 8000);
            NetworkStream streamTcp = clientTcp.GetStream();
            Console.WriteLine("Підключено до сервера. DDos-почати атаку, Exit-завершити сеанс");
            while (true)
            {
                Console.Write(">");
                string msg = Console.ReadLine();

                if (msg.Equals("Exit"))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes("Exit");
                    streamTcp.Write(buffer, 0, buffer.Length);
                    Thread.Sleep(2000);
                    break;
                }
              
                if (msg.Equals("DDos"))
                {
                    Console.Write("Введіть кількість повідомлень у секунду: ");
                    int count = int.Parse(Console.ReadLine());

                    int frequency = 1000 / count;

                    for (int i = 0; i < 100; i++)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes("gettime");
                        streamTcp.Write(buffer, 0, buffer.Length);

                        byte[] responseBuffer = new byte[256];
                        int bytesRead = streamTcp.Read(responseBuffer, 0, responseBuffer.Length);
                        string responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                        Console.WriteLine("Час з сервера: " + responseMessage);

                        Thread.Sleep(frequency);

                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine("Відправка повідомлень зупинена.");
                            break;
                        }
                    }
                }
            }
        }
    }
}






