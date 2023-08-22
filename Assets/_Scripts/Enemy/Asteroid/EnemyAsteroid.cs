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
    private float rotationRate;

    private const string SPAWN_PROJ = "SpawnProj";

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<AIStateController>();
        internalSpawnProjTimer = projectileSpawnCD;
        rotationRate = Random.Range(1f, 15f);
        pooler = GameObject.Find("ProjPoolAsteroid").GetComponent<ObjectPooler>();
    }

    protected override void Update()
    {
        base.Update();
        internalSpawnProjTimer -= Time.deltaTime;
        if(controller.GetCurrentState() == spawnProjState && internalSpawnProjTimer <= 0)
        {
            SpawnProjectiles();
            internalSpawnProjTimer = projectileSpawnCD;
        }
    }

    private void FixedUpdate()
    {
        // Apply the torque to the Rigidbody
        Rb.AddTorque(Rb.inertia * Mathf.Deg2Rad * rotationRate);
    }

    private void SpawnProjectiles()
    {
        Animator.SetTrigger(SPAWN_PROJ);
        for(int i = 0; i < projectilesSpawned; i++)
        {
            Projectile proj = pooler.GetObjectFromPool().GetComponent<Projectile>();
            proj.gameObject.SetActive(true);
            proj.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            proj.Rb.AddForce(new Vector2(Random.Range(-spawnDeviationStart, spawnDeviationStart), Random.Range(-spawnDeviationStart, spawnDeviationStart)), ForceMode2D.Impulse);
        }
    }
}
