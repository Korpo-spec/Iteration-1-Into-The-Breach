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
    
    public virtual bool AvailableMove(Entity otherUnit, Entity thisUnit)
    {
        return true;
    }

    public virtual void MoveInteract(Entity otherUnit, Entity thisUnit, Vector2 pos)
    {
        
    }
    public virtual void MoveInteract(Entity entity,Vector2 pos, Grid<Entity> grid)
    {
        
    }

    public virtual IEnumerator VisualizeMove(Entity entity, Vector2 pos, Grid<Entity> grid)
    {
        yield break;
    }
    

}

    
