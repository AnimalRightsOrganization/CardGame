using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using TcpChatClient;

public class UI_Login : UIBase
{
    [SerializeField] Text m_Title;
    [Header("登录")]
    [SerializeField] GameObject m_LoginPanel;
    [SerializeField] InputField m_LoginUsrInput;
    [SerializeField] InputField m_LoginPwdInput;
    [SerializeField] Button m_LoginBtn;
    [SerializeField] Button m_ToRegistBtn;
    [Header("注册")]
    [SerializeField] GameObject m_RegistPanel;
    [SerializeField] InputField m_RegistUsrInput;
    [SerializeField] InputField m_RegistPwdInput1;
    [SerializeField] InputField m_RegistPwdInput2;
    [SerializeField] Button m_RegisterBtn;
    [SerializeField] Button m_ToLoginBtn;
    [Header("连接")]
    [SerializeField] GameObject m_InfoPanel;
    ChatClient client;
    private Queue<int> _actions = new Queue<int>();

    void Awake()
    {
        m_Title = transform.Find("Title/Text").GetComponent<Text>();
        //
        m_LoginPanel = transform.Find("LoginBody").gameObject;
        m_LoginUsrInput = transform.Find("LoginBody/UsernameInput").GetComponent<InputField>();
        m_LoginPwdInput = transform.Find("LoginBody/PasswordInput").GetComponent<InputField>();
        m_LoginBtn = transform.Find("LoginBody/LoginBtn").GetComponent<Button>();
        m_LoginBtn.onClick.AddListener(DoLogin);
        m_ToRegistBtn = transform.Find("LoginBody/ToRegisterBtn").GetComponent<Button>();
        m_ToRegistBtn.onClick.AddListener(ToRegist);
        //
        m_RegistPanel = transform.Find("RegisterBody").gameObject;
        m_RegistUsrInput = transform.Find("RegisterBody/UsernameInput").GetComponent<InputField>();
        m_RegistPwdInput1 = transform.Find("RegisterBody/PasswordInput1").GetComponent<InputField>();
        m_RegistPwdInput2 = transform.Find("RegisterBody/PasswordInput2").GetComponent<InputField>();
        m_RegisterBtn = transform.Find("RegisterBody/RegisterBtn").GetComponent<Button>();
        m_RegisterBtn.onClick.AddListener(DoRegister);
        m_ToLoginBtn = transform.Find("RegisterBody/ToLoginBtn").GetComponent<Button>();
        m_ToLoginBtn.onClick.AddListener(ToLogin);
        //
        m_InfoPanel = transform.Find("InfoPanel").gameObject;
    }

    void Start()
    {
        m_InfoPanel.SetActive(true);
        DoConnect();
    }

    void OnDestroy()
    {
        client.DisconnectAndStop();
    }

    void Update()
    {
        lock (_actions)
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
    }

    void OnNetCallback(int id)
    {
        lock (_actions)
        {
            _actions.Enqueue(id);
        }
    }

    void DoConnect()
    {
        // TCP server address
        string address = "127.0.0.1";

        // TCP server port
        int port = 1111;

        // Create a new TCP chat client
        client = new ChatClient(address, port);
        EventManager.RegisterEvent(OnNetCallback);

        // Connect the client
        Debug.Log($"Client connecting...{address}:{port}");
        client.ConnectAsync();
    }

    void ToLogin()
    {
        m_LoginPanel.SetActive(true);
        m_RegistPanel.SetActive(false);
    }

    void ToRegist()
    {
        m_LoginPanel.SetActive(false);
        m_RegistPanel.SetActive(true);
    }

    void DoLogin()
    {
        if (string.IsNullOrEmpty(m_LoginUsrInput.text))
        {
            Debug.LogError("请输入用户名");
            return;
        }
        if (string.IsNullOrEmpty(m_LoginPwdInput.text))
        {
            Debug.LogError("请输入用户名");
            return;
        }
        Login packet = new Login { Username = m_LoginUsrInput.text, Password = m_LoginPwdInput.text };
        byte[] data = ProtobufferTool.PackMessage(CSID.C2SLogin, packet);
        client.SendAsync(data);
    }

    void DoRegister()
    {
        if (m_RegistPwdInput1.text.Equals(m_RegistPwdInput2.text) == false)
        {
            Debug.LogError("密码不一致");
            return;
        }
        Register packet = new Register { Username = m_RegistUsrInput.text, Password = m_RegistPwdInput1.text };
        byte[] data = ProtobufferTool.PackMessage(CSID.C2SRegister, packet);
        client.SendAsync(data);
    }
}