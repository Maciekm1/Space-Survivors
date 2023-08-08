using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTrail : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.velocity.magnitude == 0)
        {
            trail.SetActive(false);
        }
        else
        {
            trail.SetActive(true);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -rb.velocity.normalized);
            rotation *= Quaternion.Euler(0f, 0f, 90f);
            trail.transform.rotation = rotation;
        }
    }
}
