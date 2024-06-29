using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : AllEntity
{

    public Transform LeftEdge; // Первая точка патрулирования
    public Transform RightEdge; // Вторая точка патрулирования
    private Transform targetPoint; // Текущая целевая точка

    private float speed = 4f; // Скорость движения
    private float jump = 10f;
    public float powerKnockback = 100f;
    public float detectionRange = 5f;
    private bool isChasingPlayer = false;
    private bool isGrounded = true;



    private Rigidbody2D rb;
    private Collider2D collider;
    private Transform player; 
    private Animator animator;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        targetPoint = LeftEdge; // Начальная целевая точка
        player = GameObject.FindGameObjectWithTag("Hero").transform;
        animator.SetBool("walking", true);
        StartCoroutine(RunTimer());

        livesCount = 7;
    }

    void Update()
    {
        if (!isGrounded) CheckGround();

        DetectPlayer();

        if (!isChasingPlayer)
            Patrol();
        

    }

    void Patrol()
    {
        // Движение к целевой точке
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        
        // Проверка достижения целевой точки с учётом размера коллайдера
        float distanceToTarget = transform.position.x - targetPoint.position.x;
        if (Mathf.Abs(distanceToTarget)<0.1f)
        {
            targetPoint = (targetPoint == LeftEdge) ? RightEdge: LeftEdge;
        }
        FlipSprite(targetPoint.position.x - transform.position.x);
    }


    void DetectPlayer()
    {
        // Проверка расстояния до игрока
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            isChasingPlayer = true;
            // Преследование игрока

            Vector3 targetPositionX = new Vector3(player.position.x, transform.position.y, transform.position.z);

            float deltaY = player.position.y - transform.position.y;
            if (isGrounded && deltaY>0.2)
            {
                isGrounded = false;
                Debug.Log("прыгнул");
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPositionX, speed * Time.deltaTime);


            FlipSprite(player.position.x - transform.position.x);
        }
        else
            isChasingPlayer = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            StartCoroutine(Knockback(collision));
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }

    }
    
    private IEnumerator Knockback(Collision2D collision)
    {

        Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
        rb.velocity = knockbackDirection * powerKnockback;

        yield return 0;
    }
    

    void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);

            animator.SetBool("running", true);

            speed = 6.5f;

            yield return new WaitForSeconds(5f);

            animator.SetBool("running", false);
        }
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        Debug.Log(colliders.Length);
        foreach (Collider2D col in colliders)
        {
            if (col != collider && col.bounds.max.y < collider.bounds.min.y)
            {
                isGrounded = true;
                Debug.Log("на земеле");
                break;
            }
        }
    }
}

