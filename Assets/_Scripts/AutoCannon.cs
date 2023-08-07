using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AutoCannon : PlayerWeapon
{
    [SerializeField] private Transform leftPointer;
    [SerializeField] private Transform rightPointer;
    [SerializeField] private ObjectPooler pooler;

    private bool canShoot;

    private Animator animator;
    private float nextShootTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canShoot = true;
    }

    private void Update()
    {
        nextShootTime -= Time.deltaTime;
        if(nextShootTime <= 0f) 
        {
            canShoot = true;
        }
    }

    public override void Shoot()
    {
        if(canShoot)
        {
            animator.SetTrigger(SHOOT);
            nextShootTime = fireCoolDown;
            canShoot = false;
        }
    }

    public void ShootLeft() 
    {
        GameObject proj = pooler.GetProjectileFromPool();
        proj.transform.SetPositionAndRotation(leftPointer.position, transform.rotation);
        proj.SetActive(true);
        ApplyRecoil();
    }

    public void ShootRight()
    {
        GameObject proj = pooler.GetProjectileFromPool();
        proj.transform.SetPositionAndRotation(rightPointer.position, transform.rotation);
        proj.SetActive(true);
        ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        PlayerController.Instance.Rb.AddForce(-PlayerController.Instance.transform.up * recoilStrength);

        if (cameraShakeActive)
        {
            cameraShake.ShakeCamera(cameraAmp, cameraFreq, cameraShakeDuration);
        }
    }
}
