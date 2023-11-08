using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private int rayLength = 4;
    [SerializeField] private LayerMask layerMaskDoor;
    [SerializeField] private string excludeLayerName = null;
    private MyDoorCont raycastedDoor;
    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    private const string interactableTag = "Interactable";
    bool isDone = false;

    private void Update()
    {
        RaycastHit hit; 
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskDoor.value;

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!isDone)
                {
                    raycastedDoor = hit.collider.gameObject.GetComponent<MyDoorCont>();
                }
                isDone = true;
                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastedDoor.PlayAnimation();
                }

            }
        }
        else
        {
            isDone = false;
        }
    }

    





}
