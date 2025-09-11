using System.Collections;
using System.Collections.Generic;
using  UnityEngine.InputSystem;
using UnityEngine;

// Title: How to Pick Up and Drop Objects in Unity
// Author: Hayes, A
// Date: 09/08/2025
// Avalability: DIGA2001A Lecture Slides
public class PickUpObject : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float pickUpRange = 10f;

    private GameObject heldObject;
    private Rigidbody heldObjectRb;
    private bool canDrop = true;
    private int LayerNumber;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
        if (heldObject != null)
        {
            MoveObject();
            StopClipping();
        }
    }

    void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (heldObject == null) // if no object is held
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                if (hit.transform.CompareTag("canPickUp"))
                {
                    DoPickUp(hit.transform.gameObject);
                }
            }
        }
    }
    public void PickUp(Transform holdPoint)
    {
        DoPickUp(gameObject); // reuse your existing method
    }

    public void Drop()
    {
        DropObject();
    }

    private void DoPickUp(GameObject pickUpObject)
        {
            if (pickUpObject.TryGetComponent(out Rigidbody rb))
            {
                heldObject = pickUpObject;
                heldObjectRb = rb;
                heldObjectRb.isKinematic = true; // make the object kinematic
                heldObject.transform.parent = holdPos.transform;
                heldObject.layer = LayerNumber; // set the layer to holdLayer

                Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true); // ignore collision with player
            }

        }
        private void DropObject()
        {
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false); // stop ignoring collision with player
            heldObject.layer = 0;
            heldObjectRb.isKinematic = false; // make the object non-kinematic
            heldObject.transform.parent = null; // remove parent
            heldObject = null; // clear the held object
        }

        void MoveObject()
        {
            heldObject.transform.position = holdPos.transform.position; // move the object to the hold position
        }

        void StopClipping()
        {
            var clipRange = Vector3.Distance(heldObject.transform.position, transform.position);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

            if (hits.Length > 1)
            {
                heldObject.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); // move the object to the hold position
            }
        }

}
