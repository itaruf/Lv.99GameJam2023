using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public Vector2 windForce = new Vector2(0, 100);

    public Delegate.D2 onPlayerEnterWind;

    public Wind currentWind { get; set; }
}
