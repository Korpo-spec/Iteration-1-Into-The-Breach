using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AttackMove : UnitMove
{
    public override List<Vector2> movements { get; set; }

    private void OnEnable()
    {
        movements.Add(new Vector2(1,0));
        movements.Add(new Vector2(-1,0));
        movements.Add(new Vector2(0,1));
        movements.Add(new Vector2(0,-1));
    }

    public override bool AvailableMove(Entity otherUnit, Entity thisUnit)
    {
        if (otherUnit.entityFaction != thisUnit.entityFaction)
        {
            return true;
        }

        return false;
    }

    public override bool AvailableMove()
    {
        return false;
    }

    public override void MoveInteract(Entity thisUnit, Entity otherUnit, Vector2 pos)
    {
        if (otherUnit.entityFaction != thisUnit.entityFaction)
        {
            otherUnit.DecreaseHealth(thisUnit.damage);
            
        }
    }

    public override IEnumerator VisualizeMove(Entity entity,Entity otherEntity, Vector2 pos, Grid<Entity> grid)
    {
        MoveInteract(entity, otherEntity, pos);

        return PlayAnim();
    }

    private IEnumerator PlayAnim()
    {
        yield return null;
    }
}