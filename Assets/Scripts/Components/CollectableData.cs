using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectableData", order = 1)]
public class CollectableData : ScriptableObject
{
    [SerializeField] public int collectable_score_given = 0;
    [SerializeField] public Vector2 collectable_speed_given = new Vector2(1f, 1f);

    [HideInInspector][SerializeField] public Vector2 collectable_speed = new Vector2(1f, 1f);
    [SerializeField] public float collectable_follow_duration = 10f;
}
