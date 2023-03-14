using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bullet;
    [SerializeField] public GameObject impact;
    [SerializeField] public Transform barrelPos;
    [SerializeField] public float bulletVelocity;
    [SerializeField] public int bulletsPerShot;
    [SerializeField] TextMeshProUGUI extraAmmo;
    [SerializeField] TextMeshProUGUI currentAmmo;
    public float damage = 20;
    public float critDamage = 35;
    [SerializeField] public float critChance = 15.0f;
    AimStateManager aim;
    public ParticleSystem muzzleFlash;
    public ParticleSystem magBullet;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    WeaponAmmo ammo;
    WeaponBloom bloom;
    ActionStateManager actions;
    WeaponRecoil recoil;

    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        actions = GetComponentInParent<ActionStateManager>();
        recoil = GetComponent<WeaponRecoil>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (aim.controlSystem.isDisabled)
        {
            if (Shouldfire()) Fire();
            muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
        }
        else {
            return;
        }

    }

    bool Shouldfire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (actions.currentState == actions.Reload) return false;
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
        barrelPos.localEulerAngles = bloom.BarrelBloom(barrelPos);

        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlashLight();
        ammo.currentAmmo--;
        currentAmmo.text = ammo.currentAmmo.ToString();
        extraAmmo.text = ammo.extraAmmo.ToString();
        for (int i = 0; i < bulletsPerShot; i++)
        {

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aim.aimMask))
            {

                Instantiate(impactEffect, hit.point + (hit.normal * 0.1f), Quaternion.FromToRotation(Vector3.up, hit.normal));

            }

            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlashLight()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
