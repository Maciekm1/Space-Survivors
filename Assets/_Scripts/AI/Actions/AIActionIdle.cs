using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/AIAction/AIActionIdle")]
public class AIActionIdle : AIAction
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
