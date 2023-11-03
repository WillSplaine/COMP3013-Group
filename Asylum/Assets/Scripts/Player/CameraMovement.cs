using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform cameraPos;

    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
