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
        healthBar.gameObject.SetActive(entity.hasHealthBar);
        Vector3 pos = transform.position;
        pos.x = entity.gridPos.x + 0.5f;
        pos.z = entity.gridPos.y + 0.5f;
        var g = Instantiate(this,  pos, Quaternion.identity);
        g.unitRef = Instantiate(entity.prefab, g.transform);
        return g;
    }

    public void UpdateHealth(Entity entity, int currentHealth, int maxHealth)
    {
        healthBar.UpdateHealth(entity, currentHealth,maxHealth);
    }
    
    public void UpdateHealth(Entity entity, float frac)
    {
        healthBar.UpdateHealth(entity, frac);
    }

    
}
