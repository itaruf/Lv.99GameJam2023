using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WindData", order = 1)]
public class WindData : ScriptableObject
{
    [SerializeField] public Vector2 wind_force = new Vector2(0, 150);
}
