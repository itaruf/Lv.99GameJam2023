using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player { get; private set; }

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
