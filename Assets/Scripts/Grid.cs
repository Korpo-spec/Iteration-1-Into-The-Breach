using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Grid<T>
{
    private T[,] grid;
    private TextMesh[,] _textMeshes;
    private Plane _plane;

    private int width;
    private int height;
    private float cellSize;
    private Vector3 origin;
    private Vector3 _normal;

    public event Action<T> OnGridValueChanged;
    public Grid(int width, int height, float cellSize, Vector3 origin, Vector3 normal)
    {
        grid = new T[width, height];
        _textMeshes = new TextMesh[width, height];
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        this._normal = normal;
        _normal.Normalize();
        _plane = new Plane(Vector3.forward, normal);
        VisualizeGrid();
    }

    private void VisualizeGrid()
    {
        for (int i = 0; i < grid.GetLength(1); i++)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                /*_textMeshes[j,i] = UtilsClass.CreateWorldText(grid[j, i].ToString(), null, new Vector3(cellSize * (j +0.5f), cellSize * (i+0.5f), 0), 7,
                    Color.white, TextAnchor.MiddleCenter);
                    */
                Debug.DrawLine(origin+new Vector3(cellSize * j, cellSize * i, 0),origin+new Vector3(cellSize*(j+1), cellSize*(i), 0), Color.white, 100f);
                Debug.DrawLine(origin+new Vector3(cellSize * j, cellSize * i, 0),origin+new Vector3(cellSize*(j), cellSize*(i+1), 0), Color.white, 100f);
                if (j == grid.GetLength(0)-1)
                {
                    Debug.DrawLine(origin+new Vector3(cellSize * (j+1), cellSize * i, 0),origin+new Vector3(cellSize*(j+1), cellSize*(i+1), 0), Color.white, 100f);
                }
                
                if (i == grid.GetLength(1)-1)
                {
                    Debug.DrawLine(origin+new Vector3(cellSize * j, cellSize * (i+1), 0),origin+new Vector3(cellSize*(j+1), cellSize*(i+1), 0), Color.white, 100f);
                }
            }
        }
    }

    private void UpdateVisualizer(int x, int y)
    {
        //_textMeshes[x, y].text = grid[x, y].ToString();
    }

    public T GetValue(int x, int y)
    {
        if (x < 0 || x > width || y < 0 || y > height)
        {
            return default(T);
        }

        return grid[x, y];
    }

    public void SetValue(int x, int y, T value)
    {
        if (x < 0 || x > width || y < 0 || y > height)
        {
            return;
        }
        grid[x, y] = value;
        UpdateVisualizer(x,y);
        OnGridValueChanged?.Invoke(value);
    }

    public void SetValue(Vector2 vector2, T value)
    {
        SetValue(Mathf.FloorToInt(vector2.x),Mathf.FloorToInt(vector2.y), value);
    }

    public bool CameraRaycast(Camera camera, out Vector2 vec)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        float enter = 0;
        if (_plane.Raycast(ray, out enter))
        {
            Debug.Log(ray.GetPoint(enter));
            vec = ray.GetPoint(enter);
            return true;
        }
        vec = Vector2.zero;
        return false;
    }
    
}
