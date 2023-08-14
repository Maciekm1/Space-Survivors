using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTheMoth : Enemy
{
    [Header("TheMoth Settings")]
    [SerializeField] Projectile projectile;
    [SerializeField] private int projectilesSpawned = 2;
    [SerializeField] private float projectileSpawnCD = 5f;
    [SerializeField] private float moveSpeedWhenHurt;
    private float internalSpawnProjTimer;

    private ObjectPooler pooler;
    private AIStateController controller;
    [SerializeField] private AIState spawnProjState;
    [SerializeField] Transform spawnProjPos;
    AIState startState;

    protected override void Awake()
    {
        base.Awake();
        pooler = GetComponent<ObjectPooler>();
        controller = GetComponent<AIStateController>();
        internalSpawnProjTimer = projectileSpawnCD;
        startState = controller.GetCurrentState();
    }

    private void Update()
    {
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
            SpawnProjectiles();
            internalSpawnProjTimer = projectileSpawnCD;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controller.SetState(startState);

    }
    private void SpawnProjectiles()
    {
        //Animator.SetTrigger("SpawnProj");
        for (int i = 0; i < projectilesSpawned; i++)
        {
            Projectile proj = pooler.GetProjectileFromPool().GetComponent<Projectile>();
            proj.gameObject.SetActive(true);
            proj.transform.SetPositionAndRotation(spawnProjPos.position, Quaternion.identity);
            proj.Shoot();
        }
    }
}
