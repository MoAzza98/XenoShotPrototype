using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    AimStateManager aim;
    public ParticleSystem muzzleFlash;
    public ParticleSystem magBullet;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    WeaponAmmo ammo;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponent<WeaponAmmo>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Shouldfire()) Fire();
        Debug.Log(ammo.currentAmmo);
    }

    bool Shouldfire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        magBullet.Play();
        muzzleFlash.Play();
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        ammo.currentAmmo--;
        for(int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
