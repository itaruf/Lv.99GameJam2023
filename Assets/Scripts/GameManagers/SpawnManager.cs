using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector2 playerStartPosition;
    public float yOffsetFromPlayerToDestroyEntity;

    KeyValuePair<ESeasons, Spawner> current_spawner;

    [UDictionary.Split(30, 70)]
    public SpawnableDictionary spawners_map;

    [Serializable] public class SpawnableDictionary : UDictionary<ESeasons, Spawner> { }
    [Serializable] public class Key { public EEntities entity; }
    [Serializable] public class Value { public Spawner spawnData; }

    void Awake()
    {
        SeasonManager season_manager = ManagerHelper.GetSeasonManager();
        season_manager.onSeasonStart += StartSeasonSpawn;
    }

    private void Start()
    {

    }

    void StartSeasonSpawn(int index)
    {
        current_spawner = spawners_map.ElementAt(index);
        current_spawner.Value.StartSpawn();
    }

    public void UpdateSpawner(ESeasons e_season)
    {
        KeyValuePair<ESeasons, Spawner> selected_season = new KeyValuePair<ESeasons, Spawner>(e_season, spawners_map[e_season]);

        if (selected_season.Key == current_spawner.Key)
            return;

        current_spawner.Value.StopSpawn();
        current_spawner = selected_season;
        if (current_spawner.Value)
        {
            current_spawner.Value.StartSpawn();
        }
    }

    public float GetYOffsetFromPlayerToDestroyEntity()
    {
        return yOffsetFromPlayerToDestroyEntity;
    }
}
