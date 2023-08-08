using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/AIAction/AIActionChase")]
public class AIActionChase : AIAction
{
    [SerializeField] private float moveSpeed;
    private Vector2 towardsPlayer;

    public override void Do(AIStateController A)
    {
        towardsPlayer = PlayerController.Instance.transform.position - A.transform.position;
    }
    public override void DoFixed(AIStateController A)
    {
        A.GetComponent<Rigidbody2D>().velocity = towardsPlayer.normalized * moveSpeed;
    }
}
