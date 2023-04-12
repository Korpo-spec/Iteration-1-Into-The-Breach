using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EntityVisualizer : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private UnitHealthBar healthBar;
    public GameObject unitRef;
    
    public EntityVisualizer Spawn(Entity entity)
    {
        Debug.Log(entity);
        Debug.Log(entity.prefab);
        var g = Instantiate(this, new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
        g.unitRef = Instantiate(entity.prefab, g.transform);
        return g;
    }

    public void UpdateHealth(Entity entity, int currentHealth, int maxHealth)
    {
        healthBar.UpdateHealth(entity, currentHealth,maxHealth);
    }

    
}
