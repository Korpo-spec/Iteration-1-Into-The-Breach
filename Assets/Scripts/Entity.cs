using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Entity : ScriptableObject
{
    public GameObject visualizer;

    public Mesh mesh;

    public Material mat;

    public virtual void OnSpawn()
    {
        visualizer = visualizer.GetComponent<EntityVisualizer>().Spawn(this);
    }
}
