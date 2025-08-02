using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeBehavior : MonoBehaviour
{
    float _pipeSpeed = 2;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        BirdController.OnPlayerDied += PlayerDied;
    }

    void PlayerDied()
    {
        _pipeSpeed = 0;
    }

    void OnDestroy()
    {
        BirdController.OnPlayerDied -= PlayerDied;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-1 * _pipeSpeed, 0);
    }


}
