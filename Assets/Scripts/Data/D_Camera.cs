using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D_Camera", menuName = "ScriptableObjects/D_Camera", order = 1)]
public class D_Camera : ScriptableObject
{
    [SerializeField] public bool enable_camera_lag_speed = false;
    [SerializeField] public Vector2 camera_speed = new Vector2(1f, 1f);
    [SerializeField] public GameObject camera_target = null;
    [SerializeField] public Vector2 camera_offset_from_target = new Vector2(0f, 0f);
}
