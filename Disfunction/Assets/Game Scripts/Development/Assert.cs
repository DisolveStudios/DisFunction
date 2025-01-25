using UnityEngine;
using System;

// This is a work in progeres interface, do not use it.
public interface Numeric { }

public class Assert : MonoBehaviour
{
    public static bool isNumeric<T>()
    {
        if (typeof(T) == typeof(int) || typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(Numeric)) 
        {
            return true;
        }
        return false;
    }

    public static double getNumericFromGeneric<T>(T number) where T: IConvertible
    {
        if (isNumeric<T>())
        {
            return number.ToDouble(null);
        } else
        {
            throw new Exception("Invalid Numeric");
        }
    }
}
