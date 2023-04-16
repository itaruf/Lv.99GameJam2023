using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public int score_given = 0;
    public int speed_given = 1;

    void ICollectable.Collect()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
