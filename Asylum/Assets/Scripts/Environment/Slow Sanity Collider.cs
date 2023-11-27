using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSanityCollider : MonoBehaviour
{

    //SphereCollider c_light;
    [SerializeField] Collider other;
    [SerializeField] float localSanityRateMultiplier = 2.0f;
    
    
    void Awake()
    {
        //c_light = GetComponent<SphereCollider>();
        //other.isTrigger = true; 
    }
        

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            SanityController.SanityRateMultiplier = localSanityRateMultiplier;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            SanityController.SanityRateMultiplier = 1.0f;
        }
    }
}
