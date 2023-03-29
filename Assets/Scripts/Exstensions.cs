using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Exstentions 
{
    public static Vector2 RandomVec2(this Vector2 vec)
    {
        vec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        return vec.normalized;
    }
    
    public static Vector3 RandomVec3(this Vector3 vec)
    {
        vec = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        return vec.normalized;
    }

    
}
