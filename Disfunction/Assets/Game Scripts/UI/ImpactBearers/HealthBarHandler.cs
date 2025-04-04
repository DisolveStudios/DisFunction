using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{

    public Image healthBar;
    public Dummy IB;

    private void Start()
    {
        float healthBarRatio = IB.health / healthBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
