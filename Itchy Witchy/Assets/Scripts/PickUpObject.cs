using UnityEngine;

public class PickUpObject : MonoBehaviour
{
private Rigidbody rb;
private Collider col;

private Transform holdPoint;
private bool isHeld = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isHeld && holdPoint != null)
        {
            MoveToHoldPoint(holdPoint.position);
        }
    }

    public void PickUp(Transform holdPoint)
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero; // Reset position relative to hold point
    }

    public void Drop()
    {
        rb.useGravity = true;
        transform.SetParent(null);
    }

    public void MoveToHoldPoint (Vector3 targetPosition)
    {
        rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f));
    }
}
