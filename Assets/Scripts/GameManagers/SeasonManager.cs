using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public Delegate.D5 onSeasonChange;
    public Delegate.D4 onSeasonChangeIndex;

    [HideInInspector] public int current_season_index = 0;

    void Awake()
    {
        
    }

    public int GetNextSeasonIndex()
    {
        int nb_season = ManagerHelper.GetBackgroundManager().season_map.Count;

        if (current_season_index == nb_season - 1)
        {
            current_season_index = 0;
        }
        else
        {
            ++current_season_index;
        }

        return current_season_index;
    }
}
