using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Rigidbody2D rb;
    public bool launched = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude == 0 && launched == true)
        {
            StartCoroutine(Hakai());
        }
    }
    IEnumerator Hakai()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
