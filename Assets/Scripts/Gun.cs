using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlush;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineSize = 30;


    public float reloadTime = 2f;
    private bool isReloading;



    public Animator animator;



    InputAction shoot;
    InputAction reload;

    void Start()
    {

        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        reload = new InputAction("Reload", binding: "<keyboard>/r");

        shoot.Enable();
        reload.Enable();

        currentAmmo = maxAmmo;

    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }



    // Update is called once per frame
    void Update()
    {

        if (currentAmmo == 0 && magazineSize > 0 && magazineSize == 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;
        
            
        

        bool isShooting = shoot.ReadValue <float>() == 1;
        animator.SetBool("isShooting", isShooting);
        

        if(isShooting && Time.time >=  nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();

        }
        if (currentAmmo == 0 && !isReloading)
        {
            StartCoroutine(Reload());   
        }

    }
    private void Fire()
    {

        AudioManager.instance.Play("Shoot");



        RaycastHit hit;

        muzzleFlush.Play();

        currentAmmo--;


        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null)

            {
                e.TakeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);

        }
    }

    IEnumerator Reload ()
    {
        isReloading = true;
        AudioManager.instance.Play("Reload");
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("isReloading", false);
        if (magazineSize >= maxAmmo ) 
        {
            currentAmmo = maxAmmo;
            magazineSize -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineSize;
            magazineSize = 0;
        }
        isReloading = false;
    }


}
