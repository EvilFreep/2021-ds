using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text.Json;
using ShareClassesLibrary;

namespace Server
{
    internal static class Program
    {
        public static void StartListening(int port)
        {

            // Разрешение сетевых имён
            var history = new List<string>();
            // Привязываем сокет ко всем интерфейсам на текущей машинe
            IPAddress ipAddress = IPAddress.Any; 
            
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            // CREATE
            Socket listener = new Socket(
                ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            try
            {
                // BIND
                listener.Bind(localEndPoint);

                // LISTEN
                listener.Listen(10);

                while (true)
                {
                    var handler = listener.Accept();
                    var data = Interactions.ReceiveMsg(handler);
                    history.Add(data);
                    Console.WriteLine("Message received: {0}", data);
                    var jsonMsg = JsonSerializer.Serialize(history);
                    Interactions.SendMsg(handler, jsonMsg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void Main(string[] args)
        {
            Interactions.CheckArgumentCount(args, 1);
            StartListening(int.Parse(args[0]));
        }
    }
}
