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
    private Quaternion _rotationQuat;
    private Action<Vector2, T[,]> _onSetup;

    public event Action<T> OnGridValueChanged;
    public Grid(int width, int height, float cellSize, Vector3 origin, Vector3 normal, Action<Vector2, T[,]> onSetup)
    {
        grid = new T[width, height];
        _textMeshes = new TextMesh[width, height];
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        this._normal = normal;
        _normal.Normalize();
        _onSetup = onSetup;
        
        _plane = new Plane(normal, origin);
        float angle = Vector3.Angle(normal, Vector3.forward);
        Vector3 rotationAxis = Vector3.Cross(normal, Vector3.forward);
        _rotationQuat = Quaternion.AngleAxis(angle, rotationAxis);
        Matrix4x4 mat = Matrix4x4.Rotate(_rotationQuat);
        
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
                _onSetup(new Vector2(j,i), grid);
                Debug.DrawLine(_rotationQuat * (origin+new Vector3(cellSize * j, cellSize * i, 0)) ,_rotationQuat *(origin+new Vector3(cellSize*(j+1), cellSize*(i), 0)), Color.white, 100f);
                Debug.DrawLine(_rotationQuat * (origin+new Vector3(cellSize * j, cellSize * i, 0)),_rotationQuat *(origin+new Vector3(cellSize*(j), cellSize*(i+1), 0)), Color.white, 100f);
                if (j == grid.GetLength(0)-1)
                {
                    Debug.DrawLine(_rotationQuat *(origin+new Vector3(cellSize * (j+1), cellSize * i, 0)),_rotationQuat *(origin+new Vector3(cellSize*(j+1), cellSize*(i+1), 0)), Color.white, 100f);
                }
                
                if (i == grid.GetLength(1)-1)
                {
                    Debug.DrawLine(_rotationQuat *(origin+new Vector3(cellSize * j, cellSize * (i+1), 0)),_rotationQuat *(origin+new Vector3(cellSize*(j+1), cellSize*(i+1), 0)), Color.white, 100f);
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

    public T GetValue(Vector2 vector2)
    {
        return GetValue(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
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
            Vector3 rayHit = ray.GetPoint(enter);
            vec.x = rayHit.x;
            vec.y = rayHit.z;
            
            /*
            Vector3 cameraX = Vector3.zero;
            cameraX = camera.transform.right;
            Vector3 normalX = Vector3.zero;
            normalX = _normal;
            
            Vector3 cameraY = Vector3.zero;
            cameraY = camera.transform.up;
            Vector3 normalY = Vector3.zero;
            normalY = _normal;
            
            Vector3 scaleVec = Vector3.zero;
            vec.x *= Vector3.Angle(cameraX, normalX) / 90 + 1;
            vec.y *= Vector3.Angle(cameraY, normalY) / 90 + 1;
            Debug.Log(Vector3.Angle(cameraX, normalX) + " " + cameraX + " " + normalX);
            Debug.Log(vec);
            */
            return true;
        }
        vec = Vector2.zero;
        return false;
    }
    
}
