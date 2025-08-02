using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopGround : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public float width;
    private Vector2 startSize;
    public float speed = 5;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    void OnEnable()
    {
        BirdController.OnPlayerDied += StopLooping;
    }

    private void StopLooping()
    {
        speed = 0;
    }

    void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + speed * Time.deltaTime, spriteRenderer.size.y);
        if (spriteRenderer.size.x > width)
        {
            spriteRenderer.size = startSize;

        }
    }
}
