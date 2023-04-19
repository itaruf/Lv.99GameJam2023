using System.Collections;
using UnityEngine;

public class Delegate
{
    public delegate IEnumerator D1(GameObject entity);
    public delegate void D2(GameObject entity);

    public delegate void D4(int value);
    public delegate void D3();

    public delegate void D5(ESeasons e_season);
}