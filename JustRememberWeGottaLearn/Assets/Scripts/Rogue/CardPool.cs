using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class CardPool : Singleton<CardPool>
{
    [Serializable]
    private struct CardCount
    {
        public Card card;
        public int count;
    }

    [SerializeField] private List<CardCount> m_cardCount = new List<CardCount>();
    
    private List<Card> m_cards = new List<Card> ();


    public override void Awake()
    {
        //Populate the card pool
        foreach(CardCount cardCount in m_cardCount)
        {
            for(int i = 0; i < cardCount.count; i++)
            {
                Card card = Instantiate(cardCount.card);
                m_cards.Add(card);
                card.transform.parent = this.transform;
            }
        }
    }

    [Command()]
    public List<Card> DrawCards(int numDraw)
    {
        List<Card> drawnCards = new List<Card>();

        for (int i = 0; i < numDraw; i++)
        {
            if (m_cards.Count == 0)
            {
                Debug.LogWarning("Card pool is empty! Cannot draw more cards.");
                break;
            }

            int randomIndex = UnityEngine.Random.Range(0, m_cards.Count);
            Card drawnCard = m_cards[randomIndex];
            drawnCards.Add(drawnCard);
            m_cards.RemoveAt(randomIndex);
        }

        return drawnCards;
    }

    public void AddCard(Card card)
    {
        m_cards.Add(card);
    }

    public void RemoveAllCardsByRareness(CardRareness rareness)
    {
        m_cards.RemoveAll(card => card.Rareness == rareness);
    }
}
