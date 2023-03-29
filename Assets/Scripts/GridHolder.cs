using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridHolder : MonoBehaviour
{
    // Start is called before the first frame 
    private Grid<int> grid;
    void Start()
    {
        grid = new Grid<int>(5,4,1,Vector3.zero, Vector3.up);
        Debug.DrawLine(Vector3.zero, Vector3.right, Color.white, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition+ new Vector3(0,0,5)));
            grid.CameraRaycast(Camera.main, out Vector2 pos);
            //grid.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition+ new Vector3(0,0,10)), 10);
            
        }
    }
}
