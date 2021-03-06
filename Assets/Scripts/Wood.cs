using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{

    private int hp = 20;
    Level L;

    private void Awake()
    {
        L = gameObject.transform.parent.transform.parent.GetComponent<Level>();
    }

    void Die()
    {
        L.AddScore(100);
        L.S.AddReward(1f);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
        {
            L.resetTimer();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > hp)
        {
            L.AddScore(collision.relativeVelocity.magnitude);
            Die();
        }
        else
        {
            hp -= (int)collision.relativeVelocity.magnitude;
            L.AddScore(collision.relativeVelocity.magnitude);
        }

    }
}
