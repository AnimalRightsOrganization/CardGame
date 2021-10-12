using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using TcpChatClient;

public class UI_Login : UIBase
{
    [SerializeField] InputField m_UsernameInput;
    [SerializeField] InputField m_PasswordInput;
    [SerializeField] Button m_LoginBtn;
    ChatClient client;

    void Awake()
    {
        m_UsernameInput = transform.Find("Body/UsernameInput").GetComponent<InputField>();
        m_PasswordInput = transform.Find("Body/PasswordInput").GetComponent<InputField>();
        m_LoginBtn = transform.Find("Body/LoginBtn").GetComponent<Button>();

        m_LoginBtn.onClick.AddListener(DoLogin);
    }

    void Start()
    {
        DoConnect();
    }

    void OnDestroy()
    {
        client.DisconnectAndStop();
    }

    void DoConnect()
    {
        // TCP server address
        string address = "127.0.0.1";

        // TCP server port
        int port = 1111;

        // Create a new TCP chat client
        client = new ChatClient(address, port);

        // Connect the client
        Debug.Log($"Client connecting...{address}:{port}");
        client.ConnectAsync();
    }

    void DoLogin()
    {
        Login login = new Login { Username = m_UsernameInput.text, Password = m_PasswordInput.text };
        byte[] buffer = ProtobufferTool.Serialize(login);
        client.SendAsync(buffer);
    }
}