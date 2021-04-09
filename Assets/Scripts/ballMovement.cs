using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
     Rigidbody2D rb2D;
    public string color;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
