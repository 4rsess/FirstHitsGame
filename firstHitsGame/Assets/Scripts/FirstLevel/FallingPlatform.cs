using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Invoke("FallPlatform", 0.3f);
            Destroy(gameObject, 0.5f); 
        }
    }

    void FallPlatform()
    {
        rb.isKinematic = false;
    }

}
