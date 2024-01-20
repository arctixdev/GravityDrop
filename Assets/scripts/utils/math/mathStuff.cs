using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class mathStuff
{
    public static int factorial(int x)
    {
        int y = 1;
        for(int i = 0; i < x; i++)
        {
            y *= (x - i);
        }
        return y;
    }
}


