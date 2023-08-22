using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private string parentName;
    private GameObject parent;

    [SerializeField] private GameObject item;
    [SerializeField] private int poolSize;
    [SerializeField] private bool poolCanExpand;

    private List<GameObject> items = new();

    private void Start()
    {
        parent = new GameObject(parentName);
        for (int i = 0; i < poolSize; i++) 
        {
            NewObject();
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject i in items)
        {
            if (!i.activeInHierarchy)
            {
                return i;
            }
        }
        if (poolCanExpand)
        {
            return NewObject();
        }
        return null;
    }

    private GameObject NewObject()
    {
        GameObject i = Instantiate(item);
        item.SetActive(false);
        items.Add(i);
        i.transform.parent = parent.transform;
        return i;
    }
}
