using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting.ReorderableList;

public class WalkingAudio : MonoBehaviour
{
    /*public AudioSource walkingSound;

    // Title: How to Add Footsteps Sounds in Unity
    // Author: Omogonix
    // Date: 14/08/2025
    // Avalability: https://youtu.be/uNYF1gsvD1A?si=aMqA-JzoVf382j9n 
    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walkingSound.enabled = true;
        }
        else
        {
            walkingSound.enabled = false;
        }
    }
    */

    public AudioClip audioClip;
    public float minSpeed = 0.1f;

    CharacterController cc;
    AudioSource src;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        src = GetComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = true;
    }

    private void Update()
    {
       Vector3 v = cc.velocity;
       bool moving = v.magnitude > minSpeed;

       if (moving)
        {
            src.Play();

        }

        else
        {
            src.Stop();
        }
    }
}