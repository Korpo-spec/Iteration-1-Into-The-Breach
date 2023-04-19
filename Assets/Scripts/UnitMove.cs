using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UnitMove : ScriptableObject
{

    public abstract List<Vector2> movements { get; set; }

    public int priority;
    public Color moveColor;

    public virtual bool AvailableMove()
    {
        return true;
    }
    
    public virtual bool AvailableMove(Entity otherUnit, Entity thisUnit, Grid<Entity> grid)
    {
        return false;
    }

    public virtual void MoveInteract(Entity thisUnit, Entity otherUnit, Vector2 pos, Grid<Entity> grid)
    {
        
    }
    public virtual void MoveInteract(Entity entity,Vector2 pos, Grid<Entity> grid)
    {
        
    }

    public virtual IEnumerator VisualizeMove(Entity entity, Vector2 pos, Grid<Entity> grid)
    {
        yield break;
    }
    
    public virtual IEnumerator VisualizeMove(Entity entity,Entity otherEntity, Vector2 pos, Grid<Entity> grid)
    {
        yield break;
    }

}

    
