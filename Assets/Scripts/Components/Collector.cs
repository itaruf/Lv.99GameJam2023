using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    CircleCollider2D collectCollider;

    void Awake()
    {
        if (!collectCollider)
            TryGetComponent(out collectCollider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}
