using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashlightPickup : MonoBehaviour
{
    public static bool FlightPickedUp;
    public SphereCollider c_flight;
    public AudioSource audioData;
    public GameObject Flashlight_Model;
    public GameObject Flashlight_Light;

    void Start()
    {
        FlightPickedUp = false;
        Flashlight_Light.SetActive(false);
    }
    void Awake()
    {
        c_flight = GetComponent<SphereCollider>();
        c_flight.isTrigger = true;
    }

     void OnTriggerEnter(Collider c_flight)
    {
        if (c_flight.CompareTag("Player"))
        {   
            audioData.Play(0);
            FlightPickedUp = true;
            Flashlight_Light.SetActive(true);

            Destroy(Flashlight_Model);

        }
    }
}
