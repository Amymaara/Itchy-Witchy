using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Canvas _objectCanvas;

    private Transform _playerTransform;

    private const float INTERACT_DISTANCE = 20f;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isWithinInteractDistance())
        {
            //interact with this npc
            Interact();
        }

        if (_objectCanvas.gameObject.activeSelf && !isWithinInteractDistance())
        {
            // turn off sprite 
            _objectCanvas.gameObject.SetActive(false);
        }
        else if (!_objectCanvas.gameObject.activeSelf && isWithinInteractDistance())
        {
            // turn on sprite 
            _objectCanvas.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    private bool isWithinInteractDistance()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
