using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    [Header("Basic Game Info")]
    [SerializeField] int score;
    [SerializeField] float groundSpeed;

    private static GameManager _instance;


    [Header("References")]
    public GameObject player;
    [SerializeField] TransitionController transitionAnimator;
    // public GameObject startScreen;
    // public GameObject blackScreen;
    // public TMP_Text scoreText;
    private bool gameRunning;
    private AudioSource audioSorce;

    // Property to access the instance of the GameManager
    public static GameManager Instance
    {
        get
        {
            // If _instance is null, find the GameManager object in the scene
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
                // If GameManager object not found, create a new GameObject with GameManager attached
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }

    }
    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // If no instance exists, set this as the instance
            _instance = this;
            // Make sure the GameManager object persists through scene changes
            DontDestroyOnLoad(gameObject);
        }
        Application.targetFrameRate = 120;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.buildIndex == 0) // Assuming 0 is the main menu or starting scene
            {
                InitializeAllReferences();
            }
        };
    }

    void OnEnable()
    {
        BirdController.OnPlayerScored += AddPoint;
    }

    void InitializeAllReferences()
    {
        gameRunning = true;
        // pipeOldSpeed = pipeSpeed;
        audioSorce = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        transitionAnimator = FindAnyObjectByType<TransitionController>();
    }

    public void StartGame()
    {
        //fadeOffStartScreen();
        // StartCoroutine("SpawnPipes");
    }

    public void LoseGame()
    {
        StartCoroutine(LostSequence());
    }

    public void RestartGame()
    {
        StartCoroutine(RestartTransition());
    }

    public void LoseGameNoJump()
    {
        StartCoroutine(LostSequenceNoJump());
    }

    public void AddPoint()
    {
        score += 1;
        // scoreText.text = score.ToString();
    }

    // use unity default sound playe to play game sounds at the center of the screen
    public void PlaySound(AudioClip sound)
    {
        audioSorce.PlayOneShot(sound);
    }


    IEnumerator LostSequence()
    {
        // pipeOldSpeed = pipeSpeed;
        // pipeSpeed = 0;
        RestartGame();
        player.GetComponent<CircleCollider2D>().isTrigger = true;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(9, 16f), ForceMode2D.Impulse);
        gameRunning = false;
        yield return new WaitForSeconds(2);
    }

    IEnumerator LostSequenceNoJump()
    {
        // pipeOldSpeed = pipeSpeed;
        // pipeSpeed = 0;
        player.GetComponent<CircleCollider2D>().isTrigger = true;
        gameRunning = false;
        yield return new WaitForSeconds(2);
    }

    IEnumerator RestartTransition()
    {
        gameRunning = false;
        yield return new WaitForSeconds(1.5f);
        // pipeSpeed = pipeOldSpeed;
        transitionAnimator.StartTransition();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        InitializeAllReferences();

    }


}
