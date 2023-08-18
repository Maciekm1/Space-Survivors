using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private string parentName;
    private GameObject parent;

    [SerializeField] private GameObject projectile;
    [SerializeField] private int poolSize;
    [SerializeField] private bool poolCanExpand;

    private List<GameObject> projectiles = new();

    private void Start()
    {
        parent = new GameObject(parentName);
        for (int i = 0; i < poolSize; i++) 
        {
            NewProjectile();
        }
    }

    public GameObject GetProjectileFromPool()
    {
        foreach (GameObject proj in projectiles)
        {
            if (!proj.activeInHierarchy)
            {
                return proj;
            }
        }
        if (poolCanExpand)
        {
            return NewProjectile();
        }
        return null;
    }

    private GameObject NewProjectile()
    {
        GameObject proj = Instantiate(projectile);
        projectile.gameObject.SetActive(false);
        projectiles.Add(proj);
        proj.transform.parent = parent.transform;
        return proj;
    }
}
