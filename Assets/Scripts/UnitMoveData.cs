using UnityEngine;


public struct UnitMoveData
{
    public Entity entity;
    public Vector2 pos;
    public UnitMove move;

    public UnitMoveData(Entity entity, Vector2 position, UnitMove movee)
    {
        this.entity = entity;
        pos = position;
        move = movee;
    }
    
    /*
    public static bool operator == (UnitMoveData data, UnitMoveData data2)
    {
        
    }
    
    public static bool operator != (UnitMoveData data, UnitMoveData data2)
    {
        return false;
    }
    */
}
