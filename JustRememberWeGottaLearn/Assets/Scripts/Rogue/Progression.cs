using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : Singleton<Progression>
{
    [SerializeField] private int m_numDraw = 2;
    public Action<List<Card>> OnShowUI;

    private List<Card> m_cardForSelect = new List<Card>();
    private void Start()
    {
        Experience.Instance.OnLevelUp += DoLevelUp;
        CardSelectionUI.Instance.OnPlayerSelect += DoPlayerSelection;
    }
    private void DoLevelUp()
    {
        Time.timeScale = 0;
        m_cardForSelect = CardPool.Instance.DrawCards(m_numDraw);
        OnShowUI.Invoke(m_cardForSelect);

    }

    private void DoPlayerSelection(int cardIndex)
    {
        Debug.Log(cardIndex);
        Debug.Log(m_cardForSelect.Count);
        Card selectedCard = m_cardForSelect[cardIndex];
        m_cardForSelect.RemoveAt(cardIndex);

        foreach(Card card in m_cardForSelect)
        {
            CardPool.Instance.AddCard(card);
        }
        m_cardForSelect.Clear();
        //Use card.
        selectedCard.DoUpgrade();

        //Debug.Log("Player select: " + cardIndex.ToString());
        Time.timeScale = 1.0f;
    }
}
