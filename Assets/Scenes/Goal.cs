using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int Score;
    private void Awake()
    {
        scoreText.text = $"{Score}";
    }
}
