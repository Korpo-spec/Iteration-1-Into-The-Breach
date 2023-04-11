using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EntityVisualizer : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    public GameObject Spawn(Entity entity)
    {
        var g = Instantiate(this, new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
        g._meshFilter.mesh = entity.mesh;
        g._meshRenderer.material = entity.mat;
        return g.gameObject;
    }

    
}
