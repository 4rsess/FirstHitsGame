using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Her : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 6.5f;
    private Vector2 moveVector;
    public float force = 100;
    bool isGround = true ;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }


    void Update()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveVector.x * speed, 0);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

    }
}
