using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using eeGames.Widget;

public class wMainMenu : Widget
{
    [SerializeField, Range(2, 6)] int playerCount; //本局玩家数，开房间时确定

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
        ///TODO: 4位RoomID，由服务器分配
        int roomId = Random.Range(1000, 10000);

        ///TODO: 等待其他玩家加入
        LogicManager.instance.playerList.Clear();
        playerCount = Random.Range(2, 7);
        for (int i = 0; i < playerCount; i++)
        {
            GamePlayer player = new GamePlayer()
            {
                gameid = i,
                user_id = ""
            };
            LogicManager.instance.playerList.Add(player);
        }

        WidgetManager.Instance.Pop(this.Id, false);
    }

    void OnJoinButtonClick()
    {
        WidgetManager.Instance.Pop(this.Id, false);
    }

    void AddBot(int amount)
    {

    }

    #endregion
}
