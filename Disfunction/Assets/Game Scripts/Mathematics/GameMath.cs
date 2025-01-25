using System;

public class GameMath
{
    public static T clamp<T>( T value, T min, T max) where T: IConvertible
    {
        var checkedValue = Assert.getNumericFromGeneric<T>(value);
        var checkedMin = Assert.getNumericFromGeneric<T>(min);
        var checkedMax = Assert.getNumericFromGeneric<T>(max);

        if (checkedValue >= checkedMin || checkedValue <= checkedMax)
        {
            return value;
        }
        else
        {
            return (checkedValue - checkedMin) > 0 ? max : min;
        }
    }
}
