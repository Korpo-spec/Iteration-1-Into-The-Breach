using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Faction GetNextFaction(Faction currentFaction)
    {
        Faction nextFaction;

        nextFaction = currentFaction == Faction.Player ? Faction.Enemy : Faction.Player;
        
        return nextFaction;
    }

    public List<UnitMoveData> GetMoves()
    {
        List<UnitMoveData> validMoves = new List<UnitMoveData>();
        List<Entity> validEntities = _entities.Where(e => e.entityFaction == _stateFaction).ToList();

        foreach (var entity in validEntities)
        {
            foreach (var unitMove in entity.movements)
            {
                foreach (var move in unitMove.movements)
                {
                    bool availableMove = false;
                    var otherEntity = _enemyGrid.GetValue(entity.gridPos + move);
                    if (otherEntity)
                    {
                        availableMove = unitMove.AvailableMove(otherEntity, entity, _enemyGrid);
                    }
                    else
                    {
                        availableMove = unitMove.AvailableMove(entity, entity.gridPos + move, _enemyGrid);
                    }

                    if (!availableMove) continue;
                    
                    if (!entity.availableMoves.TryAdd(entity.gridPos +move, unitMove))
                    {
                        if (entity.availableMoves[entity.gridPos +move].priority < unitMove.priority)
                        {
                            entity.availableMoves[entity.gridPos + move] = unitMove;
                        }
                    }
                }
            }

            foreach (var movePos in entity.availableMoves.Keys)
            {
                validMoves.Add(new UnitMoveData(entity, movePos, entity.availableMoves[movePos]));
            }
        }

        return null;
    }

}
