using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using eeGames.Widget;

public class wMainMenu : Widget
{
    [SerializeField] Button m_closeButton;
    [SerializeField] Button m_createButton;
    [SerializeField] Button m_joinButton;

    #region UNity Methods

    protected override void Awake()
    {
        m_closeButton.onClick.AddListener(OnCloseButtonClick);
        m_createButton.onClick.AddListener(OnCreateButtonClick);
        m_joinButton.onClick.AddListener(OnJoinButtonClick);
    }

    void OnDestroy()
    {
        m_closeButton.onClick.RemoveListener(OnCloseButtonClick);
        m_createButton.onClick.RemoveListener(OnCreateButtonClick);
        m_joinButton.onClick.RemoveListener(OnJoinButtonClick);

        base.DestroyWidget();
    }

    #endregion

    #region Helper Method

    void OnCloseButtonClick()
    {
        WidgetManager.Instance.Pop(this.Id, false);
    }

    void OnCreateButtonClick()
    {
        // 创建4人房间
        LogicManager.instance.CreateRoom(4);

        WidgetManager.Instance.Pop(this.Id, false);
    }

    void OnJoinButtonClick()
    {
        //WidgetManager.Instance.Pop(this.Id, false);
    }

    #endregion
}
