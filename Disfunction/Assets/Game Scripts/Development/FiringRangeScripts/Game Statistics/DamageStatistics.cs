using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatistics
{
    private int hits = 0;

    private int headShots = 0;
    private int bodyShots = 0;
    private int footShots = 0;

    private int bulletsUsed = 0;

    private float accuracy = 0.0f;
    private float headShotAccuracy = 0.0f;

    public void isHeadShot()
    {
        bulletsUsed++;
        hits++;
        headShots++;
    }

    public void isBodyShot()
    {
        bulletsUsed++;
        hits++;
        bodyShots++;
    }

    public void isFootShot()
    {
        bulletsUsed++;
        hits++;
        footShots++;
    }

    public void hitNone()
    {
        bulletsUsed++;
    }

    public float getAccuracy()
    {
        if (bulletsUsed == 0) {
            return this.accuracy;
        }

        this.accuracy = ((float)hits / (float)bulletsUsed) * 100;
        return this.accuracy;
    }

    public float getHeadAccuracy()
    {
        if (this.hits == 0 ) {
            return this.headShotAccuracy;
        }

        this.headShotAccuracy = ( (float)headShots / (float)hits) * 100;
        return this.headShotAccuracy;
    }

    public float[] endSession()
    {
        float accuracy = getAccuracy();
        float headAccuracy = getHeadAccuracy();

        this.headShots = 0;
        this.bodyShots = 0;
        this.footShots = 0;

        this.accuracy = 0.0f;
        this.headShotAccuracy = 0.0f;

        this.hits = 0;
        this.bulletsUsed = 0;

        return new float[] { accuracy, headAccuracy };
    }
}
