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

    public override bool AvailableMove(Entity otherUnit, Entity thisUnit, Grid<Entity> grid)
    {
        if (otherUnit.entityFaction != thisUnit.entityFaction &&  otherUnit.entityFaction != Faction.Terrain)
        {
            return true;
        }

        return false;
    }

    public override bool AvailableMove()
    {
        return false;
    }

    public override void MoveInteract(Entity thisUnit, Entity otherUnit, Vector2 pos, Grid<Entity> grid)
    {
        if (otherUnit.entityFaction != thisUnit.entityFaction &&  otherUnit.entityFaction != Faction.Terrain)
        {
            Vector2 dif = thisUnit.gridPos - otherUnit.gridPos;
            dif.Normalize();

            Entity entityBehindOtherUnit = grid.GetValue(otherUnit.gridPos - dif);

            if (entityBehindOtherUnit && entityBehindOtherUnit.entityFaction == Faction.Terrain)
            {
                otherUnit.DecreaseHealth(thisUnit.damage * 2);
            }
            else
            {
                otherUnit.DecreaseHealth(thisUnit.damage);
            }
            
            
            
        }
    }

    public override IEnumerator VisualizeMove(Entity entity,Entity otherEntity, Vector2 pos, Grid<Entity> grid)
    {
        MoveInteract(entity, otherEntity, pos, grid);

        return PlayAnim(entity, pos);
    }

    private IEnumerator PlayAnim(Entity entity, Vector2 newPos)
    {
        
        TurnManager.IsDoingVisual = true;
        Vector3 newPosVec3 = newPos;
        newPosVec3.y = entity.visualizer.unitRef.transform.position.y;
        
        float time = 0;

        Quaternion startRot = entity.visualizer.unitRef.transform.rotation;
        while (time < 1)
        {
            entity.visualizer.unitRef.transform.rotation = Quaternion.Lerp(startRot, Quaternion.LookRotation(newPosVec3-  entity.visualizer.unitRef.transform.position ), time);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime * 5;
        }
        
        
        yield return null;
        
        entity.visualizer.unitRef.GetComponent<Animator>().SetTrigger("Punch");
        TurnManager.IsDoingVisual = false;
    }
}
