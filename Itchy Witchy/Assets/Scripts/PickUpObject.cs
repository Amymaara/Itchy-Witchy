using UnityEngine;

// Title: How to Pick Up and Drop Objects in Unity
// Author: Hayes, A
// Date: 09/08/2025
// Avalability: DIGA2001A Lecture Slides
public class PickUpObject : MonoBehaviour
{
private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform holdPoint)
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero; // Reset position relative to hold point
        transform.localRotation = Quaternion.identity; // Reset rotation relative to hold point
    }

    public void Drop()
    {
        rb.useGravity = true;
        transform.SetParent(null);
        rb.constraints = RigidbodyConstraints.None; // adds rotation back
    }

    public void MoveToHoldPoint(Vector3 targetPosition)
    {
        rb.MovePosition(targetPosition);
    }
}
