using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject spawnable;
        public float nbMax;
        public float frequencyOfApparition;
    }

    public Vector2 playerStartPosition;

    /*public List<Spawnable> spawnables;*/

    [UDictionary.Split(30, 70)]
    public SpawnableDictionary spawnables;

    [Serializable] public class SpawnableDictionary : UDictionary<EEntities, Spawnable> { }
    [Serializable] public class Key { public EEntities entity; }
    [Serializable] public class Value { public Spawnable spawnData; }
}
