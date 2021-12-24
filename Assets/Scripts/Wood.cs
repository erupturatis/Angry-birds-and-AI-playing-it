using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{

    private int hp = 20;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > hp)
        {
            Destroy(gameObject);
        }
        else if(canLoseHp == true)
        {
            canLoseHp = false;
            Invoke("Switch", 0.1f);
            hp -= (int)collision.relativeVelocity.magnitude;
            //print(collision.relativeVelocity.magnitude);
        }
        
    }
}
