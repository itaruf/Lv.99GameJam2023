using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManagerHelper
{
    public static GameManager GetGameManager()
    {
        return Object.FindObjectOfType<GameManager>();
    }
}
