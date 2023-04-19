using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player { get; private set; }

    public WindManager windManager;
    public ScoreManager scoreManager;
    public BackgroundManager backgroundManager;
    public SpawnManager spawnManager;
    public SeasonManager seasonManager;

    void Awake()
    {
        if (!player)
            player = FindObjectOfType<Player>();

        if (!windManager)
            TryGetComponent(out windManager);

        if (!scoreManager)
            TryGetComponent(out scoreManager);

        if (!backgroundManager)
            TryGetComponent(out backgroundManager);

        if (!spawnManager)
            TryGetComponent(out spawnManager);

        if (!seasonManager)
            TryGetComponent(out seasonManager);
    }

    void Start()
    {
        if (!player)
            player = FindObjectOfType<Player>();

        if (!windManager)
            TryGetComponent(out windManager);

        if (!scoreManager)
            TryGetComponent(out scoreManager);

        if (!backgroundManager)
            TryGetComponent(out backgroundManager);

        if (!spawnManager)
            TryGetComponent(out spawnManager);

        if (!seasonManager)
            TryGetComponent(out seasonManager);
    }
}
