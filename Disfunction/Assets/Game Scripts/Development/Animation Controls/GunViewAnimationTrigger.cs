using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunViewAnimationTrigger : MonoBehaviour
{
    public Animator animator;

    public bool lockAnimation = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y) && !lockAnimation) {
            animator.SetBool("isTrue", true);
        }
    }

    public void lockAnimationTrigger(bool _lock) {
        lockAnimation = _lock;
    }

    public void disableViewAnimation() {
        animator.SetBool("isTrue", false);
    }
}
