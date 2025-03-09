using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.linearVelocity.y < 0){
            animator.SetTrigger("Falling");
            animator.ResetTrigger("GoingUp");
        } else if(rb.linearVelocity.y > 0.1) {
            animator.SetTrigger("GoingUp");
            animator.ResetTrigger("Falling");

        }
    }


}
