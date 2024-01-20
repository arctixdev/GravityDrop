using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customMathTester : MonoBehaviour
{
    bool factorialTest()
    {
        Debug.Log("testing factorial function");
        int[] debugData = new int[11]
        {
            1, 1, 2, 6, 24, 120, 720, 5_040, 40_320, 362_880, 3_628_800
        };
        string debugInfoString = "";
        for (int i = 0; i <= 10; i++)
        {
            int x = mathStuff.factorial(i);
            if (x != debugData[i])
            {
                throw new IncorrectResultException("factorial function gave wrong return value");
            }
            debugInfoString += ("factorial of "+i+" is: "+x+"\n");
        }
        Debug.Log(debugInfoString);
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        factorialTest();
    }

}

public class IncorrectResultException : Exception
{
    public IncorrectResultException(string message) : base(message)
    {
    }
}
