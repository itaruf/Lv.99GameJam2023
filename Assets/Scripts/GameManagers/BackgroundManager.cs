using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    KeyValuePair<ESeasons, Background> current_background_season;

    [UDictionary.Split(30, 70)]
    public BackgroundDictionary background_season_map;

    [Serializable] public class BackgroundDictionary : UDictionary<ESeasons, Background> { }
    [Serializable] public class Key { public ESeasons e_season; }
    [Serializable] public class Value { public Background background_season; }

    void Start()
    {
        int nb_season = background_season_map.Count;

        if (nb_season > 0)
        {
            current_background_season = background_season_map.ElementAt(0);
            Background background_season = current_background_season.Value;

            SeasonManager season_manager = ManagerHelper.GetSeasonManager();
            Season season = season_manager.season_map.ElementAt(0).Value;
            (season as IActivity).Activation();

            EnableBackground(background_season);

            for (int i = 1; i < nb_season; ++i)
            {
                DisableBackground(background_season_map.ElementAt(i).Value);
            }

            season_manager.onSeasonChangeIndex += SetBackground;
            season_manager.onSeasonStart(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SeasonManager season_manager = ManagerHelper.GetSeasonManager();
            SetBackground(season_manager.GetNextSeasonIndex());
        }
    }

    void SetBackground(int index)
    {
        if (index < 0 || index > background_season_map.Count - 1)
            return;

        KeyValuePair<ESeasons, Background> selected_background = background_season_map.ElementAt(index);

        if (selected_background.Key == current_background_season.Key)
            return;

        (current_background_season.Value as IActivity).Deactivation();
        DisableBackground(current_background_season.Value);

        current_background_season = selected_background;
        EnableBackground(current_background_season.Value);

        SeasonManager season_manager = ManagerHelper.GetSeasonManager();
        season_manager.SetSeason(index);

        SpawnManager spawnManager = ManagerHelper.GetSpawnManager();
        spawnManager.UpdateSpawner(selected_background.Key);
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
