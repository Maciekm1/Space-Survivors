using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTheMoth : Enemy, IFlippable
{
    [Header("TheMoth Settings")]
    [SerializeField] Projectile projectile;
    [SerializeField] private int projectilesSpawned = 1;
    [SerializeField] private float projectileSpawnCD = 5f;
    [SerializeField] private float moveSpeedWhenHurt;
    private float internalSpawnProjTimer;

    private ObjectPooler pooler;
    private AIStateController controller;
    [SerializeField] private AIState spawnProjState;
    [SerializeField] private AIState rechargeState;
    [SerializeField] Transform spawnProjPos;
    AIState startState;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<AIStateController>();
        internalSpawnProjTimer = projectileSpawnCD;
        startState = controller.GetCurrentState();
        pooler = GameObject.Find("ProjPoolMoth").GetComponent<ObjectPooler>();
    }

    protected override void Update()
    {
        base.Update();
        Flip();
        if(HealthComp.GetHealthCurrent() == 1)
        {
            MoveSpeed = moveSpeedWhenHurt;
        }
        else
        {
            MoveSpeed = initialMoveSpeed;
        }
        internalSpawnProjTimer -= Time.deltaTime;
        if (controller.GetCurrentState() == spawnProjState && internalSpawnProjTimer <= 0)
        {
            Animator.SetTrigger("Attack"); // Temp, replace with Attack()
            internalSpawnProjTimer = projectileSpawnCD;
        }

        if(controller.GetCurrentState() == rechargeState)
        {
            Animator.SetBool("RechargeState", true);
        }
        else
        {
            Animator.SetBool("RechargeState", false);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controller.SetState(startState);

    }

    public void Attack()
    {
        SpawnProjectiles();
    }
    private void SpawnProjectiles()
    {
        //Animator.SetTrigger("SpawnProj");
        for (int i = 0; i < projectilesSpawned; i++)
        {
            Projectile proj = pooler.GetObjectFromPool().GetComponent<Projectile>();
            proj.gameObject.SetActive(true);
            proj.transform.SetPositionAndRotation(spawnProjPos.position, Quaternion.identity);
            proj.Shoot();
        }
    }

    public void Flip()
    {
        if(controller.GetCurrentState() == spawnProjState) // Spawning Projectiles
        {
            if (controller.GetCurrentState() == spawnProjState)
            {
                // Check if TowardsPlayer is not a zero vector
                if (TowardsPlayer != Vector2.zero)
                {
                    float playerDirection = Mathf.Sign(TowardsPlayer.x);
                    if (playerDirection < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
            }

        }
        else
        {
            // otherwise, face movement direction
            if (Rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
