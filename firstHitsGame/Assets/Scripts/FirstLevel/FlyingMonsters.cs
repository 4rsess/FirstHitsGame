using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingMonsters : AllEntity
{
    private SpriteRenderer sprite;
    [SerializeField] private AIPath aiPath;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        livesCount = 4;
    }

    void Update()
    {
        sprite.flipX = aiPath.desiredVelocity.x <= 5f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            livesCount--;

        }

        if (livesCount < 1)
            Die();
    }

}
