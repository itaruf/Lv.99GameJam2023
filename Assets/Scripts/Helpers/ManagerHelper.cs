using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManagerHelper
{
    public static GameManager GetGameManager()
    {
        if (!GameManager.gameManager)
            return Object.FindObjectOfType<GameManager>();
        else
            return GameManager.gameManager;
    }

    public static WindManager GetWindManager()
    {
        return GetGameManager().windManager;
    }

    public static ScoreManager GetScoreManager()
    {
        return GetGameManager().scoreManager;
    }

    public static Vector2 GetGlobalWindForce()
    {
        return GetWindManager().GetGlobalWindForce();
    }

    public static SeasonManager GetSeasonManager()
    {
        return GetGameManager().seasonManager;
    }

    public static BackgroundManager GetBackgroundManager()
    {
        return GetGameManager().backgroundManager;
    }

    public static SpawnManager GetSpawnManager()
    {
        return GetGameManager().spawnManager;
    }
}
