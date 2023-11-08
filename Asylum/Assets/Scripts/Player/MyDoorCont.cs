using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorCont : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if(!doorOpen)
        {
            doorAnim.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;

        }
        
        
    }

}
