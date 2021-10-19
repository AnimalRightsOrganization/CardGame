using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using NetCoreServer;
using NetCoreServer.Utils;

namespace TcpChatServer
{
    public class ChatSession : TcpSession
    {
        public ChatSession(TcpServer server) : base(server) {}

        protected override void OnConnected()
        {
            Debug.Print($"Chat TCP session with Id {Id} connected!");

            // Send invite message
            string message = "Hello from TCP chat! Please send a message or '!' to disconnect the client!";
            SendAsync(message);
        }

        protected override void OnDisconnected()
        {
            Debug.Print($"Chat TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            CSID header = (CSID)buffer[0];
            byte[] body = new byte[buffer.Length - 1];
            Array.Copy(buffer, 1, body, 0, buffer.Length - 1);
            Debug.Print($"[C2S] {header}");

            switch (header)
            {
                case CSID.C2SRegister:
                    break;
                case CSID.C2SLogin:
                    var packet = ProtobufferTool.Deserialize<Login>(body);
                    Debug.Print($"[C2SLogin] {packet.Username}, {packet.Password}");

                    // 查询MongoDB，返回结果
                    uint dbCode = DBTools.QueryLogin(packet.Username, packet.Password);
                    Debug.Print($"db查询结果={dbCode}");

                    var loginResult = new LoginResult { Code = dbCode, Username = packet.Username, Token = "000000" };
                    byte[] loginData = ProtobufferTool.PackMessage(SCID.S2CLogin, loginResult);
                    SendAsync(loginData);
                    break;
                default: //处理成文本
                    string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
                    Debug.Print($"Server Received: {message}");
                    break;
            }

            //string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            //Debug.Print("Incoming: " + message);

            // Multicast message to all connected sessions
            //Server.Multicast(message);

            // If the buffer starts with '!' the disconnect the current session
            //if (message == "!")
            //    Disconnect();
        }

        protected override void OnError(SocketError error)
        {
            Debug.Print($"Chat TCP session caught an error with code {error}");
        }
    }

    public class ChatServer : TcpServer
    {
        public ChatServer(IPAddress address, int port) : base(address, port) {}

        protected override TcpSession CreateSession() { return new ChatSession(this); }

        protected override void OnError(SocketError error)
        {
            Debug.Print($"Chat TCP server caught an error with code {error}");
        }
    }

    public class TCPChatServer
    {
        protected static ChatServer server;

        public static void Run()
        {
            // TCP server port
            int port = 1111;

            Debug.Print($"TCP server port: {port}");

            // Create a new TCP chat server
            server = new ChatServer(IPAddress.Any, port);

            // Start the server
            Debug.Print("Server starting...");
            server.Start();
            Debug.Print("Done!");

            //Debug.Print("Press Enter to stop the server or '!' to restart the server...");

            /*
            // Perform text input
            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Debug.Print("Server restarting...");
                    server.Restart();
                    Debug.Print("Done!");
                    continue;
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                server.Multicast(line);
            }

            // Stop the server
            Debug.Print("Server stopping...");
            server.Stop();
            Debug.Print("Done!");
            */
        }

        public static void Stop()
        {
            // Stop the server
            Debug.Print("Server stopping...");
            server.Stop();
            Debug.Print("Done!");
        }
    }
}