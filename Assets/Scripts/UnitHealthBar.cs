using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour
{
    
    private Quaternion m_InitRot;
    private float m_OriginalXScale;


    private void Start()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
        m_InitRot = transform.rotation;
        m_OriginalXScale = transform.localScale.x;

    }

    public void UpdateHealth(Entity entity, int currentHealth, int maxHealth)
    {
        Debug.Log("ran");
        float percentHp = (float)currentHealth / (float)maxHealth;
        Vector3 vec = transform.localScale;
        vec.x = m_OriginalXScale * percentHp;
        transform.localScale = vec;
    }

    private void LateUpdate()
    {
        transform.rotation = m_InitRot;
    }
}
