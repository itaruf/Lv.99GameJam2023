using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D_WaterDrop", menuName = "ScriptableObjects/D_WaterDrop", order = 1)]
public class D_WaterDrop : D_Entity
{
    [SerializeField] public Vector2 waterdrop_speed_loss_given = new Vector2(1f, 1f);
}