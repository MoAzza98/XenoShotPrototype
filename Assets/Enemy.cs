using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public ParticleSystem muzzleFlash;
    public Transform projectilePoint;
    public WeaponManager weapon;

    public void Shoot()
    {
        weapon.muzzleFlash.Play();
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce((projectilePoint.forward * weapon.bulletVelocity) * 0.3f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 7f, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        //muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
