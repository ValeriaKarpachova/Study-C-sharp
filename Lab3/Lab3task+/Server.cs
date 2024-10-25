using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;


namespace Lab3task2
{
    internal class Server
    {
        static public void Play(string command)
        {
            while (true)
            {
                switch (command)
                {
                    case "Paint":
                        Console.WriteLine("Відкриваю графічний редактор...");
                        Process.Start("C:/Program Files/WindowsApps/Microsoft.Paint_11.2408.30.0_x64__8wekyb3d8bbwe/PaintApp/mspaint.exe");
                        break;
                    case "Off":
                        Console.WriteLine("Вимкнення комп'ютера...");
                        Process.Start("shutdown", "/s /t 30");
                        break;
                    case "Notepad":
                        Console.WriteLine("Відкриваю текстовий редактор...");
                        Process.Start("C:/Windows/notepad.exe");
                        break;
                    case "GitHub":
                        Console.WriteLine("Відкриваю браузер...");
                        Process.Start("C:/Program Files/Google/Chrome/Application/chrome.exe", "https://github.com/");
                        break;
                    case "PlaySound":
                        Console.WriteLine("Програю звук...");
                        Console.Beep(440, 500); 
                        break;
                    case "Processes":
                        Console.WriteLine("Список процесів:");
                        foreach (var process in Process.GetProcesses())
                        {
                            Console.WriteLine($"{process.ProcessName} (ID: {process.Id})");
                        }
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses;
            ipAddresses = Dns.GetHostAddresses(hostName);
            Console.WriteLine("Сервер запущено ;)");
            Console.WriteLine("Host name: {0}", hostName);
            Console.WriteLine("IP address: {0}", ipAddresses[0]);

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Socket serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSock.Bind(ipEndPoint);

            Console.WriteLine("Очікуємо підключення...");
            serverSock.Listen(10);
            Socket clientSock = serverSock.Accept();
            IPEndPoint remote = (IPEndPoint)clientSock.RemoteEndPoint;
            Console.WriteLine("Клієнт {0} підключився :)", clientSock.RemoteEndPoint.ToString());
            byte[] buf = new byte[1024];
            while (true)
            {
                int CountBytes = clientSock.Receive(buf);
                if (CountBytes <= 0)
                {
                    Console.WriteLine("Клієнт завершив сеанс :(");
                    break;
                }
                string msg = Encoding.UTF8.GetString(buf, 0, CountBytes);
                Console.WriteLine("  {0}", msg);
                Server.Play(msg);
                clientSock.Send(Encoding.UTF8.GetBytes("Команда виконана"));
            }
            clientSock.Shutdown(SocketShutdown.Both);
            clientSock.Close();

            Console.ReadKey();
        }
    }
}

