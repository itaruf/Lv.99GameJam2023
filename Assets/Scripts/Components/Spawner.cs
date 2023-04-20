using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Spawner;

public class Spawner : MonoBehaviour, IActivity
{
    [System.Serializable]
    public struct Spawnable
    {
        public EntityData entity;
        public float nbMax;
        public float frequencyOfApparition;
        public float delayBeforeNextSpawn;
        [HideInInspector] public float currentDelayTimer;
    }

    [UDictionary.Split(30, 70)]
    public SpawnableDictionary spawnables;

    [Serializable] public class SpawnableDictionary : UDictionary<EEntities, Spawnable> { }
    [Serializable] public class Key { public EEntities entity; }
    [Serializable] public class Value { public Spawnable spawnData; }

    [HideInInspector] public List<Point> spawn_points;

    Coroutine c_spawn;

    void Awake()
    {
        Point[] spawn_points_array = GetComponentsInChildren<Point>();

        if (spawn_points.Contains(null))
            spawn_points.Clear();

        if (spawn_points.Count != spawn_points_array.Length)
        {
            spawn_points.Clear();
            foreach (Point spawn_point in spawn_points_array)
            {
                spawn_points.Add(spawn_point);
            }
        }

        StartSpawn();
    }

    void IActivity.Activation()
    {
        throw new NotImplementedException();
    }

    void IActivity.Deactivation()
    {
        throw new NotImplementedException();
    }

    public void StartSpawn()
    {
        if (c_spawn != null)
            return;

        c_spawn = StartCoroutine(SpawnTracking());
        IEnumerator SpawnTracking()
        {
            while (true)
            {
                for (int i = 0; i < spawnables.Count; ++i)
                {
                    EEntities eentity = spawnables.ElementAt(i).Key;
                    Spawnable spawnable = spawnables.ElementAt(i).Value;

                    if (spawnable.currentDelayTimer < spawnable.delayBeforeNextSpawn)
                    {
                        spawnable.currentDelayTimer += Time.deltaTime;
                    }

                    else if (spawnable.currentDelayTimer >= spawnable.delayBeforeNextSpawn)
                    { 
                        // We can spawn
                        spawnable.currentDelayTimer = 0;
                        Instantiate(spawnable);
                    }

                    spawnables[eentity] = spawnable;
                }

                yield return null;
            }
        }
    }

    public void StopSpawn()
    {
        if (c_spawn == null)
            return;

        StopCoroutine(c_spawn);
        c_spawn = null;
    }

    void Instantiate(Spawnable spawnable)
    {
        /*Instantiate(spawnable.spawnable, GetSpawnPoint().transform.localPosition, Quaternion.identity);*/
        Instantiate(spawnable.entity.entity, GetSpawnPoint().transform.position, Quaternion.identity);

    }

    public Point GetSpawnPoint()
    {
        Point point = null;
        StartCoroutine(RandomPointTracking());
        IEnumerator RandomPointTracking()
        {
            while (true)
            {
                int random = UnityEngine.Random.Range(0, spawn_points.Count);
                point = spawn_points[random];
                if (!point.isOccupied
                    &&
                    MathsHelper.CompareFloat(EntityHelper.GetPosition(point.gameObject).y, PlayerHelper.GetPlayerPosition().y, MathsHelper.EMathSymbol.HIGHER))
                {
                    yield break;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        point.StartOccupationDelay();
        return point;
    }
}
