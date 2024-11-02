using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunViewAnimationTrigger : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y)) {
            animator.SetBool("isTrue", true);
        }
    }

    public void disableViewAnimation() {
        animator.SetBool("isTrue", false);
    }
}
