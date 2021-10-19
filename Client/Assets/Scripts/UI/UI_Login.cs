using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    void OnEnable()
    {
        NetStateManager.RegisterEvent(OnNetState);
        NetPacketManager.RegisterEvent(OnNetPacket);
    }

    void OnDisable()
    {
        NetStateManager.UnRegisterEvent(OnNetState);
        NetPacketManager.UnRegisterEvent(OnNetPacket);
    }

    void Start()
    {
        m_InfoPanel.SetActive(true);

        NetManager.Instance.Connect();
    }

    void OnNetState(int code)
    {
        switch (code)
        {
            case 0:
                Debug.Log("<color=red>Disconnected</color>");
                m_InfoPanel.SetActive(true);
                break;
            case 1:
                Debug.Log("<color=green>Connected</color>");
                m_InfoPanel.SetActive(false);
                break;
        }
    }
    void OnNetPacket(NetPacket pt)
    {
        SCID header = (SCID)pt.header;
        byte[] body = pt.body;
        switch (header)
        {
            case SCID.S2CRegister:
                {
                    var packet = ProtobufferTool.Deserialize<RegisterError>(body);
                    Debug.Log($"[{header}] Code={packet.Code}");
                    if (packet.Code == 100)
                    {
                        Debug.LogError("注册失败，用户名已存在");
                        var ui = UIManager.GetInstance().Push<UI_Toast>();
                        ui.Show("注册失败，用户名已存在");
                    }
                }
                break;
            case SCID.S2CLogin:
                {
                    var packet = ProtobufferTool.Deserialize<LoginResult>(body);
                    Debug.Log($"[{header}] Code={packet.Code}");
                    if (packet.Code == 0)
                    {
                        Debug.Log($"<color=green>登录成功, Username={packet.Username}, Token={packet.Token}</color>");
                        UIManager.GetInstance().Push<UI_Lobby>();
                        this.Pop();
                    }
                    else if (packet.Code == 100)
                    {
                        Debug.LogError("登录失败，用户名或密码错误");
                        var ui = UIManager.GetInstance().Push<UI_Toast>();
                        ui.Show("登录失败，用户名或密码错误");
                    }
                    else
                    {
                        Debug.LogError($"登录失败，其他原因：{packet.Code}");
                    }
                }
                break;
            //default: //处理成文本
            //    string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            //    Debug.Log($"Client Received: {message}");
            //    break;
        }
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
            var ui = UIManager.GetInstance().Push<UI_Toast>();
            ui.Show("请输入用户名");
            return;
        }
        if (string.IsNullOrEmpty(m_LoginPwdInput.text))
        {
            Debug.LogError("请输入密码");
            var ui = UIManager.GetInstance().Push<UI_Toast>();
            ui.Show("请输入密码");
            return;
        }
        Login packet = new Login { Username = m_LoginUsrInput.text, Password = m_LoginPwdInput.text };
        byte[] data = ProtobufferTool.PackMessage(CSID.C2SLogin, packet);
        NetManager.Instance.SendAsync(data);
    }

    void DoRegister()
    {
        if (m_RegistPwdInput1.text.Equals(m_RegistPwdInput2.text) == false)
        {
            Debug.LogError("密码不一致");
            var ui = UIManager.GetInstance().Push<UI_Toast>();
            ui.Show("密码不一致");
            return;
        }
        Register packet = new Register { Username = m_RegistUsrInput.text, Password = m_RegistPwdInput1.text };
        byte[] data = ProtobufferTool.PackMessage(CSID.C2SRegister, packet);
        NetManager.Instance.SendAsync(data);
    }
}