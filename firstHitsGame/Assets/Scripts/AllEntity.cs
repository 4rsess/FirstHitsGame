using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntity : MonoBehaviour
{
    protected int livesCount;

    public virtual void GetDamage()
    {
        livesCount--;
        if (livesCount < 1)
            Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
