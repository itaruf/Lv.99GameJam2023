using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    [SerializeField] public GameObject entity;
}
