using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    [SerializeField] Transform minorCardTemplate;
    [SerializeField] Transform majorCardTemplate;
    [SerializeField] Transform ultimateCardTemplate;

    [SerializeField] Transform cardChooseScreen;
    [SerializeField] Transform cardParent;

    public void ShowCardUI()
    {
        addCardsToUI();
        cardChooseScreen.gameObject.SetActive(true);
    }

    public void HideCardUI()
    {
        cardChooseScreen.gameObject.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    private void addCardsToUI()
    {
        clearUICards();
        foreach (Card card in CardManager.Instance.GetCurrentPossibleCardsList())
        {
            Transform newCard = Instantiate(minorCardTemplate, cardParent);

            // Use a lambda function to add the listener
            Debug.Log("Button :" + newCard.GetComponent<Button>());
            Debug.Log($"Adding card to UI: {card.name}");
            newCard.GetComponent<Button>().onClick.AddListener(() => CardHolder.Instance.addNewCard(card));

            newCard.GetComponent<CardUI>().SetCard(card);
            newCard.gameObject.SetActive(true);
        }
    }

    private void clearUICards()
    {
        foreach(Transform child in cardParent)
        {
            if (child == minorCardTemplate || child == majorCardTemplate || child == ultimateCardTemplate) continue;
            Destroy(child.gameObject);
        }
    }
}
