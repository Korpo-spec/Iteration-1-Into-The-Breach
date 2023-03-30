using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridHolder : MonoBehaviour
{
    // Start is called before the first frame 
    [SerializeField] private GameObject gridObj;
    [SerializeField] private Entity FLIEGZUG;
    [SerializeField] private Entity FLYINGPLANETHINGYFROMDOOMANDBEOYND;

    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private Entity selected;
    private Grid<GameObject> _worldGrid;
    private Grid<Entity> _enemyGrid;
    
    void Start()
    {
        _worldGrid = new Grid<GameObject>(_width,_height,1,Vector3.zero, Vector3.up, OnSetup);

        _enemyGrid = new Grid<Entity>(_width, _height, 1, Vector3.zero, Vector3.up, null);
        var g = Instantiate(FLIEGZUG);
        g.OnSpawn();
        _enemyGrid.SetValue(0,0, g);
        
        g = Instantiate(FLYINGPLANETHINGYFROMDOOMANDBEOYND);
        
        g.OnSpawn();
        g.Movements.Add(new Vector2(2,0));
        g.Movements.Add(new Vector2(-2,0));
        g.Movements.Add(new Vector2(0,2));
        g.Movements.Add(new Vector2(0,-2));
        _enemyGrid.SetValue(_width-1,_height-1, g);
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
        if (Input.GetMouseButtonDown(0))
        {
            if (!selected)
            {
                _enemyGrid.CameraRaycast(Camera.main, out Vector2 pos);
                selected = _enemyGrid.GetValue(pos);
                if (selected)
                {
                    
                    selected.gridPos = pos.FloorToInt();
                    foreach (var moves in selected.Movements)
                    {
                        var g = _worldGrid.GetValue(selected.gridPos + moves);
                        if (g &&!_enemyGrid.GetValue(selected.gridPos + moves))
                        {
                            g.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }
                }
                
            }
            else
            {
                if (!_enemyGrid.CameraRaycast(Camera.main, out Vector2 pos))
                {
                    return;
                }

                
                bool validPos = false;
                pos = pos.FloorToInt();
                foreach (var moves in selected.Movements)
                {
                    if (pos == selected.gridPos + moves &&!_enemyGrid.GetValue(pos))
                    {
                        validPos = true;
                        break;
                    }
                }


                if (!validPos)
                {
                    return;
                }
                
                foreach (var moves in selected.Movements)
                {
                    var g = _worldGrid.GetValue(selected.gridPos + moves);
                    if (g)
                    {
                        g.GetComponent<MeshRenderer>().material.color = Color.gray;
                    }
                }

                Vector3 vec = new Vector3(1f, 0, 1f);
                vec.x *= (int)pos.x;
                vec.z *= (int)pos.y;
                vec += new Vector3(0.5f, 0, 0.5f);
                selected.visualizer.transform.position = vec;
                _enemyGrid.SetValue(selected.gridPos, null);
                _enemyGrid.SetValue(pos, selected);
                selected.gridPos = pos;
                selected = null;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
