using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitMove : ScriptableObject
{

    public virtual List<Vector2> movements { get; set; }

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

    public virtual void MoveInteract(Entity otherUnit, Entity thisUnit)
    {
        
    }

}

    
