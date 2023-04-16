using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public Delegate.D3 onPlayerScore;
    public Delegate.D4 onScoreChange;

    public void SetScore(int newValue)
    {
        score += newValue;
    }

    public int GetScore()
    {
        return score;
    }
}
