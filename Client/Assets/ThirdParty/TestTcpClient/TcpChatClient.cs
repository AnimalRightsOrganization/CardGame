using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpClient = NetCoreServer.TcpClient;
using Debug = UnityEngine.Debug;

namespace TcpChatClient
{
    public class ChatClient : TcpClient
    {
        // 线程中，不支持操作UI。
        //public Action m_OnConnected_Callback;
        //public Action m_OnDisconnected_Callback;
        //public Action<SocketError> m_OnNetworkError_Callback;

        public ChatClient(string address, int port) : base(address, port) {}

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Debug.Log($"Chat TCP client connected a new session with Id {Id}");
            //m_OnConnected_Callback?.Invoke();
            //EventManager.Trigger(1);
        }

        protected override void OnDisconnected()
        {
            Debug.Log($"Chat TCP client disconnected a session with Id {Id}");
            //m_OnDisconnected_Callback?.Invoke();
            //EventManager.Trigger(0);

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Debug.Log("Client Received Sth. ...");
            Debug.Log(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
        }

        protected override void OnError(SocketError error)
        {
            Debug.Log($"Chat TCP client caught an error with code {error}");
            //m_OnNetworkError_Callback?.Invoke(error);
        }

        private bool _stop;
    }

    public class Program
    {
        public static void Connect()
        {
            // TCP server address
            string address = "127.0.0.1";

            // TCP server port
            int port = 1111;

            Debug.Log($"TCP server address: {address}");
            Debug.Log($"TCP server port: {port}");

            // Create a new TCP chat client
            var client = new ChatClient(address, port);

            // Connect the client
            Debug.Log("Client connecting...");
            client.ConnectAsync();
            Debug.Log("Done!");

            Debug.Log("Press Enter to stop the client or '!' to reconnect the client...");

            /*
            // Perform text input
            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Disconnect the client
                if (line == "!")
                {
                    Debug.Log("Client disconnecting...");
                    client.DisconnectAsync();
                    Debug.Log("Done!");
                    continue;
                }

                // Send the entered text to the chat server
                client.SendAsync(line);
            }

            // Disconnect the client
            Debug.Log("Client disconnecting...");
            client.DisconnectAndStop();
            Debug.Log("Done!");
            */
        }
    }
}