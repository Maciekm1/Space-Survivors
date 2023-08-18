using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimeText;

    public void UpdateGameTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        String formattedString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        gameTimeText.text = formattedString;
    }
}
