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
    private int _currenthp;

    public int damage;

    public int energy;
    
    



    public List<UnitMove> movements;

    public Dictionary<Vector2, UnitMove> availableMoves;

    public event Action<Entity> onDestroyEntity;


    private Grid<Entity> entityGrid;

    public void DecreaseHealth(int amount)
    {
        _currenthp -= amount;
        if (_currenthp <= 0)
        {
            entityGrid.SetValue(gridPos, null);
            onDestroyEntity?.Invoke(this);
            Destroy(visualizer.gameObject);
        }
        else
        {
            visualizer.UpdateHealth(this, _currenthp, maxHp);
        }
        
    }

    public virtual void OnSpawn(Grid<Entity> grid)
    {
        Debug.Log(this);
        Debug.Log(prefab);
        //movements = new List<UnitMove>();
        entityGrid = grid;
        visualizer = visualizer.GetComponent<EntityVisualizer>().Spawn(this);

        availableMoves = new Dictionary<Vector2, UnitMove>();

        _currenthp = maxHp;

    }
    
    

    
}


