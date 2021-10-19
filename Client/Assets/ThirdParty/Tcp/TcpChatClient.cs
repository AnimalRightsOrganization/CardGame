using System;
using System.Net.Sockets;
using System.Threading;
using TcpClient = NetCoreServer.TcpClient;
using Debug = UnityEngine.Debug;

namespace TcpChatClient
{
    public class ChatClient : TcpClient
    {
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
            NetManager.Instance.OnNetStateReceived(1);
        }

        protected override void OnDisconnected()
        {
            Debug.Log($"Chat TCP client disconnected a session with Id {Id}");
            NetManager.Instance.OnNetStateReceived(0);

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            byte header = buffer[0];
            byte[] body = new byte[buffer.Length - 1];
            Array.Copy(buffer, 1, body, 0, buffer.Length - 1);
            //Debug.Log($"[S2C] {header}");

            // 这里是线程内，不解包。
            // 分发给网络管理器（NetManager），在Update中取出。
            // 再分发到UI里面去处理。
            NetManager.Instance.OnNetPacketReceived(header, body);

            /*
            switch ((SCID)header)
            {
                case SCID.S2CRegister:
                    {
                        var packet = ProtobufferTool.Deserialize<RegisterError>(body);
                        Debug.Log($"[{header}] Code={packet.Code}");
                        if (packet.Code == 100)
                        {
                            Debug.LogError("注册失败，用户名已存在");
                        }
                    }
                    break;
                case SCID.S2CLogin:
                    {
                        var packet = ProtobufferTool.Deserialize<LoginResult>(body);
                        Debug.Log($"[{header}] Code={packet.Code}, Username={packet.Username}, Token={packet.Token}");
                        if (packet.Code == 0)
                        {
                            Debug.Log("<color=green>登录成功</color>");
                        }
                        else if (packet.Code == 100)
                        {
                            Debug.LogError("登录失败，用户名或密码错误");
                        }
                        else
                        {
                            Debug.LogError($"登录失败，其他原因：{packet.Code}");
                        }
                    }
                    break;
                default: //处理成文本
                    string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
                    Debug.Log($"Client Received: {message}");
                    break;
            }
            */
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

            /* for (;;) 反编译底层比 while(true) 省资源。
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