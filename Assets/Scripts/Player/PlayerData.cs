using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] public Vector2 player_speed = new Vector2(2f, 2f);
    [HideInInspector] [SerializeField] public Vector2 player_jump_velocity = new Vector2(2f, 2f);

    [SerializeField] public float player_gravity_in_wind = 0f;
    [SerializeField] public float player_gravity = 10f;

    [SerializeField] public Vector2 player_min_speed = new Vector2(2f, 2f);
    [SerializeField] public Vector2 player_max_speed = new Vector2(2f, 2f);
}
