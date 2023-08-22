using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    List<Card> cardsChosen;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void addNewCard(Card card)
    {
        card.ApplyEffect(playerController);
        cardsChosen.Add(card);
    }
}
