using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player { get; private set; }

    public WindManager windManager;
    public ScoreManager scoreManager;

    void Awake()
    {
        if (!player)
            player = FindObjectOfType<Player>();

        if (!windManager)
            TryGetComponent(out windManager);

        if (!scoreManager)
            TryGetComponent(out scoreManager);
    }

    void Start()
    {
        if (!player)
            player = FindObjectOfType<Player>();

        if (!windManager)
            TryGetComponent(out windManager);

        if (!scoreManager)
            TryGetComponent(out scoreManager);
    }
}
