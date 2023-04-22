using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D_Wind", menuName = "ScriptableObjects/D_Wind", order = 1)]
public class D_Wind : ScriptableObject
{
    [SerializeField] public Vector2 wind_force = new Vector2(0, 150);
}
