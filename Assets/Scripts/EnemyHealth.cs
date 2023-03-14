using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] CapsuleCollider col;
    public Animator anim;
    public GameObject damagePop;

    public void TakeDamage(float damage, bool isCrit)
    {
        if(health > 0)
        {
            //Instantiate popup
            DamagePopup.Create(Vector3.zero, (int)damage, isCrit);
            health -= damage;
            if (health <= 0)
            {
                EnemyDeath();
            }
        }
    }

    void EnemyDeath()
    {
        col.enabled = !col.enabled;
        anim.SetTrigger("Dead");
    }
}
