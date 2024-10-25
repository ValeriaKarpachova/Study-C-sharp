using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Lab3task2
{
    class Client
    {
        static void Main(string[] args)
        {
            Socket sockClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipServ = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndP = new IPEndPoint(ipServ, 8000);
            try
            {
                sockClient.Connect(ipEndP);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Підключення успішне");
            Console.WriteLine("\nКоманди:\nPaint - відкрити графічний редактор\nNotepad - відкрити текстовий редактор\nOff - вимкнути комп'ютер\nGitHub-відкрити вебсервіс\nPlaySound-програти звук\nProcesses-показати поточні процеси\nExit - завершити сеанс");

            byte[] buf = new byte[32];
            while (true)
            {
                Console.Write("> ");
                string msg = Console.ReadLine();
                if (msg.Length <= 0)
                    continue;
                if (msg == "Exit")
                {
                    sockClient.Shutdown(SocketShutdown.Both);
                    sockClient.Close();
                    Console.WriteLine("Сеанс завершено");
                    break;
                }
                sockClient.Send(Encoding.UTF8.GetBytes(msg));
                int CountBytes = sockClient.Receive(buf);
                Console.WriteLine(Encoding.UTF8.GetString(buf, 0, CountBytes));
            }
        }
    }
}






