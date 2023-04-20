using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{
    private Grid<Entity> _enemyGrid;
    public List<Entity> _entities;
    private Faction _stateFaction;

    public GameState(Vector2 size, List<Entity> entities, Faction startFaction)
    {
        _enemyGrid = new Grid<Entity>((int)size.x, (int)size.y, 1, Vector3.zero,
            Vector3.up, null);

        _entities = new List<Entity>();

        for (int i = 0; i < entities.Count; i++)
        {
            Entity copy = ScriptableObject.Instantiate(entities[i]);
            _entities.Add(copy);
            _enemyGrid.SetValue(copy.gridPos, copy);
            
        }
    }

    public GameState(GameState gameState)
    {
        _enemyGrid = new Grid<Entity>((int)gameState._enemyGrid.size.x, (int)gameState._enemyGrid.size.y, 1, Vector3.zero,
            Vector3.up, null);

        _entities = new List<Entity>();

        for (int i = 0; i < gameState._entities.Count; i++)
        {
            Entity copy = ScriptableObject.Instantiate(gameState._entities[i]);
            _entities.Add(copy);
            _enemyGrid.SetValue(copy.gridPos, copy);
            
        }

        
    }
    
}
