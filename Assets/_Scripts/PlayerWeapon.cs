using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    protected const string SHOOT = "Shoot";

    [SerializeField] protected float damageMultiplier;
    [SerializeField] protected float fireCoolDown;
    [SerializeField] protected float recoilStrength;

    [SerializeField] protected Projectile projectile;

    //Camera
    [SerializeField] protected CameraShake cameraShake;
    [SerializeField] protected bool cameraShakeActive;
    [SerializeField] protected float cameraAmp;
    [SerializeField] protected float cameraFreq;
    [SerializeField] protected float cameraShakeDuration;


    public abstract void Shoot();

}
