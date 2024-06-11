using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : AllEntity
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private int health;
    [SerializeField] private float jumpForce = 10f;
    private int lungeImpulse = 14;
    private bool isGrounded = false;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;


    public bool isAttacking = false;
    public bool isRecharged = true;
    public bool isDashing = false;
    private bool isLungeOnCooldown = false;
    private bool isCrouching = false;


    public Transform attackPosition;
    public float attackRange;
    public LayerMask enemy;


    private Rigidbody2D rb;
    private Animator animation;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)animation.GetInteger("state"); }
        set { animation.SetInteger("state", (int)value); }
    
    }

    private void Awake()
    {
        livesCount = 5;
        health = livesCount;
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Instance = this;
        isRecharged = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded && !isAttacking && !isDashing && !isCrouching) 
            State = States.HeroAnimation;
        if (Input.GetButton("Horizontal") && !isAttacking && !isDashing)
            Run();
        if (isGrounded && Input.GetButtonDown("Jump") && !isAttacking && !isDashing && !isCrouching)
            Jump();
        if (Input.GetButtonDown("Fire1") && !isDashing && !isCrouching)
            Attack();
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing && !isLungeOnCooldown && !isCrouching)
            StartCoroutine(Lunge());
        if (Input.GetKey(KeyCode.S) && isGrounded && !isDashing && !isAttacking)
            Crouch();
        if (Input.GetKeyUp(KeyCode.S))
            StandUp();


        if (health > livesCount)
            health = livesCount;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if (i < livesCount)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }


    //Move
    private void Run()
    {
        if (isGrounded)
        {
            if (isCrouching)
            {
                State = States.crouchAnimation;
            }
            else
            {
                State = States.runAnimation;
            }
        }

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private IEnumerator Lunge()
    {
        isLungeOnCooldown = true;
        isDashing = true;
        float originalGravity = rb.gravityScale;

        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0); 

        Vector2 lungeDirection = sprite.flipX ? Vector2.left : Vector2.right;
        float dashSpeed = lungeImpulse / rb.mass;
        rb.velocity = new Vector2(lungeDirection.x * dashSpeed, 0);

        yield return new WaitForSeconds(0.35f); 

        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0, 0);
        isDashing = false;

        yield return new WaitForSeconds(3f);
        isLungeOnCooldown = false;
    }

    private void Crouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            State = States.crouchAnimation;
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(collider.size.x, collider.size.y / 2);
            collider.offset = new Vector2(collider.offset.x, collider.offset.y / 2);
        }
    }

    private void StandUp()
    {
        if (isCrouching)
        {
            isCrouching = false;
            State = States.HeroAnimation;
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(collider.size.x, collider.size.y * 2);
            collider.offset = new Vector2(collider.offset.x, collider.offset.y * 2);
        }
    }

    private void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attackAnimation;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());

        }
    }

    public override void GetDamage()
    {
        health -= 1;
        Debug.Log(livesCount);

        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
    }
    
    //end move


    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = States.jumpAnimation;
    }
    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<AllEntity>().GetDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

}

public enum States
{
    HeroAnimation,
    runAnimation,
    jumpAnimation,
    attackAnimation,
    crouchAnimation
}
