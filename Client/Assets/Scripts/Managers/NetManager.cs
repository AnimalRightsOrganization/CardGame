using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcpChatClient;

[System.Serializable]
public class NetPacket
{
    public byte header;
    public byte[] body;
}
public class NetManager : MonoBehaviour
{
    static NetManager _instance;
    public static NetManager Instance { get { return _instance; } }

    private Queue<int> _states = new Queue<int>(); //网络状态
    private Queue<NetPacket> _packets = new Queue<NetPacket>(); //网络消息
    private ChatClient client;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnDestroy()
    {
        client.DisconnectAndStop();
    }

    void Update()
    {
        lock (_states)
        {
            if (_states.Count > 0)
            {
                int code = _states.Dequeue();
                NetStateManager.Trigger(code); //分发
            }
        }

        lock (_packets)
        {
            if (_packets.Count > 0)
            {
                var packet = _packets.Dequeue();
                NetPacketManager.Trigger(packet); //分发
            }
        }
    }

    public void Connect()
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

    public void OnNetStateReceived(int _code)
    {
        lock (_packets)
        {
            _states.Enqueue(_code);
        }
    }

    public void OnNetPacketReceived(byte _header, byte[] _body)
    {
        lock (_packets)
        {
            var pack = new NetPacket { header = _header, body = _body };
            _packets.Enqueue(pack);
        }
    }

    public void SendAsync(byte[] data)
    {
        client.SendAsync(data);
    }
}