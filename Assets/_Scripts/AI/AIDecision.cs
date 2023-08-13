using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class AIDecision : ScriptableObject
{
    public virtual bool Decide(GameObject go)
    {
        return false;
    }
}
