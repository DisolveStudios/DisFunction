using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFrameRate : MonoBehaviour
{
    [Header("Frame Rate Cap")]
    public int frameRateCap;

    public bool capFrameRate;

    private static float time;

    private static int frameCount;
    private static int frameRate;

    public static int GetCurrentFrameRate(float pollingTime) {
        time += Time.deltaTime;
        frameCount++;
        if(time >= pollingTime) {
            frameRate = Mathf.RoundToInt(frameCount / time);
            time -= pollingTime;
            frameCount = 0;
        }
        return frameRate;
    }

    void Update()
    {
        if (capFrameRate) {
            lockFrameRate(frameRateCap);
        }
    }

    private void lockFrameRate(int fps) {
        Application.targetFrameRate = fps;
    }
}
