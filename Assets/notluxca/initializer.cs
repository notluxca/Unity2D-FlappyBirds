using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class initializer : MonoBehaviour
{
    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.player = GameObject.Find("Player");
        // gm.startScreen = GameObject.Find("message");
        // gm.blackScreen = GameObject.Find("Transition").transform.GetChild(0).gameObject; // pega a child do objeto
        // gm.scoreText = GameObject.Find("InGameScreen").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        // gm.pipeSpawnPosition = GameObject.Find("pipeSpawnPosition");
    }

}
