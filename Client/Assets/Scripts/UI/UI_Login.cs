﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using TcpChatClient;

public class UI_Login : UIBase
{
    [SerializeField] Text m_Title;
    [SerializeField] InputField m_UsernameInput;
    [SerializeField] InputField m_PasswordInput;
    [SerializeField] Button m_LoginBtn;
    [SerializeField] GameObject m_InfoPanel;
    ChatClient client;

    void Awake()
    {
        m_Title = transform.Find("Title/Text").GetComponent<Text>();
        m_UsernameInput = transform.Find("Body/UsernameInput").GetComponent<InputField>();
        m_PasswordInput = transform.Find("Body/PasswordInput").GetComponent<InputField>();
        m_LoginBtn = transform.Find("Body/LoginBtn").GetComponent<Button>();
        m_InfoPanel = transform.Find("InfoPanel").gameObject;

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

    private Queue<int> _actions = new Queue<int>();

    void Update()
    {
        if (_actions.Count > 0)
        {
            var id = _actions.Dequeue();
            //Debug.Log($"Update: {id}");

            switch (id)
            {
                case 0:
                    Debug.Log("<color=red>Disconnected</color>");
                    m_Title.text = "Connecting";
                    m_InfoPanel.SetActive(true);
                    break;
                case 1:
                    Debug.Log("<color=green>Connected</color>");
                    m_Title.text = "跑得快";
                    m_InfoPanel.SetActive(false);
                    break;
            }
        }
    }

    void OnNetCallback(int id)
    {
        lock (_actions)
        {
            _actions.Enqueue(id);
        }
        //Debug.Log($"OnNetCallback: {id}");
    }

    void DoConnect()
    {
        // TCP server address
        string address = "127.0.0.1";

        // TCP server port
        int port = 1111;

        // Create a new TCP chat client
        client = new ChatClient(address, port);
        //client.m_OnConnected_Callback = OnConnected;
        //client.m_OnDisconnected_Callback = OnDisconnected;
        EventManager.RegisterEvent(OnNetCallback);

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

    [ContextMenu("OnConnected")]
    void OnConnected()
    {
        Debug.Log("<color=green>Connected</color>");
        m_InfoPanel.SetActive(false);
    }

    [ContextMenu("OnDisconnected")]
    void OnDisconnected()
    {
        Debug.Log("<color=red>Disconnected</color>");
        m_InfoPanel.SetActive(true);
    }
}