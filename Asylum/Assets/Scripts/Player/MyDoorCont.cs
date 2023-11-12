using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorCont : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;
    private AudioSource doorSound;

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
        doorSound = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAnimation()
    {
        
        if (!doorOpen)
        {
            doorSound.Play();
            doorAnim.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;

            
            

        }
        
        
    }

}
