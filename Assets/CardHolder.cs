using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public static CardHolder Instance { get; private set; }

    List<Card> cardsChosen = new List<Card>();

    private PlayerController playerController;

    private void Awake()
    {
        Instance = this;
        playerController = GetComponent<PlayerController>();
    }

    public void addNewCard(Card card)
    {
        card.ApplyEffect(playerController);
        cardsChosen.Add(card);
    }

    public List<Card> getCardsChosenList()
    {
        return cardsChosen;
    }
}
