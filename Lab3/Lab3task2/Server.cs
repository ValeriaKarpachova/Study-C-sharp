using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lab3task2
{
    internal class Server
    {
        static void Main(string[] args)
        {
            TcpListener tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            tcpServer.Start();
            Console.WriteLine("Очікування підключення...");

            TcpClient tcpClient = tcpServer.AcceptTcpClient();
            Console.WriteLine("Підключення встановлено, Вхідне повідомлення: ");

            byte[] buffer = new byte[1];
            NetworkStream streamTcp = tcpClient.GetStream();
            streamTcp.Read(buffer, 0, buffer.Length);
            Console.WriteLine(buffer[0]);

            string response = IsPalindrome(buffer[0]) ? "Yes" : "No"; 
            byte[] pack = Encoding.UTF8.GetBytes(response);

            streamTcp.Write(pack, 0, pack.Length);
            Console.WriteLine($"Відповідь надіслано: {response}");

            streamTcp.Close();
            tcpClient.Close();
            tcpServer.Stop();
            Console.ReadKey();
        }

        static bool IsPalindrome(byte buffer)
        {
            string stringbuffer = buffer.ToString();

            int length = stringbuffer.Length;
            for (int i = 0; i < length / 2; i++)
            {
                if (stringbuffer[i] != stringbuffer[length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
