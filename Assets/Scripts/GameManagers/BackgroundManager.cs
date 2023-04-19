using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    KeyValuePair<ESeasons, Season> current_season;

    [UDictionary.Split(30, 70)]
    public BackgroundDictionary season_map;

    [Serializable] public class BackgroundDictionary : UDictionary<ESeasons, Season> { }
    [Serializable] public class Key { public ESeasons e_season; }
    [Serializable] public class Value { public Season season; }

    int inc = 0;

    void Awake()
    {
        int nb_season = season_map.Count;
        
        if (nb_season > 0)
        {
            current_season = season_map.ElementAt(0);
            foreach (Background season_background in current_season.Value.backgrounds)
            {
                EnableBackground(season_background);
            }
        }

        for (int i = 1; i < nb_season; ++i)
        {
            foreach (Background season_background in season_map.ElementAt(i).Value.backgrounds)
            {
                DisableBackground(season_background);
            }
        }

        SeasonManager season_manager = ManagerHelper.GetSeasonManager();
        season_manager.onSeasonChange += SetBackground;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //SetBackground(++inc);
            SetBackground(ESeasons.WINTER);
        }
    }

    void SetBackground(int index)
    {
        if (index < 0 || index > season_map.Count - 1)
            return;

        KeyValuePair<ESeasons, Season> selected_background = season_map.ElementAt(index);

        if (selected_background.Key == current_season.Key)
            return;

        foreach(Background season in selected_background.Value.backgrounds)
        {
            DisableBackground(season);
        }

        current_season = selected_background;

        foreach (Background season in selected_background.Value.backgrounds)
        {
            EnableBackground(season);
        }
    }

    void SetBackground(ESeasons e_season)
    {
        KeyValuePair<ESeasons, Season> selected_background = new KeyValuePair<ESeasons, Season>(e_season, season_map[e_season]);

        if (selected_background.Key == current_season.Key)
            return;

        foreach (Background season in selected_background.Value.backgrounds)
        {
            DisableBackground(season);
        }

        current_season = selected_background;

        foreach (Background season in selected_background.Value.backgrounds)
        {
            EnableBackground(season);
        }
    }

    void EnableBackground(Background season_background)
    {
        (season_background as IActivity).Activation();
    }

    void DisableBackground(Background season_background)
    {
        (season_background as IActivity).Deactivation();
    }
}
