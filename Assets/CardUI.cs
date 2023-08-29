using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CardUI : MonoBehaviour
{
    private Card card;
    [SerializeField] private TextMeshProUGUI cardTitle;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quoteText;
    [SerializeField] private TextMeshProUGUI effectText;

    public void SetCard(Card card)
    {
        this.card = card;
        cardTitle.text = card.name;
        iconImage.sprite = card.artwork;
        quoteText.text = card.description;
        effectText.text = card.effectDescription;

    }
}
