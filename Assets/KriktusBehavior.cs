using UnityEngine;

public class KriktusBehavior : MonoBehaviour
{    
    [Header("Basic Infos")]
    [SerializeField] float minJumpForce;
    [SerializeField] float maxJumpForce;    
    Rigidbody2D rb;
    SpriteRenderer sr;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    

    void JumpRandomDirection(){
        // raycast front to check for walls (up angle)
        // if there is no wall Jump with a random force to that direction
        

    }
}
