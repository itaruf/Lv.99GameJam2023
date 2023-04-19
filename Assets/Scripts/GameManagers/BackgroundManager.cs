using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    KeyValuePair<ESeasons, Season> current_background;

    [UDictionary.Split(30, 70)]
    public BackgroundDictionary backgrounds_map;

    [Serializable] public class BackgroundDictionary : UDictionary<ESeasons, Season> { }
    [Serializable] public class Key { public ESeasons e_season; }
    [Serializable] public class Value { public Season season; }

    int inc = 0;

    void Awake()

    {
        int nb_backgrounds_renderer = backgrounds_map.Count;
        
        if (nb_backgrounds_renderer > 0)
        {
            current_background = backgrounds_map.ElementAt(0);
            foreach (Background season_background in current_background.Value.backgrounds)
            {
                EnableBackground(season_background);
            }
        }

        for (int i = 1; i < nb_backgrounds_renderer; ++i)
        {
            foreach (Background season_background in backgrounds_map.ElementAt(i).Value.backgrounds)
            {
                DisableBackground(season_background);
            }
        }
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
        if (index < 0 || index > backgrounds_map.Count - 1)
            return;

        KeyValuePair<ESeasons, Season> selected_background = backgrounds_map.ElementAt(index);

        if (selected_background.Key == current_background.Key)
            return;

        foreach(Background season in selected_background.Value.backgrounds)
        {
            DisableBackground(season);
        }

        current_background = selected_background;

        foreach (Background season in selected_background.Value.backgrounds)
        {
            EnableBackground(season);
        }
    }

    void SetBackground(ESeasons e_season)
    {
        KeyValuePair<ESeasons, Season> selected_background = new KeyValuePair<ESeasons, Season>(e_season, backgrounds_map[e_season]);

        if (selected_background.Key == current_background.Key)
            return;

        foreach (Background season in selected_background.Value.backgrounds)
        {
            DisableBackground(season);
        }

        current_background = selected_background;

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
