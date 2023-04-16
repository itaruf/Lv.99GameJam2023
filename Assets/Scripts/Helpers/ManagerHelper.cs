using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManagerHelper
{
    public static GameManager GetGameManager()
    {
        return Object.FindObjectOfType<GameManager>();
    }

    public static WindManager GetWindManager()
    {
        return GetGameManager().windManager;
    }

    public static ScoreManager GetScoreManager()
    {
        return GetGameManager().scoreManager;
    }
}
