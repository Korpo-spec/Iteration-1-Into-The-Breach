using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour
{
    
    private Quaternion initRot;


    private void Start()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
        initRot = transform.rotation;
        
    }

    private void LateUpdate()
    {
        transform.rotation = initRot;
    }
}
