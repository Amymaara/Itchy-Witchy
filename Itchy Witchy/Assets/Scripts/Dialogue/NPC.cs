using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Canvas _objectCanvas;
    [SerializeField] private HideMarker marker;

    private Transform _playerTransform;

    [SerializeField] private UnityEvent onInteract;

    private const float INTERACT_DISTANCE = 20f;

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

    //UnityEvent IInteractable.onInteract 
    //{ get => onInteract; 
     //set => onInteract = value; }

    private bool isWithinInteractDistance()
    {
        return Vector3.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE;
    }

   
    
}
