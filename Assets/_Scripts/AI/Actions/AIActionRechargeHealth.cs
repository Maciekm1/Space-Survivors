using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIAction/AIActionRechargeHealth")]
public class AIActionRechargeHealth : AIAction
{
    [SerializeField] private float amount;
    [SerializeField] private float timer;
    public override void Do(GameObject go)
    {
        base.Do(go);
        Enemy enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (enemy.InternalHealthRechargeTimer > 0) { enemy.InternalHealthRechargeTimer -= Time.deltaTime; }
            if (enemy.InternalHealthRechargeTimer <= 0) {
                enemy.GetComponent<EnemyHealth>().GainHealth(amount);
                enemy.InternalHealthRechargeTimer = timer;
            }
        }
    }

    public override void DoFixed(GameObject go)
    {
        go.GetComponent<Enemy>().Rb.velocity = Vector2.zero;
    }

}
