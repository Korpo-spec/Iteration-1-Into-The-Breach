using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
   // Start is called before the first frame 
    [SerializeField] private GameObject gridObj;
    [SerializeField] private Entity player;
    [SerializeField] private Entity enemy;

    [SerializeField] private int width;
    [SerializeField] private int height;

    private Entity m_Selected;
    private Grid<GameObject> m_WorldGrid;
    private Grid<Entity> m_EnemyGrid;
    private bool m_IsDoingVisual;

    private Faction m_CurrentFaction;
    
    void Start()
    {
        m_CurrentFaction = Faction.Player;
        m_WorldGrid = new Grid<GameObject>(width,height,1,Vector3.zero, Vector3.up, OnSetup);

        m_EnemyGrid = new Grid<Entity>(width, height, 1, Vector3.zero, Vector3.up, null);
        var g = Instantiate(player);
        g.OnSpawn();
        m_EnemyGrid.SetValue(0,0, g);
        
        g = Instantiate(enemy);
        
        g.OnSpawn();
        /*
        g.movements[0].movements.Add(new Vector2(2,0));
        g.movements[0].movements.Add(new Vector2(-2,0));
        g.movements[0].movements.Add(new Vector2(0,2));
        g.movements[0].movements.Add(new Vector2(0,-2));
        */
        m_EnemyGrid.SetValue(width-1,height-1, g);
    }

    private void OnSetup(Vector2 vec, GameObject[,] gameObjects)
    {
        var g = Instantiate(gridObj, new Vector3(vec.x + 0.5f, -0.5f, vec.y + 0.5f), Quaternion.identity);
        g.transform.parent = transform;
        gameObjects[(int)vec.x, (int)vec.y] = g;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (m_IsDoingVisual)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_Selected)
            {
                m_EnemyGrid.CameraRaycast(Camera.main, out Vector2 pos);
                m_Selected = m_EnemyGrid.GetValue(pos);
                if (m_Selected)
                {
                    if (m_Selected.entityFaction != m_CurrentFaction)
                    {
                        m_Selected = null;
                        return;
                    }
                    m_Selected.gridPos = pos.FloorToInt();
                    foreach (var unitMoves in m_Selected.movements)
                    {
                        foreach (var moves in unitMoves.movements)
                        {
                            var g = m_WorldGrid.GetValue(m_Selected.gridPos + moves);
                            if (g)
                            {
                                bool availableMove = false;
                                var otherEntity = m_EnemyGrid.GetValue(m_Selected.gridPos + moves);
                                if (otherEntity)
                                {
                                    availableMove = unitMoves.AvailableMove(otherEntity, m_Selected);
                                }
                                else
                                {
                                    availableMove = unitMoves.AvailableMove();
                                }

                                if (availableMove)
                                {
                                    if (!m_Selected.availableMoves.TryAdd(m_Selected.gridPos +moves, unitMoves))
                                    {
                                        if (m_Selected.availableMoves[m_Selected.gridPos +moves].priority < unitMoves.priority)
                                        {
                                            m_Selected.availableMoves.Remove(m_Selected.gridPos + moves);
                                            m_Selected.availableMoves.Add(m_Selected.gridPos + moves, unitMoves);
                                        }
                                    }
                                    g.GetComponent<MeshRenderer>().material.color = unitMoves.moveColor;
                                }
                               
                            }
                        }
                    }
                    
                }
                
            }
            else
            {
                if (!m_EnemyGrid.CameraRaycast(Camera.main, out Vector2 pos))
                {
                    return;
                }
                
                bool validPos = false;
                pos = pos.FloorToInt();
                foreach (var moves in m_Selected.movements[0].movements)
                {
                    if (pos == m_Selected.gridPos + moves &&!m_EnemyGrid.GetValue(pos))
                    {
                        validPos = true;
                        break;
                    }
                }
                
                if (!validPos)
                {
                    return;
                }
                
                foreach (var moves in m_Selected.movements[0].movements)
                {
                    var g = m_WorldGrid.GetValue(m_Selected.gridPos + moves);
                    if (g)
                    {
                        g.GetComponent<MeshRenderer>().material.color = Color.gray;
                    }
                }

                Vector3 vec = new Vector3(1f, 0, 1f);
                vec.x *= (int)pos.x;
                vec.z *= (int)pos.y;
                vec += new Vector3(0.5f, 0, 0.5f);
                //selected.visualizer.transform.position = vec;
                m_CurrentFaction = GetNextFaction(m_CurrentFaction);
                StartCoroutine(MoveCharacter(m_Selected.visualizer.transform, vec));
                m_EnemyGrid.SetValue(m_Selected.gridPos, null);
                m_EnemyGrid.SetValue(pos, m_Selected);
                m_Selected.gridPos = pos;
                m_Selected = null;
            }
            
        }
    }

    private Faction GetNextFaction(Faction currentFaction)
    {
        Faction nextFaction;

        if (currentFaction == Faction.Player)
        {
            nextFaction = Faction.Enemy;
        }
        else
        {
            nextFaction = Faction.Player;
        }
        
        return nextFaction;
    }

    private IEnumerator MoveCharacter(Transform character, Vector3 newPos)
    {
        m_IsDoingVisual = true;
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
        m_IsDoingVisual = false;
    }
}
