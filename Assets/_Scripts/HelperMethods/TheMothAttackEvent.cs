using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMothAttackEvent : MonoBehaviour
{
    public void Attack()
    {
        GetComponentInParent<EnemyTheMoth>().Attack();
    }
}
