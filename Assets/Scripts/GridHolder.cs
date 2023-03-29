using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridHolder : MonoBehaviour
{
    // Start is called before the first frame 
    [SerializeField] private GameObject gridObj;
    [SerializeField] private Entity FLIEGZUG;


    private Entity selected;
    private Grid<GameObject> _worldGrid;
    private Grid<Entity> _EnemyGrid;
    
    void Start()
    {
        _worldGrid = new Grid<GameObject>(10,10,1,Vector3.zero, Vector3.up, OnSetup);

        _EnemyGrid = new Grid<Entity>(10, 10, 1, Vector3.zero, Vector3.up, null);
        var g = Instantiate(FLIEGZUG, new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
        _EnemyGrid.SetValue(0,0, g);
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
                _EnemyGrid.CameraRaycast(Camera.main, out Vector2 pos);
                selected = _EnemyGrid.GetValue(pos); 
            }
            else
            {
                _EnemyGrid.CameraRaycast(Camera.main, out Vector2 pos);
                Vector3 vec = new Vector3(1f, 0, 1f);
                vec.x *= (int)pos.x;
                vec.z *= (int)pos.y;
                vec += new Vector3(0.5f, 0, 0.5f);
                selected.transform.position = vec;
                _EnemyGrid.SetValue(pos, selected);
                selected = null;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        //grid.VisualizeGizmoGrid();
    }
}
