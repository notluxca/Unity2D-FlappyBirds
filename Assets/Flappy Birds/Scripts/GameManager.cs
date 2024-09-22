using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{

    [Header("Basic Game Info")]
    [SerializeField] int score;
    [SerializeField] float pipeSpawnDelay;
    [SerializeField] float groundSpeed;
    public float pipeSpeed;
    float pipeOldSpeed;

    private static GameManager _instance;


    [Header("References")]
    public GameObject player;
    public GameObject startScreen;
    public GameObject blackScreen;
    public GameObject pipeSpawnPosition;
    public GameObject pipe;
    public GameObject pipeWithSaw;
    public TMP_Text scoreText;
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
                _instance = FindObjectOfType<GameManager>();
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
        InitializeAllReferences();
    }


    void InitializeAllReferences()
    {
        gameRunning = true;
        pipeOldSpeed = pipeSpeed;
        audioSorce = GetComponent<AudioSource>();
        score = 0;
    }

    public void StartGame()
    {
        fadeOffStartScreen();
        StartCoroutine("SpawnPipes");
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

    public void fadeOffStartScreen()
    {
        startScreen = GameObject.Find("message");
        startScreen.GetComponent<Animator>().SetTrigger("fadeOffTrigger");
    }

    public void AddPoint(int quantity)
    {
        score += quantity;
        scoreText.text = score.ToString();
    }
    // use unity default sound playe to play game sounds at the center of the screen
    public void PlaySound(AudioClip sound)
    {
        audioSorce.PlayOneShot(sound);
    }

    // decide wich pipe it is going to spawn at a random height
    IEnumerator SpawnPipes()
    {
        while (gameRunning)
        {
            float randomHeight = Random.Range(-12, -7);
            int randomChoice = Random.Range(0, 4);
            if (randomChoice < 3)
            {
                Instantiate(pipeWithSaw, new Vector3(pipeSpawnPosition.transform.position.x, randomHeight), Quaternion.identity);
                yield return new WaitForSeconds(pipeSpawnDelay);
            }
            else
            {
                Instantiate(pipe, new Vector3(pipeSpawnPosition.transform.position.x, randomHeight), Quaternion.identity);
                yield return new WaitForSeconds(pipeSpawnDelay);
            }

        }
    }


    IEnumerator LostSequence()
    {
        pipeOldSpeed = pipeSpeed;
        pipeSpeed = 0;
        player.GetComponent<CircleCollider2D>().isTrigger = true;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(9, 16f), ForceMode2D.Impulse);
        gameRunning = false;
        yield return new WaitForSeconds(2);
    }

    IEnumerator LostSequenceNoJump()
    {
        pipeOldSpeed = pipeSpeed;
        pipeSpeed = 0;
        player.GetComponent<CircleCollider2D>().isTrigger = true;
        gameRunning = false;
        yield return new WaitForSeconds(2);
    }

    IEnumerator RestartTransition()
    {
        gameRunning = false;
        blackScreen.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1);
        pipeSpeed = pipeOldSpeed;
        SceneManager.LoadScene(0);
        InitializeAllReferences();

    }


}
