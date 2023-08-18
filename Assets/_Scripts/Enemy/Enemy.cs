using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [field:SerializeField] public float MoveSpeed {  get; protected set; }
    [field:SerializeField] public float Damage { get; private set; }
    [field:SerializeField] public float ExperienceGiven { get; private set; }
    [field: SerializeField]  public float KnockbackForce { get; internal set; }
    public float InternalHealthRechargeTimer { get; set; }

    public Rigidbody2D Rb { get; private set; }
    public Collider2D Col { get; private set; }
    public Health HealthComp { get; private set; }
    public FlashEffect Flash { get; private set; }
    public Animator Animator { get; private set; }
    public Vector2 TowardsPlayer { get; set; }
    protected float initialMoveSpeed;

    [SerializeField] protected bool hasDeathAnim;
    [SerializeField] AnimationClip deathAnimationClip;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        HealthComp = GetComponent<Health>();
        Flash = GetComponentInChildren<FlashEffect>();
        Animator = GetComponentInChildren<Animator>();
        initialMoveSpeed = MoveSpeed;
    }

    protected virtual void OnEnable()
    {
        Col.enabled = true;
        HealthComp.ResetHealthShield();
        HealthComp.OnLoseAllHealth += Health_OnLoseAllHealth;
        GameManager.Instance.OnGameEnd += DeathStart;
    }

    private void Health_OnLoseAllHealth()
    {
        Debug.Log("Death");
        PlayerController.Instance.PlayerLevel.PlayerGainXP(ExperienceGiven);
        if (hasDeathAnim)
        {
            DeathStart();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        TowardsPlayer = PlayerController.Instance.transform.position - transform.position;
    }

    private void DeathStart()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        Rb.velocity = Vector2.zero;
        Col.enabled = false;
        Animator.SetTrigger("Death");
        yield return new WaitForSeconds(deathAnimationClip.length);
        gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        HealthComp.OnLoseAllHealth -= Health_OnLoseAllHealth;
        GameManager.Instance.OnGameEnd -= DeathStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj;
        if (collision.TryGetComponent<Projectile>(out proj))
        {
            if (proj.playerDamage)
            {
                HealthComp.TakeDamage(1);

                //Knockback + visual
                Rb.AddForce(proj.Rb.velocity * proj.ProjectileKnockback, ForceMode2D.Impulse);
                Flash.StartFlash(.08f, 1);

                proj.DestroyProjectile();
            }
        }
    }
}
