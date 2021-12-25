using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    int score = 0;
    TextMeshProUGUI Tm;
    void Start()
    {
        Tm = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int a)
    {
        score += a;
    }

    void Update()
    {
        Tm.text = "" + score;
    }
}
