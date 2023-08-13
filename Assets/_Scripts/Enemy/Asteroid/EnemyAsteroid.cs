using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroid : Enemy
{
    [Header("Asteroid Settings")]
    [SerializeField] Projectile projectile;
    [SerializeField] private int projectilesSpawned = 2;
    [SerializeField] private float projectileSpawnCD = 5f;
    private float internalSpawnProjTimer;

    private ObjectPooler pooler;
    private AIStateController controller;
    [SerializeField] private AIState spawnProjState;
    [SerializeField] private float spawnDeviationStart;

    protected override void Awake()
    {
        base.Awake();
        pooler = GetComponent<ObjectPooler>();
        controller = GetComponent<AIStateController>();
        internalSpawnProjTimer = projectileSpawnCD;
    }

    private void Update()
    {
        internalSpawnProjTimer -= Time.deltaTime;
        if(controller.GetCurrentState() == spawnProjState && internalSpawnProjTimer <= 0)
        {
            SpawnProjectiles();
            internalSpawnProjTimer = projectileSpawnCD;
        }
    }

    private void SpawnProjectiles()
    {
        for(int i = 0; i < projectilesSpawned; i++)
        {
            Projectile proj = pooler.GetProjectileFromPool().GetComponent<Projectile>();
            proj.gameObject.SetActive(true);
            proj.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            proj.Rb.AddForce(new Vector2(Random.Range(-spawnDeviationStart, spawnDeviationStart), Random.Range(-spawnDeviationStart, spawnDeviationStart)), ForceMode2D.Impulse);
        }
    }
}
