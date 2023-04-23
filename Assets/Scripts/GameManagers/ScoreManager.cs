using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int max_score = 9999;

    public TextMeshProUGUI score_text;

    public Delegate.D3 onPlayerScore;
    public Delegate.D4 onScoreChange;

    void Awake()
    {
        /*onScoreChange += SetScore;*/
    }

    public void SetScore(int newValue)
    {
        if (score == max_score)
            return;

        if (score + newValue > max_score)
        {
            score += (max_score - score);
            score_text.SetText(score.ToString());
        }

        else
        {
            score += newValue;
            score_text.SetText(score.ToString());
        }
    }

    public int GetScore()
    {
        return score;
    }
}
