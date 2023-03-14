using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [HideInInspector] public WeaponManager weapon;
    public ParticleSystem impactMark;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseCritChance(float critInc)
    {
        weapon.critChance += critInc;

        //Never let the crit chance go out of range
        if (weapon.critChance > 100.0f)
        {
            weapon.critChance = 100.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {

            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();

            float randValue = Random.Range(0, 101);
            if (randValue < weapon.critChance)
            {
                enemyHealth.TakeDamage(weapon.critDamage, true);
            }
            else
            {
                enemyHealth.TakeDamage(weapon.damage, false);
            }
        } else if (collision.gameObject.GetComponent<PlayerHealth>())
        {

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            float randValue = Random.Range(0, 101);
            if (randValue < weapon.critChance)
            {
                playerHealth.TakeDamage(weapon.critDamage, true);
            }
            else
            {
                playerHealth.TakeDamage(weapon.damage, false);
            }
        }
        //impactMark.Play();
        Destroy(this.gameObject);

    }
}
