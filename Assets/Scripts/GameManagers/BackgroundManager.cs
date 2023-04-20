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

    void Start()
    {
        int nb_season = season_map.Count;
        
        if (nb_season > 0)
        {
            current_season = season_map.ElementAt(0);
            (current_season.Value as IActivity).Activation();
            foreach (Background season_background in current_season.Value.backgrounds)
            {
                EnableBackground(season_background);
            }

            for (int i = 1; i < nb_season; ++i)
            {
                foreach (Background season_background in season_map.ElementAt(i).Value.backgrounds)
                {
                    DisableBackground(season_background);
                }
            }
        }

        SeasonManager season_manager = ManagerHelper.GetSeasonManager();
        /*season_manager.onSeasonChange += SetBackground;*/
        season_manager.onSeasonChangeIndex += SetBackground;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SeasonManager season_manager = ManagerHelper.GetSeasonManager();
            SetBackground(season_manager.GetNextSeasonIndex());
            /*SetBackground(ESeasons.WINTER);*/
        }
    }

    void SetBackground(int index)
    {
        if (index < 0 || index > season_map.Count - 1)
            return;

        KeyValuePair<ESeasons, Season> selected_background = season_map.ElementAt(index);

        if (selected_background.Key == current_season.Key)
            return;

        (current_season.Value as IActivity).Deactivation();
        foreach (Background season in selected_background.Value.backgrounds)
        {
            DisableBackground(season);
        }

        SpawnManager spawnManager = ManagerHelper.GetSpawnManager();
        spawnManager.UpdateSpawner(selected_background.Key);

        current_season = selected_background;
        (current_season.Value as IActivity).Activation();
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

        (current_season.Value as IActivity).Deactivation();
        foreach (Background season in selected_background.Value.backgrounds)
        {
            DisableBackground(season);
        }

        SpawnManager spawnManager = ManagerHelper.GetSpawnManager();
        spawnManager.UpdateSpawner(selected_background.Key);

        current_season = selected_background;
        (current_season.Value as IActivity).Activation();
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
