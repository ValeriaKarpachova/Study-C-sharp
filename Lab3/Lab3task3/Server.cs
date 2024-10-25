using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

internal class Server
{
    private static DateTime lastRequestTime;
    private static bool shouldRespond = true;

    static void Main(string[] args)
    {
        TcpListener tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        tcpServer.Start();
        Console.WriteLine("Очікуємо підключення...");

        TcpClient tcpClient = tcpServer.AcceptTcpClient();
        Console.WriteLine("Підключення встановлено");

        try
        {
            NetworkStream streamTcp = tcpClient.GetStream();
            lastRequestTime = DateTime.Now.AddMinutes(-1);

            while (true) 
            {
                byte[] buffer = new byte[256]; 
                int bytesRead = streamTcp.Read(buffer, 0, buffer.Length);

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                DateTime now = DateTime.Now;

                if (message.Equals("Exit"))
                {
                    Console.WriteLine("Отримано команду Exit. Завершення з'єднання.");
                    Thread.Sleep(2000);
                    break; 
                }

                if (message.Equals("gettime"))
                {
                    double timeSinceLastRequest = (now - lastRequestTime).TotalMilliseconds;

                    if (timeSinceLastRequest <= 260) 
                    {
                        Console.WriteLine("Надто висока частота запитів. Запити відхилено.");
                        shouldRespond = false;
                    }
                    else
                    {
                        lastRequestTime = now;
                        shouldRespond = true;

                        if (shouldRespond)
                        {
                            byte[] response = Encoding.UTF8.GetBytes(DateTime.Now.ToString("hh:mm:ss"));
                            streamTcp.Write(response, 0, response.Length);
                            Console.WriteLine("Відправлено час: " + DateTime.Now.ToString("hh:mm:ss"));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}






