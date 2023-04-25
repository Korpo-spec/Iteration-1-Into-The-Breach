using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
   // Start is called before the first frame 
    [SerializeField] private GameObject gridObj;
    [SerializeField] private Entity player;
    [SerializeField] private Entity enemy;
    [SerializeField] private Entity Terrain;

    [SerializeField] private int width;
    [SerializeField] private int height;

    private Entity m_Selected;
    private Grid<GameObject> m_WorldGrid;
    //private Grid<Entity> m_EnemyGrid;
    public static bool IsDoingVisual;
    public GameState currentGameState;

    private Faction m_CurrentFaction;
    
    void Start()
    {
        
        m_CurrentFaction = Faction.Player;
        m_WorldGrid = new Grid<GameObject>(width,height,1,Vector3.zero, Vector3.up, OnSetup);

        //m_EnemyGrid = new Grid<Entity>(width, height, 1, Vector3.zero, Vector3.up, null);
        List<Entity> entities = new List<Entity>();
        var g = Instantiate(player);
        g.gridPos = new Vector2(0, 0);
        g.OnSpawn();
        //m_EnemyGrid.SetValue(0,0, g);
        entities.Add(g);
        
        g = Instantiate(enemy);
        g.gridPos = new Vector2(width -1, height-1);
        g.OnSpawn();
        
        //m_EnemyGrid.SetValue(width-1,height-1, g);
        entities.Add(g);
        g = Instantiate(enemy);
        g.gridPos = new Vector2(2, 1);
        g.OnSpawn();
        
        //m_EnemyGrid.SetValue(2,1, g);
        entities.Add(g);
        g = Instantiate(Terrain);
        g.gridPos = new Vector2(2, 2);
        g.OnSpawn();
        entities.Add(g);
        
        //m_EnemyGrid.SetValue(2,2, g);
        currentGameState = new GameState(new Vector2(width, height), entities, m_CurrentFaction);
        Time.timeScale = 10;
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
        if (IsDoingVisual)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_Selected)
            {
                currentGameState._enemyGrid.CameraRaycast(Camera.main, out Vector2 pos);
                m_Selected = currentGameState._enemyGrid.GetValue(pos);
                if (m_Selected)
                {
                    m_Selected.availableMoves.Clear();
                    if (m_Selected.entityFaction != m_CurrentFaction)
                    {
                        m_Selected = null;
                        return;
                    }
                    m_Selected.gridPos = pos.FloorToInt();
                    currentGameState.GetEntityMove(m_Selected);
                    foreach (var moves in m_Selected.availableMoves.Keys)
                    {
                        var g = m_WorldGrid.GetValue(moves);
                        if (g)
                        {
                            g.GetComponent<MeshRenderer>().material.color = m_Selected.availableMoves[moves].moveColor;

                        }


                    }
                }
                
            }
            else
            {
                if (!currentGameState._enemyGrid.CameraRaycast(Camera.main, out Vector2 pos))
                {
                    return;
                }
                
                bool validPos = false;
                pos = pos.FloorToInt();
                
                validPos = m_Selected.availableMoves.TryGetValue(pos, out UnitMove move);
                
                if (!validPos)
                {
                    return;
                }

                Entity otherEntity = currentGameState._enemyGrid.GetValue(pos);
                
                foreach (var moves in m_Selected.availableMoves.Keys)
                {
                    var g = m_WorldGrid.GetValue(moves);
                    if (g)
                    {
                        g.GetComponent<MeshRenderer>().material.color = Color.gray;
                    }
                }
                m_CurrentFaction = GetNextFaction(m_CurrentFaction);
                
                StartCoroutine(otherEntity ? move.VisualizeMove(m_Selected,otherEntity, pos , currentGameState._enemyGrid):move.VisualizeMove(m_Selected, pos , currentGameState._enemyGrid));
                
                m_Selected = null;
            }
            
        }
    }

    private Faction GetNextFaction(Faction currentFaction)
    {
        Faction nextFaction;

        nextFaction = currentFaction == Faction.Player ? Faction.Enemy : Faction.Player;
        
        return nextFaction;
    }

    private IEnumerator MoveCharacter(Transform character, Vector3 newPos)
    {
        IsDoingVisual = true;
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
        IsDoingVisual = false;
    }
}
