
using System;
using System.Collections;
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

    public override void MoveInteract(Entity entity, Vector2 pos, Grid<Entity> grid)
    {
        grid.SetValue(entity.gridPos, null);
        grid.SetValue(pos, entity);
        entity.gridPos = pos;
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
