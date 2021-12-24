using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private int hp = 10;
    private bool canLoseHp = true;
    void Start()
    {
        //print("111ceva");
    }

    void Update()
    {

    }

    void Switch()
    {
        canLoseHp = !canLoseHp;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > hp)
        {
            Die();
        }
        else if (canLoseHp == canLoseHp)
        {
            canLoseHp = false;
            Invoke("Switch", 0.1f);
            hp -= (int)collision.relativeVelocity.magnitude;
            //print(collision.relativeVelocity.magnitude);
        }

    }
}
