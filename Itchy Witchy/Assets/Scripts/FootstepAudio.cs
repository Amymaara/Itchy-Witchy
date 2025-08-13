using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    [Header("Footstep Audio Settings")]
    public AudioSource footstepSound;
    public AudioClip footstepClip; // The sound to play for footsteps
    public float stepInterval = 0.5f; // Time between footsteps

    [Header("Character Controller Settings")]
    public AudioSource footstepAudio;
    public CharacterController controller; // Reference to the CharacterController component
    private float stepTimer; // Timer to track time since last step

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        footstepSound = GetComponent<AudioSource>();

        if (footstepSound == null)
        {
            Debug.LogError("FootstepAudio: No AudioSource component found on the GameObject.");
        }
    }

    private void Update()
    {
        Handlefootstep();
    }
    void Handlefootstep()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= 0f)
            {
                footstepSound.PlayOneShot(footstepClip);
                stepTimer = stepInterval;
            }
            else
            {
                stepTimer = 0f; // Reset timer if not grounded or moving
            }
        }
    }
}
