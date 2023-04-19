using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Season : MonoBehaviour
{
    public List<Background> backgrounds;

    void Awake()
    {
        Background[] backgrounds_array = GetComponentsInChildren<Background>();

        if (backgrounds.Contains(null))
            backgrounds.Clear();

        if (backgrounds.Count != backgrounds_array.Length)
        {
            backgrounds.Clear();
            foreach(Background background in backgrounds_array)
            {
                backgrounds.Add(background);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
