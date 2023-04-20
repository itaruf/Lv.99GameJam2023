using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathsHelper
{
    public enum EMathSymbol
    {
        EQUAL,
        HIGHER,
        LOWER,
        HIGHER_EQUAL,
        LOWER_EQUAL,
    }

   public static bool CompareFloat(float a, float b, EMathSymbol eMathSymbol)
    {
        switch(eMathSymbol)
        {
            case EMathSymbol.EQUAL:
                { return a == b; }
            case EMathSymbol.HIGHER:
                { return a > b; }
            case EMathSymbol.LOWER:
                { return a < b; }
            case EMathSymbol.HIGHER_EQUAL:
                { return a >= b; }
            case EMathSymbol.LOWER_EQUAL:
                { return a <= b; }
            default:
                return false;
        }
    }
}
