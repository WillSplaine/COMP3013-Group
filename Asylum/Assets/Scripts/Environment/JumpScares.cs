using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScares : MonoBehaviour
{
  
    // Reference to the object with the animation
    public GameObject animatedObject;
    public AudioSource audioData1;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Play the animation on the animatedObject
            Animator animator = animatedObject.GetComponent<Animator>();
            if (animator != null)
            {
                // Trigger the animation by setting a parameter
                audioData1.Play(0);
                animator.SetTrigger("Scare");
                SanityController.sanityValue -= 100;
            }
            else
            {
                Debug.LogError("Animator component not found on the animatedObject.");
            }
        }
    }
}


