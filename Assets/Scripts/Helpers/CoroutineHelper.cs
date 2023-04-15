using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelper
{
    public static void ClearCoroutine(MonoBehaviour mono, Coroutine coroutine)
    {
        mono.StopCoroutine(coroutine);
        coroutine = null;
    }
}
