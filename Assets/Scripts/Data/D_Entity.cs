using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D_Entity", menuName = "ScriptableObjects/D_Entity", order = 1)]
public class D_Entity : ScriptableObject
{
    [SerializeField] public GameObject entity;
}
