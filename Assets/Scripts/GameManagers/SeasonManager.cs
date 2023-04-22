using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeasonManager : MonoBehaviour
{
    public Delegate.D5 onSeasonChange;
    public Delegate.D4 onSeasonChangeIndex;
    public Delegate.D4 onSeasonStart;

    [HideInInspector] public int current_season_index = 0;

    KeyValuePair<ESeasons, Season> current_season;

    [UDictionary.Split(30, 70)]
    public SeasonDictionary season_map;

    [Serializable] public class SeasonDictionary : UDictionary<ESeasons, Season> { }
    [Serializable] public class Key { public ESeasons e_season; }
    [Serializable] public class Value { public Season season; }

    void Awake()
    {
        
    }

    public int GetNextSeasonIndex()
    {
        int nb_season = season_map.Count;

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

    public void SetSeason(int index)
    {
        if (index < 0 || index > season_map.Count - 1)
            return;

        KeyValuePair<ESeasons, Season> selected_season = season_map.ElementAt(index);

        if (selected_season.Key == current_season.Key)
            return;

        current_season = selected_season;

        Season season = current_season.Value;

        PlayerHelper.SetPlayerPosition(new Vector3(PlayerHelper.GetPlayerPosition().x, EntityHelper.GetBoxCollider2D(season.gameObject).bounds.min.y, 0));

        (season as IActivity).Activation();
    }
}
