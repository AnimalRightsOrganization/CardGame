using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TcpChatClient;

public class ClientTest : MonoBehaviour
{
    ChatClient client;
    public InputField input;
    public Button sendBtn;

    void Start()
    {
        Run();
    }

    public void Run()
    {
        // TCP server address
        string address = "127.0.0.1";

        // TCP server port
        int port = 1111;

        Debug.Log($"TCP server address: {address}");
        Debug.Log($"TCP server port: {port}");

        // Create a new TCP chat client
        client = new ChatClient(address, port);

        // Connect the client
        Debug.Log("Client connecting...");
        client.ConnectAsync();
        Debug.Log("Done!");

        Debug.Log("Press Enter to stop the client or '!' to reconnect the client...");
    }

    public void SendMsg()
    {
        client.SendAsync(input.text);
        Debug.Log($"Send: {input.text}");
    }
}
