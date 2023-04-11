
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MoveUnitMove : UnitMove
{
    public override List<Vector2> movements { get; set; }

    private void OnEnable()
    {
        movements.Add(new Vector2(1,0));
        movements.Add(new Vector2(-1,0));
        movements.Add(new Vector2(0,1));
        movements.Add(new Vector2(0,-1));
    }

    public override bool AvailableMove()
    {
        return true;
    }
}
