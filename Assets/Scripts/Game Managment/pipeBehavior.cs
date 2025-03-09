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

    void Start()
    {

    }
    void OnEnable()
    {
        birdController.OnPlayerDied += PlayerDied;
    }


    void PlayerDied()
    {
        Debug.Log("chamei");
        _pipeSpeed = 0;
    }

    void OnDestroy()
    {
        birdController.OnPlayerDied -= PlayerDied;
    }

    void FixedUpdate()
    {
        // _pipeSpeed = GameManager.Instance.pipeSpeed; // should not acess gamemanager
        rb.linearVelocity = new Vector2(-1 * _pipeSpeed, 0);
    }


}
