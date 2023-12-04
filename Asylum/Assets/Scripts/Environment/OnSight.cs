using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSight : MonoBehaviour
{
    public GameObject Jumpscare;
    
    // Update is called once per frame
    void Update()
    {
        if (!Jumpscare.GetComponent<Renderer>().isVisible)
        {
            print("you're gay now");
        }
    }
}
