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
    
    public GameObject Spawn(Entity entity)
    {
        var g = Instantiate(this, new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
        g.meshFilter.mesh = entity.mesh;
        g.meshRenderer.material = entity.mat;
        return g.gameObject;
    }

    public void UpdateHealth(Entity entity, int currentHealth, int maxHealth)
    {
        healthBar.UpdateHealth(entity, currentHealth,maxHealth);
    }

    
}
