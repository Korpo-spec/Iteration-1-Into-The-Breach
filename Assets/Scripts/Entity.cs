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

    public bool hasHealthBar;

    [SerializeField] private int maxHp;
    [SerializeField] private int _currenthp;

    public float hpFraction => (float) _currenthp/maxHp ;
    public int damage;

    public int energy;

    public List<UnitMove> movements;

    public Dictionary<Vector2, UnitMove> availableMoves;

    public event Action<Entity> onDestroyEntity;


    

    public void DecreaseHealth(int amount)
    {
        _currenthp -= amount;
        //Debug.Log(_currenthp);
        if (_currenthp <= 0)
        {
            
            onDestroyEntity?.Invoke(this);
            
        }
        else
        {
            
        }
        
    }

    public virtual void OnSpawn()
    {
        //Debug.Log(this);
        //Debug.Log(prefab);
        //movements = new List<UnitMove>();
        
        visualizer = visualizer.GetComponent<EntityVisualizer>().Spawn(this);

        availableMoves = new Dictionary<Vector2, UnitMove>();

        _currenthp = maxHp;

    }
    
    

    
}


