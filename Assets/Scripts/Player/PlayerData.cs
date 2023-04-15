using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public Vector2 PLAYER_SPEED = new Vector2(2f, 2f);
    public Vector2 PLAYER_JUMP_VELOCITY = new Vector2(2f, 2f);

    public float PLAYER_GRAVITY_IN_WIND = 0f;
    public float PLAYER_GRAVITY = 10f;
}
