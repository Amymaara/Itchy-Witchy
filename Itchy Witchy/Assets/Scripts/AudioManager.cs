using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource footstepSound;
    public AudioClip footstepClip;
    public float footstepInterval = 0.5f;

    [Header("caracter controller settings")]
    public CharacterController controller;

    private float footstepTimer;

    private void Start()
    {
      //  controller = GetComponent<CharacterController>();
      //  footstepSound = gameObject.AddComponent<AudioSource>();

        if (footstepSound == null)
        {
            Debug.LogError("AudioSource component is missing on the AudioManager GameObject.");
        }
    }

    private void Update()
    {
       // HandleFootsteps();
    }
    
    public void HandleFootsteps()
    {
        
        if (controller.isGrounded)
        {
            
            footstepTimer += Time.deltaTime;
            Debug.Log(footstepTimer);
            if (footstepTimer >= footstepInterval)
            {
                
                footstepSound.Play();
            footstepTimer = 0f;
            }
    }
        else
        {
            //footstepTimer = 0f; // Reset timer if not grounded or not moving
        }
    }
    
}
