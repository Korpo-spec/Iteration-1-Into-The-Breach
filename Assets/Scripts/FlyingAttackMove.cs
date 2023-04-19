using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FlyingAttackMove : UnitMove
{
    public override List<Vector2> movements { get; set; }

    private void OnEnable()
    {
        movements.Add(new Vector2(2,0));
        movements.Add(new Vector2(-2,0));
        movements.Add(new Vector2(0,2));
        movements.Add(new Vector2(0,-2));
    }
    
    public override bool AvailableMove(Entity otherUnit, Entity thisUnit, Grid<Entity> grid)
    {
        
        return false;
    }

    public override bool AvailableMove(Entity entity,Vector2 pos, Grid<Entity> grid)
    {
        Vector2 dif = entity.gridPos - pos;
        dif.Normalize();

        Entity otherUnit = grid.GetValue(entity.gridPos - dif);
        if (otherUnit && otherUnit.entityFaction != entity.entityFaction)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public override void MoveInteract(Entity thisUnit, Entity otherUnit, Vector2 pos, Grid<Entity> grid)
    {
        
    }
    
    public override void MoveInteract(Entity entity,Vector2 pos, Grid<Entity> grid)
    {
        grid.SetValue(entity.gridPos, null);
        Vector2 dif = entity.gridPos - pos;
        dif.Normalize();
        grid.SetValue(pos, entity);
        Entity otherUnit = grid.GetValue(pos + dif);
        Debug.Log(otherUnit + "    " + dif + "   " + pos+dif);
        if (!otherUnit)
        {
            return;
        }
       
        if (otherUnit.entityFaction != entity.entityFaction && otherUnit.entityFaction != Faction.Terrain)
        {
            otherUnit.DecreaseHealth(entity.damage);
            
        }
        
    }

    public override IEnumerator VisualizeMove(Entity entity, Vector2 pos, Grid<Entity> grid)
    {
        MoveInteract(entity, pos, grid);
        
        Vector3 vec = new Vector3(1f, 0, 1f);
        vec.x *= (int)pos.x;
        vec.z *= (int)pos.y;
        vec += new Vector3(0.5f, 0, 0.5f);

        return MoveCharacter(entity.visualizer.transform, vec);
    }

    private IEnumerator MoveCharacter(Transform character, Vector3 newPos)
    {
        TurnManager.IsDoingVisual = true;
        Vector3 newPosVec3 = newPos;
        newPosVec3.y = character.position.y;
        
        float time = 0;

        Quaternion startRot = character.rotation;
        while (time < 1)
        {
            character.rotation = Quaternion.Lerp(startRot, Quaternion.LookRotation(newPosVec3- character.position ), time);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime * 5;
        }

        time = 0;
        Vector3 startPos = character.position;
        while (time < 1)
        {
            character.position = Vector3.Lerp(startPos, newPosVec3, time);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        TurnManager.IsDoingVisual = false;
    }
}
