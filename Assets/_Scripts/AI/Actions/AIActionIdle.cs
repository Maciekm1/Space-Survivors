using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/AIAction/AIActionIdle")]
public class AIActionIdle : AIAction
{
    public override void Do(AIStateController A)
    {
        base.Do(A);
    }

    public override void DoFixed(AIStateController A)
    {
        A.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
