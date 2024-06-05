using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSlug : AllEntity
{
    private void Start()
    {
        livesCount = 4;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            livesCount--;
            Debug.Log("у слизня " + livesCount + " жизней");
        }

        if (livesCount < 1)
            Die();
    }
}
