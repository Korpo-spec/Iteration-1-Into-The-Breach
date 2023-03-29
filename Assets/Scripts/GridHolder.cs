using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridHolder : MonoBehaviour
{
    // Start is called before the first frame 
    [SerializeField] private GameObject gridObj;
    private Grid<GameObject> grid;
    void Start()
    {
        grid = new Grid<GameObject>(5,4,1,Vector3.zero, Vector3.up, OnSetup);
        
        
    }

    private void OnSetup(Vector2 vec, GameObject[,] gameObjects)
    {
        gameObjects[(int)vec.x, (int)vec.y]= Instantiate(gridObj, new Vector3(vec.x + 0.5f, -0.5f, vec.y + 0.5f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.CameraRaycast(Camera.main, out Vector2 pos);
            grid.GetValue(pos).GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
