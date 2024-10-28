using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFrameRate : MonoBehaviour
{
    public int frameRateCap;
    // Start is called before the first frame update
    void Update()
    {
        lockFrameRate(frameRateCap);
    }

    private void lockFrameRate(int fps) {
        Application.targetFrameRate = fps;
    }
}
