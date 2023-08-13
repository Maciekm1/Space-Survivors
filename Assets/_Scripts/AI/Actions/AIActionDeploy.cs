using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIAction/AIActionDeploy")]
public class AIActionDeploy : AIAction
{
    public override void Do(GameObject go)
    {
        base.Do(go);
    }

    public override void DoFixed(GameObject go)
    {
        go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
