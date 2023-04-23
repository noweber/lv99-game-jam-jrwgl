using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardSelectionUI : Singleton<CardSelectionUI>
{
    public List<GameObject> CardSlots = new List<GameObject>();
    public List<Card> TestCards = new List<Card>();

    public Action<int> OnPlayerSelect;

    private void Start()
    {
        //ShowSelectionUI(TestCards);
        Progression.Instance.OnShowUI += ShowSelectionUI;
        gameObject.SetActive(false);
    }
    private void ShowSelectionUI(List<Card> cards)
    {
        gameObject.SetActive(true);
        for(int i = 0; i < CardSlots.Count; i++)
        {
            GameObject cardSlot = CardSlots[i];
            cardSlot.SetActive(true);
            if (i < cards.Count)
            {
                Card card = cards[i];
                cardSlot.SetActive(true);


                cardSlot.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = card.CardName;
                cardSlot.transform.Find("CardDescription").GetComponent<TextMeshProUGUI>().text = card.CardDescription;
                
                Button selectButton = cardSlot.transform.Find("SelectButton").GetComponent<Button>();
                int cardIndex = i;
                selectButton.onClick.AddListener(() => CardSelected(cardIndex));

                //Change card color based on its rareness
                switch (card.Rareness)
                {
                    case CardRareness.R:
                        cardSlot.transform.Find("RarenessBackground").GetComponent<Image>().color = Color.white;
                        break;
                    case CardRareness.SR:
                        cardSlot.transform.Find("RarenessBackground").GetComponent<Image>().color = Color.blue;
                        break;
                    case CardRareness.SSR:
                        cardSlot.transform.Find("RarenessBackground").GetComponent<Image>().color = Color.yellow;
                        break;
                }
            }
            else
            {
                cardSlot.SetActive(false);
            }
           
           
        }
    }


    private void CardSelected(int cardIndex)
    {
        //Debug.Log("Selected card: " + cardIndex.ToString());
        foreach(GameObject cardSlot in CardSlots)
        {
            Button selectButton = cardSlot.transform.Find("SelectButton").GetComponent<Button>();
            selectButton.onClick.RemoveAllListeners();
        }
        gameObject.SetActive(false);
        OnPlayerSelect.Invoke(cardIndex);
        // TODO: Implement the card selection logic
    }

}
