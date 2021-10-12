using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UIBase
{
    [SerializeField] Button m_closeButton;
    [SerializeField] Button m_createButton;
    [SerializeField] Button m_joinButton;

    void Awake()
    {
        m_closeButton = transform.Find("Close Button").GetComponent<Button>();
        m_createButton = transform.Find("Create Button").GetComponent<Button>();
        m_joinButton = transform.Find("Join Button").GetComponent<Button>();

        m_closeButton.onClick.AddListener(OnCloseButtonClick);
        m_createButton.onClick.AddListener(OnCreateButtonClick);
        m_joinButton.onClick.AddListener(OnJoinButtonClick);
    }

    void OnDestroy()
    {
        m_closeButton.onClick.RemoveListener(OnCloseButtonClick);
        m_createButton.onClick.RemoveListener(OnCreateButtonClick);
        m_joinButton.onClick.RemoveListener(OnJoinButtonClick);
    }

    void OnCloseButtonClick()
    {
        this.Pop();
    }

    void OnCreateButtonClick()
    {
        // 创建4人房间
        LogicManager.instance.CreateRoom(4);

        this.Pop();
    }

    void OnJoinButtonClick()
    {
        this.Pop();
    }
}