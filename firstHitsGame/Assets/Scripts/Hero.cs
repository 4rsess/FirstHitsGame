using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private int livesCount = 5;
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator animation;
    private SpriteRenderer sprite;

    private States State
    {
        get { return (States)animation.GetInteger("state"); }
        set { animation.SetInteger("state", (int)value); }
    
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = States.HeroAnimation;

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Run()
    {
        if (isGrounded) State = States.runAnimation;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = States.jumpAnimation;
    }

}

public enum States
{
    HeroAnimation,
    runAnimation,
    jumpAnimation
}
