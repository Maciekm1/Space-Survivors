using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] CardUIManager cardUIManager;
    [System.Serializable]
    public class CardChoiceInfo
    {
        [SerializeField] public Card card;
        [SerializeField] public float minTimeElapsed;
    }

    [SerializeField] List<CardChoiceInfo> minorCards;
    [SerializeField] List<CardChoiceInfo> majorCards;
    [SerializeField] List<CardChoiceInfo> ultimateCards;

    List<Card> currentPossibleCards = new List<Card>();
    [SerializeField] private int numberOfCardOptions = 3;

    public static CardManager Instance { get; private set; }

    //When card chosen, run the addNewCard method from CardHolder

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerController.Instance.PlayerLevel.OnLevelUp += PlayerLevel_OnLevelUp;
    }

    private void PlayerLevel_OnLevelUp()
    {
        GameManager.Instance.PauseGame();
        getNewCardOptions();
        cardUIManager.ShowCardUI();
    }

    private void getNewCardOptions()
    {
        currentPossibleCards.Clear();

        List<CardChoiceInfo> availableCards = new List<CardChoiceInfo>();

        // Determine which card list to use based on player level or other criteria
        // For this example, let's assume you're using minor cards
        availableCards = minorCards;

        float currentTime = GameManager.Instance.getGameTime();

        foreach (CardChoiceInfo cardChoice in availableCards)
        {
            if (currentTime >= cardChoice.minTimeElapsed)
            {
                currentPossibleCards.Add(cardChoice.card);
            }
        }

        // Shuffle the list of available cards
        Shuffle(currentPossibleCards);

        currentPossibleCards = currentPossibleCards.GetRange(0, Mathf.Min(numberOfCardOptions, currentPossibleCards.Count));
    }

    // Shuffle the list using Fisher-Yates shuffle algorithm
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public List<Card> GetCurrentPossibleCardsList()
    {
        return currentPossibleCards;
    }

}
