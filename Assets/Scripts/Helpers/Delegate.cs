using System.Collections;
using UnityEngine;

public class Delegate<T>
{
    public delegate IEnumerator D1(GameObject entity);
    public delegate void D2(GameObject entity);
}