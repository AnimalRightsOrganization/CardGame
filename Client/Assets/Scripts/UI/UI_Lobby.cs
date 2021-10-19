using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UIBase
{
    [SerializeField] Button m_StoryBtn;
    [SerializeField] Button m_MatchBtn;

    void Awake()
    {
        m_StoryBtn = transform.Find("StoryBtn").GetComponent<Button>();
        m_StoryBtn.onClick.AddListener(OnStoryMode);

        m_MatchBtn = transform.Find("MatchBtn").GetComponent<Button>();
        m_MatchBtn.onClick.AddListener(OnMatchMode);
    }

    void OnStoryMode()
    {
        Debug.Log("单机");

        //LogicManager.Instance.CreateRoom(3);
    }

    void OnMatchMode()
    {
        Debug.Log("匹配");

        EmptyPacket packet = new EmptyPacket();
        byte[] data = ProtobufferTool.PackMessage(CSID.C2SMatch, packet);
        NetManager.Instance.SendAsync(data);
    }
}