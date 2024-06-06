using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Danger : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character"))
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)    
    {
        if (collision.gameObject.name.Equals("Character"))
        {
            
        }
    } 
   
}
