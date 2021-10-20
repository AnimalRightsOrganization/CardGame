using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemCard : MonoBehaviour, IPointerDownHandler
{
    public CardAttribute m_Card;
    private RectTransform m_Rect;
    private Vector2 lastPos;

    public void InitData(CardAttribute _card)
    {
        m_Card = _card;
        m_Rect = GetComponent<RectTransform>();
        lastPos = m_Rect.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"点击：{this.gameObject}。{m_Card.ToString()}");

        m_Rect.anchoredPosition = lastPos + Vector2.up * 10;
    }

    public void ResetPosition()
    {
        m_Rect.anchoredPosition = lastPos;
    }
}