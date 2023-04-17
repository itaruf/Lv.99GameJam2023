using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField] WindData globalWindData;
    Vector2 force;

    public Delegate.D2 onPlayerEnterWind;

    public Wind currentWind { get; set; }

    void Awake()
    {
        if (globalWindData)
            force = globalWindData.wind_force;
    }

    public Vector2 GetGlobalWindForce()
    {
        return force;
    }

    void SetGlobalWindForce(Vector2 newVector)
    {
        force = newVector;
    }
}
