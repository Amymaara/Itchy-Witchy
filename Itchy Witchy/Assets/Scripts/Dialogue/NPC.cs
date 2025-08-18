using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public abstract class NPC : MonoBehaviour, IInteractable
{
    //Title: https://www.youtube.com/watch?v=jTPOCglHejE&t=4s&ab_channel=SasquatchBStudios
    //Author: SasquatchB Studios
    //Date Created: 18 Feb 2021
    //Date Accessed: 18 August 2025
    //Code Version: 1
    //Availability: https://www.youtube.com/watch?v=jTPOCglHejE&t=4s&ab_channel=SasquatchBStudios
    // I used this as a baseline but had to adapt it a bit since we used a 3d space for the market to disappear when textbox appears

    [SerializeField] private Canvas _objectCanvas;
    [SerializeField] private HideMarker marker;

    private Transform _playerTransform;

    [SerializeField] private UnityEvent onInteract;

    private const float INTERACT_DISTANCE = 3f;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {

        if (_playerTransform == null) return;

        bool dialogueOpen = marker && marker.IsDialogueOpen;
        bool inRange = Vector3.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE;

        
        if (marker)
        {
            if (dialogueOpen) marker.hideArrowMarker();
            else if (inRange) marker.showArrowMarker();
            else marker.hideArrowMarker();
        }

        if (_objectCanvas)
        {
            bool shouldShowPrompt = inRange && !dialogueOpen;
            if (_objectCanvas.gameObject.activeSelf != shouldShowPrompt)
                _objectCanvas.gameObject.SetActive(shouldShowPrompt);
        }
    }

    public abstract void Interact();

    private bool isWithinInteractDistance()
    {
        return Vector3.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE;
    }

   
    
}
