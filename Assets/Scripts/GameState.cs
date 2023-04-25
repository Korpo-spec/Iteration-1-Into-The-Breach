using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState 
{
    public Grid<Entity> _enemyGrid;
    public List<Entity> _entities;
    public Faction _stateFaction;
    public bool gameOver;

    public GameState(Vector2 size, List<Entity> entities, Faction startFaction)
    {
        _enemyGrid = new Grid<Entity>((int)size.x, (int)size.y, 1, Vector3.zero,
            Vector3.up, null);

        _entities = new List<Entity>();

        _stateFaction = startFaction;
        for (int i = 0; i < entities.Count; i++)
        {
            Entity copy = Object.Instantiate(entities[i]);
            
            copy.onDestroyEntity += OnEntityDestoyed;
            copy.availableMoves = new Dictionary<Vector2, UnitMove>();
            _entities.Add(copy);
            _enemyGrid.SetValue(copy.gridPos, copy);
            
        }
    }

    private void OnEntityDestoyed(Entity entity)
    {
        _enemyGrid.SetValue(entity.gridPos, null);
        _entities.Remove(entity);
        gameOver = !(_entities.Count(e => e.entityFaction == GetNextFaction(_stateFaction)) > 0);
        entity.onDestroyEntity -= OnEntityDestoyed;
    }

    public GameState(GameState gameState)
    {
        _enemyGrid = new Grid<Entity>((int)gameState._enemyGrid.size.x, (int)gameState._enemyGrid.size.y, 1, Vector3.zero,
            Vector3.up, null);

        _entities = new List<Entity>();
        
        _stateFaction = gameState._stateFaction;
        for (int i = 0; i < gameState._entities.Count; i++)
        {
            Entity copy = Object.Instantiate(gameState._entities[i]);
            copy.onDestroyEntity += OnEntityDestoyed;
            copy.availableMoves = new Dictionary<Vector2, UnitMove>();
            Debug.Log(copy.gridPos);
            _entities.Add(copy);
            _enemyGrid.SetValue(copy.gridPos, copy);
            
        }

        
    }
    public Faction GetNextFaction(Faction currentFaction)
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
            GetEntityMove(entity);

            foreach (var movePos in entity.availableMoves.Keys)
            {
                validMoves.Add(new UnitMoveData(entity, movePos, entity.availableMoves[movePos]));
            }
        }

        return validMoves;
    }

    public void GetEntityMove(Entity entity)
    {
        foreach (var unitMove in entity.movements)
        {
            foreach (var move in unitMove.movements)
            {
                Vector2 vec = entity.gridPos + move;
                if (vec.x < 0 || vec.x > _enemyGrid.size.x-1 || vec.y < 0 || vec.y > _enemyGrid.size.y-1)
                {
                    continue;
                }
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
    }

    public GameState GetChildGameState(UnitMoveData move)
    {
        GameState state = new GameState(this);

        Entity entity = state._enemyGrid.GetValue(move.entity.gridPos);

        Entity otherEntity = state._enemyGrid.GetValue(move.pos);

        if (otherEntity)
        {
            move.move.MoveInteract(entity, otherEntity, move.pos, state._enemyGrid);
        }
        else
        {
            move.move.MoveInteract(entity, move.pos, state._enemyGrid);
        }

        state._stateFaction = GetNextFaction(_stateFaction);
        
        
        return state;
    }

    public int Evaluate(Faction maximizingPlayer, Faction MinimizingPlayer)
    {
        if (!(_entities.Count(e => e.entityFaction == GetNextFaction(MinimizingPlayer)) > 0))
        {
            return int.MaxValue;
        }
        else if(!(_entities.Count(e => e.entityFaction == GetNextFaction(maximizingPlayer)) > 0))
        {
            return int.MinValue;
        }
        else
        {
            float scoreMax = _entities.Where(e => e.entityFaction == maximizingPlayer).Sum(i => i.hpFraction);
            float scoreMin = _entities.Where(e => e.entityFaction == MinimizingPlayer).Sum(i => i.hpFraction);

            return (int)(scoreMax * 100 - scoreMin * 100);
        }
    }
    
    

}
