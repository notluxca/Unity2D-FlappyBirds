using System;
using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    [Header("Basic Infos")]
    Rigidbody2D rb;
    public float jumpForce;
    public float rotationSpeed;
    public bool hasStartedGame;
    public bool isDead;
    public float jumpCooldown;
    public bool canJump;


    [Header("Components")]
    [SerializeField] ParticleSystem pSystem;
    [SerializeField] SpriteRenderer sr;


    [Header("Death Parts")]
    [SerializeField] GameObject[] BodyParts;

    [Header("Sounds")]
    [SerializeField] AudioClip birdFlapSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip bloodSplashSound;
    [SerializeField] AudioClip startWoosh;
    [SerializeField] AudioClip pointSound;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerScored;


    void Awake()
    {
        hasStartedGame = false;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        canJump = true;
    }
    private void FixedUpdate()
    {
        TiltBird();
    }

    public void TiltBird()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * rotationSpeed);
    }

    void Update()
    {
        // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isDead && canJump)
        if (Input.GetMouseButtonDown(0) && !isDead && canJump)
        {
            Jump();
            if (hasStartedGame == false)
            {
                hasStartedGame = true;
                GameEvents.TriggerGameStarted();
                GameManager.Instance?.PlaySound(startWoosh);
                unlockBird();
                // Jump();
            }
        }
    }

    void Jump()
    {
        GameManager.Instance.PlaySound(birdFlapSound);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    void unlockBird()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    // toca o efeito de sangue, sons de morte, trava a posição do player, spawna todas as partes do corpo.
    void ExplodeBird()
    {
        pSystem.Play();
        GameManager.Instance.PlaySound(bloodSplashSound); //! responsabilidade propria
        GameManager.Instance.PlaySound(hitSound);
        sr.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (GameObject body in BodyParts)
        {
            Instantiate(body, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnPlayerDied?.Invoke();  //fix move to playerEvents
        if (other.gameObject.transform.CompareTag("Obstacle"))
        {
            ExplodeBird();
            isDead = true;
        }
        else
        {
            GameManager.Instance.PlaySound(hitSound);
            GameManager.Instance.LoseGame();
            isDead = true;
        }
    }

    // player is only on trigger mode when he is already dead
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pointZone")) GameEvents.TriggerScoreAdded(1);
        if (other.gameObject.CompareTag("Ground")) ExplodeBird();
    }

}
