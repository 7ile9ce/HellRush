using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            ScoreCount scoreсount = FindObjectOfType<ScoreCount>();
            if (scoreсount != null)
            {
                scoreсount.AddScore(value);
            }
            Destroy(gameObject); 
        }
    }
}
