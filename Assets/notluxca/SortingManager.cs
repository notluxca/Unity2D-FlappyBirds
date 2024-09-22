using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public GameObject box;
    public float boxQuantity;
    public float positionOffset;
    public Vector3 initPosition;
    private Vector3 currentPositionOffset;
    public Transform initPositionObject;
    public Color unselected;
    public Color selected;

    public AudioClip popSound;
    public AudioSource audioSource;

    public float smoothingTime = 0.5f; // Tempo de suavização da interpolação

    public float TickDelay; // (Mili-seconds) this have no actual correlation with the game ticks, is just for measuring a delay betway each iterartion of the sorting algorithim

    public List<GameObject> boxArr = new List<GameObject>();

    void Start()
    {
        SpawnInitialCubes();
        audioSource = GetComponent<AudioSource>();
    }


    void SpawnInitialCubes(){
        for(int i = 0; i < boxQuantity; i++){
            initPosition = initPositionObject.transform.position;
            GameObject currentBox = Instantiate(box, initPosition + currentPositionOffset, Quaternion.identity);
            currentPositionOffset.x += positionOffset;
            currentBox.transform.localScale = new Vector3(currentBox.transform.localScale.x, Random.Range(0.5f, 6.5f)); // we must guarantee that there will be no boxes with the same scale, 
            float boxHeight = currentBox.GetComponent<SpriteRenderer>().bounds.extents.y;
            currentBox.transform.position = new Vector2(currentBox.transform.position.x, currentBox.transform.position.y + boxHeight);
            currentBox.gameObject.name = $"box{i}";
            boxArr.Add(currentBox);
            // append it to a array
            // since we dont do complex operations in bubble sort I just need to 
            // loop the full array and invert pairs until array is fully sorted
        }
    }

    void DestroyThemAll(){

        foreach (GameObject box in boxArr)
        {
            Destroy(box);    
        }
        boxArr.Clear();
        currentPositionOffset.x = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            DestroyThemAll();
            SpawnInitialCubes();
        } else if(Input.GetKeyDown(KeyCode.G)){
            StartCoroutine(BubbleSort());
        }
        
    }


    // I want to the sorting algorithim to be based on a corountine so i can control its executionTime

    void SwitchBoxPosition(GameObject a, GameObject b){
        Vector2 tempPosition = b.transform.position;
        b.transform.position = new Vector2(a.transform.position.x, b.transform.position.y);
        a.transform.position = new Vector2(tempPosition.x, a.transform.position.y);
    }

    

    IEnumerator BubbleSort(){
        var n = boxArr.Count;
        bool swapRequired;
        for (int i = 0; i < n - 1; i++) 
        {
            yield return new WaitForSeconds(TickDelay);
            swapRequired = false;
            for (int j = 0; j < n - i - 1; j++)
                if (boxArr[j].transform.localScale.y > boxArr[j + 1].transform.localScale.y)
                {
                    boxArr[j].GetComponent<SpriteRenderer>().color = selected;
                    boxArr[j].GetComponent<SpriteRenderer>().color = unselected;
                    var tempVar = boxArr[j];
                    boxArr[j] = boxArr[j + 1];
                    boxArr[j + 1] = tempVar;
                    SwitchBoxPosition(boxArr[j], boxArr[j + 1]);
                    PlaySound(popSound);
                    swapRequired = true;
                }
            if (swapRequired == false)
                break;
        }

        void PlaySound(AudioClip sound){
            audioSource.PlayOneShot(sound);
        }

    }








    // IEnumerator BubbleSort(){
    //     var n = boxArr.Count;
    //     bool swapRequired;
    //     for (int i = 0; i < n - 1; i++) 
    //     {
    //         yield return new WaitForSeconds(TickDelay);
    //         swapRequired = false;
    //         for (int j = 0; j < n - i - 1; j++)
    //             if (boxArr[j].transform.localScale.y > boxArr[j + 1].transform.localScale.y)
    //             {
    //                 var tempVar = boxArr[j];
    //                 boxArr[j] = boxArr[j + 1];
    //                 boxArr[j + 1] = tempVar;
    //                 SwitchBoxPosition(boxArr[j], boxArr[j + 1], 0);
    //                 swapRequired = true;
    //             }
    //         if (swapRequired == false)
    //             break;
    //     }
    // }



}
