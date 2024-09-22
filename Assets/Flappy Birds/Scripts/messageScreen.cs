using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageScreen : MonoBehaviour
{
    private void Awake() {
        this.gameObject.SetActive(true);
    }
    
    public void killMySelf(){
        this.gameObject.SetActive(false);
    }
    
}
