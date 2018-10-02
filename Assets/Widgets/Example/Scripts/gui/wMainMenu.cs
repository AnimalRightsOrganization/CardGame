using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using eeGames.Widget;

public class wMainMenu : Widget
{
    [SerializeField] private Button m_closeButton;

    #region UNity Methods

    protected override void Awake()
    {
        m_closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    void OnDestroy()
    {
        m_closeButton.onClick.RemoveListener(OnCloseButtonClick);

        base.DestroyWidget();
    }

    #endregion

    #region Helper Method

    void OnCloseButtonClick()
    {
        WidgetManager.Instance.Pop(this.Id, false);
    }

    #endregion

    #region Widget Implementation 

    #endregion

    #region Utitity Methods

    /// <summary>
    /// this method is used by MiniGame
    /// </summary>
    /// <returns> randomly generated number between 1 - 99 </returns>
    public int GetRandomGeneratedNumber()
    {
        return UnityEngine.Random.Range(1, 99);
    }

    #endregion
}
