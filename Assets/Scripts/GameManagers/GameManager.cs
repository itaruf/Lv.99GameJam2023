using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player { get; private set; }

    public Wind currentWind { get; set; }

    public Delegate<GameObject>.D2 onPlayerEnterWind;



    void Awake()
    {
        if (!player)
            player = FindObjectOfType<Player>();
    }

    void Start()
    {
        if (!player)
            player = FindObjectOfType<Player>();
    }

    void Update()
    {
        
    }
}
