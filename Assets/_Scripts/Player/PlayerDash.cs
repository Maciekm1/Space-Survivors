using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerController playerController;

    private float dashForce;
    private int dashChargesMax;
    private int dashCharges;
    private float dashRecharge;
    private float internalDashTimer;
    private bool isDashing;

    [SerializeField] private ParticleSystem dashExplosion;
    [SerializeField] Shader engineGlowShader;
    private Animator animator;

    // Events
    public event Action OnDashRecharge;

    private void OnEnable()
    {
        UpdateDashStats();

        animator = GetComponent<Animator>();
        playerInput.OnDashStarted += PlayerInput_OnDashStarted;
        dashCharges = dashChargesMax;
        internalDashTimer = 0;
    }

    private void PlayerInput_OnDashStarted()
    {

        if(dashCharges > 0)
        {
            //Animation and PS
            animator.SetTrigger("PlayerDash");
            dashExplosion.Play();


            isDashing = true;
            dashCharges--;
            if(internalDashTimer <= 0)
            {
                internalDashTimer = dashRecharge;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            playerController.Rb.AddForce(playerController.transform.up * dashForce);
            isDashing = false;
        }
    }

    private void Update()
    {
        if(dashCharges < dashChargesMax)
        {
            internalDashTimer -= Time.deltaTime;
            if(internalDashTimer <= 0)
            {
                dashCharges++;
                OnDashRecharge?.Invoke();
                if(dashCharges < dashChargesMax)
                {
                    internalDashTimer = dashRecharge;
                }
            }
        }
    }

    private void OnDisable()
    {
        playerInput.OnDashStarted -= PlayerInput_OnDashStarted;
    }

    public int GetDashCharges() { return dashCharges; }
    public int GetDashChargesMax() { return dashChargesMax; }

    public float GetDashChargesTimer() { return internalDashTimer; }

    public float GetDashChargesRecharge() { return dashRecharge; }

    public void SetDashChargesRecharge(float newValue) {  dashRecharge = newValue; }

    public void UpdateDashStats()
    {
        dashForce = playerController.GetPlayerStats().dashForce;
        dashChargesMax = playerController.GetPlayerStats().dashCharges;
        dashRecharge = playerController.GetPlayerStats().dashRegen;
    }
}
