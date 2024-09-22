using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeBehavior : MonoBehaviour
{
    float _pipeSpeed;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
    }


    void FixedUpdate()
    {
        _pipeSpeed = GameManager.Instance.pipeSpeed;
        rb.velocity = new Vector2(-1 * _pipeSpeed, 0);
        
        
    }


}
