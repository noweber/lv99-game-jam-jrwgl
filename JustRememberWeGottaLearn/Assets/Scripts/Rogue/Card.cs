using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using Unity.VisualScripting;

public enum CardRareness
{
    R,
    SR,
    SSR
}
public abstract class Card : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string m_EnglishName;
    [SerializeField] private string m_EnglishDescription;
    [SerializeField] private CardRareness m_Rareness;
    public string CardName
    {
        get { return m_EnglishName; }
    }

    public string CardDescription
    {
        get { return m_EnglishDescription; }
    }

    public CardRareness Rareness
    {
        get { return m_Rareness; }
    } 

    public void DoUpgrade()
    {
        Player.Instance.AddCard(this);
        PerformUpgrade();
    }

    
    protected abstract void PerformUpgrade();

    [Command()]
    public static void TestUpgradeByType(string typeName)
    {
        Card[] allCards = FindObjectsOfType<Card>();
        foreach(Card card in allCards)
        {
            if(card.GetType().Name == typeName)
            {
                card.PerformUpgrade();
                return;
            }
        }
    }

    [Command()]
    public static void TestUpgradeByCardName(string cardName)
    {
        Card[] allCards = FindObjectsOfType<Card>();
        foreach (Card card in allCards)
        {
            if (card.CardName == cardName)
            {
                card.PerformUpgrade();
                return;
            }
        }
    }
}
