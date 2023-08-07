using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    [SerializeField] private Image xpBar;

    [SerializeField] private Image dashCharges;

    [SerializeField] private TextMeshProUGUI playerLevelText;

    //Singleton
    public static PlayerUI instance;

    //References
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerLevel playerLevel;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        playerDash.OnDashRecharge += PlayerDash_OnDashRecharge;
    }

    private void PlayerDash_OnDashRecharge()
    {
        Debug.Log("Dash Recharged!");
        dashCharges.gameObject.GetComponent<Animator>().SetTrigger("DashRecharge");
    }

    private void OnDisable()
    {
        playerDash.OnDashRecharge -= PlayerDash_OnDashRecharge;
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateShieldBar();
        UpdateXpBar();

        UpdatePlayerDash();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = Mathf.Lerp(0, 1, playerHealth.GetPlayerHealth() / playerHealth.GetPlayerHealthMax());
    }

    private void UpdateShieldBar()
    {
        shieldBar.fillAmount = Mathf.Lerp(0, 1, playerHealth.GetPlayerShield() / playerHealth.GetPlayerShieldMax());
    }

    private void UpdateXpBar()
    {
        xpBar.fillAmount = Mathf.Lerp(0, 1, playerLevel.GetPlayerXP() / playerLevel.GetPlayerXPRequirement());
    }

    public void UpdatePlayerLevel()
    {
        playerLevelText.text = playerLevel.GetPlayerLevel().ToString();

        //DoTween
        playerLevelText.transform.DOScale(1.5f, .5f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            playerLevelText.transform.DOScale(1, .5f).SetEase(Ease.InOutSine);
        });
    }

    private void UpdatePlayerDash()
    {
        if(playerDash.GetDashChargesMax() > 4) { Debug.LogError("UI Not Implemented for > 4 dashes !!!"); }
        float dashBarSize = .25f;
        dashCharges.fillAmount = dashBarSize * playerDash.GetDashCharges();

        if(playerDash.GetDashCharges() < playerDash.GetDashChargesMax())
        {
            dashCharges.fillAmount = (dashBarSize * playerDash.GetDashCharges()) + Mathf.Lerp(dashBarSize, 0, playerDash.GetDashChargesTimer() / playerDash.GetDashChargesRecharge());
        }
    }
}
