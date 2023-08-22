using UnityEngine;

public class Card : ScriptableObject
{
    public new string name;
    public CardType type;
    public Sprite artwork;
    public string description;

    public enum CardType
    {
        Minor,
        Major,
        Ultimate
    }

    public virtual void ApplyEffect(PlayerController playerController) { }
}