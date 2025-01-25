using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Distance
{
    EXTREME_CLOSE,
    CLOSE,
    MILDLY_CLOSE,
    NEUTRAL,
    MILDLY_FAR,
    FAR,
    EXTREME_FAR
}

public class Unwrap
{
    private float threshold = 1.0f;
    private float stretch = 0.25f;

    public float extremeClose = 0.25f;
    public float close = 0.5f;
    public float mildlyClose = 0.75f;
    public float neutral = 1.0f;
    public float mildlyFar = 1.25f;
    public float far = 1.5f;
    public float extremeFar = 1.75f;

    /*
     * The proximity values of distance are made public so the distance values are flexible.
     * Once any of these values are changed after initialization, the values may not
     * evenly adapt according to 'threshold' factor.
     */

    public Unwrap(float threshold, float stretch)
    {
        this.threshold = GameMath.clamp(threshold, 0, float.MaxValue);
        this.updateStretch(stretch);
        this.updateThreshold(threshold);
    }

    // Could find a better way to do this, will check this out later(not much important).
    public void updateStretch(float stretch)
    {
        this.stretch = stretch;
        this.extremeClose += stretch;
        this.close += stretch * 2;
        this.mildlyClose += stretch * 3;
        this.neutral += stretch * 4;
        this.mildlyFar += stretch * 5;
        this.far += stretch * 6;
        this.extremeFar += stretch * 7;
    }

    public void updateThreshold(float threshold)
    {
        this.threshold = threshold;
        this.extremeClose *= threshold;
        this.close *= threshold;
        this.mildlyClose *= threshold;
        this.neutral *= threshold;
        this.mildlyFar *= threshold;
        this.far *= threshold;
        this.extremeFar *= threshold;
    }

    public float distnace(Distance distance)
    {
        switch (distance) {
            case Distance.EXTREME_CLOSE:
                return this.extremeClose;
            case Distance.CLOSE:
                return this.close;
            case Distance.MILDLY_CLOSE:
                return this.mildlyClose;
            case Distance.NEUTRAL:
                return this.neutral;
            case Distance.MILDLY_FAR:
                return this.mildlyFar;
            case Distance.FAR:
                return this.far;
            case Distance.EXTREME_FAR:
                return this.extremeFar;
        }
        return -1.0f;
    }
}