using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCard : MonoBehaviour, IPointerDownHandler
{
    public CardAttribute m_Card;
    private RectTransform m_Rect;
    private Vector2 lastPos;
    private bool IsRaised = false;

    public void InitData(CardAttribute _card)
    {
        m_Card = _card;
        m_Rect = GetComponent<RectTransform>();
        lastPos = m_Rect.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"点击：{m_Card.ToString()}");

        if (IsRaised)
        {
            m_Rect.anchoredPosition = lastPos;
        }
        else
        {
            m_Rect.anchoredPosition = lastPos + Vector2.up * 10;
        }
        IsRaised = !IsRaised;
    }

    public void ResetPosition()
    {
        m_Rect.anchoredPosition = lastPos;
    }
}