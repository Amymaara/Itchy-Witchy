using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class RuneDraw : MonoBehaviour
{
    // pix and Dev tut

    [Header("References")]
    public Transform cursor;
    public Camera mainCam;
    public LineRenderer targetPath;
    public TMP_Text accuracyText;

    [Header("Settings")]
    public float controllerSpeed = 5f;
    public float pointSpacing = 0.05f;
    public float accuracyThreshold = 0.2f;

    private LineRenderer playerLine;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing;
    private Vector2 moveInput;

    private void Awake()
    {
        playerLine = GetComponent<LineRenderer>();
        if (mainCam == null)
            mainCam = Camera.main;
    }

    // Called from Input Actions: Mouse/stick position
    public void OnPoint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2 screenPos = ctx.ReadValue<Vector2>();
            //Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            cursor.position = new Vector3(screenPos.x, 0.83f, screenPos.y);
        }
    }

    // Called from Input Actions: Controller stick
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    // Called from Input Actions: Draw button
    public void OnDraw(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            StartDrawing();
        }
        else if (ctx.canceled)
        {
            StopDrawing();
        }
    }

    private void Update()
    {
        // Controller cursor movement
        if (Gamepad.current != null && moveInput != Vector2.zero)
        {
            cursor.position += (Vector3)(moveInput * controllerSpeed * Time.deltaTime);
        }

        // If actively drawing, add points
        if (isDrawing)
        {
            float dist = points.Count > 0 ? Vector3.Distance(cursor.position, points[^1]) : Mathf.Infinity;
            if (dist >= pointSpacing)
            {
                AddPoint(cursor.position);
            }
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;
        points.Clear();
        playerLine.positionCount = 0;
        AddPoint(cursor.position);
    }

    private void StopDrawing()
    {
        isDrawing = false;
        float accuracy = CalculateAccuracy();
        accuracyText.text = $"Accuracy: {accuracy:0.0}%";
    }

    private void AddPoint(Vector3 point)
    {
        points.Add(point);
        playerLine.positionCount = points.Count;
        playerLine.SetPositions(points.ToArray());
    }

    private float CalculateAccuracy()
    {
        if (points.Count == 0) return 0f;

        Vector3[] targetPoints = new Vector3[targetPath.positionCount];
        targetPath.GetPositions(targetPoints);

        float goodPoints = 0;
        foreach (var p in points)
        {
            float minDist = Mathf.Infinity;
            foreach (var t in targetPoints)
            {
                float dist = Vector2.Distance(p, t);
                if (dist < minDist) minDist = dist;
            }
            if (minDist <= accuracyThreshold)
                goodPoints++;
        }

        return (goodPoints / points.Count) * 100f;
    }
}

