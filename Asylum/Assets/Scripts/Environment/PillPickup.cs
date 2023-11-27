using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PillPickup : MonoBehaviour
{
    public static int PillsCollected;
    public SphereCollider c_pill;
    public AudioSource audioData;
    public AudioSource audioData1;
    public GameObject PillObj;

    void Awake()
    {
        c_pill = GetComponent<SphereCollider>();
        c_pill.isTrigger = true;
    }


     void OnTriggerEnter(Collider c_pill)
    {
        //UnityEngine.Debug.Log("thing awake");
        if (c_pill.CompareTag("Player"))
        {   

            //audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            audioData1.Play(0);
            PillsCollected++;
            SanityController.sanityValue += 50; 
            
            //debug
            UnityEngine.Debug.Log("You currently have taken " + PillPickup.PillsCollected + " pills.");


            //Destroy Object
            Destroy(gameObject);

        }
    }
}
