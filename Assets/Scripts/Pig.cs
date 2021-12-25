using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private int hp = 10;
    Level L;

    private void Awake()
    {
        L = gameObject.transform.parent.transform.parent.GetComponent<Level>();
        L.pigNumber += 1;
    }
    void Die()
    {
        L.pigNumber -= 1;
        L.AddScore(500);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.01)
        {
            L.resetTimer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > hp)
        {
            Die();
        }
        else 
        {
            hp -= (int)collision.relativeVelocity.magnitude;
            L.AddScore(collision.relativeVelocity.magnitude);
        }

    }
}
