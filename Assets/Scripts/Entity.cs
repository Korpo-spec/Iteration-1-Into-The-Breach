using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Entity : ScriptableObject
{
    public GameObject visualizer;

    public Mesh mesh;

    public Material mat;

    public Faction entityFaction;

    public Vector2 gridPos;

    public int hp;

    public int damage;
    
    
    
    public virtual List<Vector2> Movements { get; set; }

    public virtual void OnSpawn()
    {
        Movements = new List<Vector2>();
        visualizer = visualizer.GetComponent<EntityVisualizer>().Spawn(this);
        Movements.Add(new Vector2(1,0));
        Movements.Add(new Vector2(-1,0));
        Movements.Add(new Vector2(0,1));
        Movements.Add(new Vector2(0,-1));
    }
}

public enum Faction
{
    Player,
    Enemy
}
