using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Entity : ScriptableObject
{
    public EntityVisualizer visualizer;

    public GameObject prefab;

    public Faction entityFaction;

    public Vector2 gridPos;

    [SerializeField] private int maxHp;
    private int _currenthp;

    public int damage;

    public int energy;



    public List<UnitMove> movements;

    public Dictionary<Vector2, UnitMove> availableMoves;

    public void DecreaseHealth(int amount)
    {
        _currenthp -= amount;
        visualizer.GetComponent<EntityVisualizer>().UpdateHealth(this, _currenthp, maxHp);
    }

    public virtual void OnSpawn()
    {
        Debug.Log(this);
        Debug.Log(prefab);
        //movements = new List<UnitMove>();
        visualizer = visualizer.GetComponent<EntityVisualizer>().Spawn(this);

        availableMoves = new Dictionary<Vector2, UnitMove>();

        _currenthp = maxHp;

    }
    
    

    
}


