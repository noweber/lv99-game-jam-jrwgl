using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : Singleton<Progression>
{
    [SerializeField] private int m_numDraw = 2;
    [SerializeField] private int m_levelToRemoveR = 5;
    [SerializeField] private int m_levelToRemoveSR = 10;

    public Action<List<Card>> OnShowUI;

    private List<Card> m_cardForSelect = new List<Card>();
    private void Start()
    {
        Experience.Instance.OnLevelUp += DoLevelUp;
        CardSelectionUI.Instance.OnPlayerSelect += DoPlayerSelection;
    }
    private void DoLevelUp()
    {
        if(Experience.Instance.Level == m_levelToRemoveR)
        {
            CardPool.Instance.RemoveAllCardsByRareness(CardRareness.R);
        }
        if (Experience.Instance.Level == m_levelToRemoveSR)
        {
            CardPool.Instance.RemoveAllCardsByRareness(CardRareness.SR);
        }
        Time.timeScale = 0;
        m_cardForSelect = CardPool.Instance.DrawCards(m_numDraw);
        OnShowUI.Invoke(m_cardForSelect);

    }

    public void IncrementNumberChoice()
    {
        m_numDraw++;
        if(m_numDraw > 3)
        {
            m_numDraw = 3;
        }
    }
    private void DoPlayerSelection(int cardIndex)
    {
        Debug.Log(cardIndex);
        Debug.Log(m_cardForSelect.Count);

        if (cardIndex != -1)
        {
            //Not -1
            Card selectedCard = m_cardForSelect[cardIndex];
            m_cardForSelect.RemoveAt(cardIndex);
            selectedCard.DoUpgrade();
        }

        //Add back.
        foreach(Card card in m_cardForSelect)
        {
            CardPool.Instance.AddCard(card);
        }
        m_cardForSelect.Clear();
        //Use card.
        //selectedCard.DoUpgrade();

        //Debug.Log("Player select: " + cardIndex.ToString());
        Time.timeScale = 1.0f;
    }
}
