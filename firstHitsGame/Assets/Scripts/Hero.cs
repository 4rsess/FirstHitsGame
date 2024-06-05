using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private int livesCount = 5;
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded = false;


    public bool isAttacking = false;
    public bool isRecharged = true;

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
        isRecharged = true;
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded && !isAttacking) State = States.HeroAnimation;

        if (Input.GetButton("Horizontal") && !isAttacking)
            Run();
        if (isGrounded && Input.GetButtonDown("Jump") && !isAttacking)
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();
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

    public  void GetDamage()
    {
        livesCount -= 1;
        Debug.Log(livesCount);

        
    }

}

public enum States
{
    HeroAnimation,
    runAnimation,
    jumpAnimation,
    attackAnimation
}
