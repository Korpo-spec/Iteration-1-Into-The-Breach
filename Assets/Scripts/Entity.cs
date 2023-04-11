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

    public int energy;



    public List<UnitMove> movements;

    public Dictionary<Vector2, UnitMove> availableMoves;

    

    public virtual void OnSpawn()
    {
        //movements = new List<UnitMove>();
        visualizer = visualizer.GetComponent<EntityVisualizer>().Spawn(this);

        availableMoves = new Dictionary<Vector2, UnitMove>();

    }
    
    

    
}


