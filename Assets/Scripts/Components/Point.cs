using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool isOccupied = false;
    public float occupationDelay = 5f;
    Coroutine c_occupation;

    void Awake()
    {
        // On change background   
        // += StopOccupationDelay();
    }

    public void StartOccupationDelay()
    {
        if (c_occupation != null)
            return;

        float current_time = occupationDelay;

        c_occupation = StartCoroutine(OccupationDelayTracking());
        IEnumerator OccupationDelayTracking()
        {
            isOccupied = true;
            while (current_time > 0)
            {
                current_time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            StopOccupationDelay();
        }
    }

    public void StopOccupationDelay()
    {
        isOccupied = false;

        if (c_occupation == null)
            return;

        StopCoroutine(c_occupation);
        c_occupation = null;
    }
}
