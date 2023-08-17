using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoidBat : Enemy, IFlippable
{

    protected override void Update()
    {
        base.Update();
        Flip();
    }
    public void Flip()
    {
        if(Rb.velocity.x > 0)
        {
            transform.localScale = new Vector3 (-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
