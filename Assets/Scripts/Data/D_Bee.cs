using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D_Bee", menuName = "ScriptableObjects/D_Bee", order = 1)]
public class D_Bee : D_Entity
{
    [SerializeField] public Vector2 bee_speed = new Vector2(1f, 1f);
    [SerializeField] public Vector2 bee_speed_loss_given = new Vector2(1f, 1f);
}
