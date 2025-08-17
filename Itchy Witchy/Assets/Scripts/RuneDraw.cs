using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;

public class RuneDraw : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject cursor;
    public Camera cameraMain;
    public Transform runeCenter;
    public LineRenderer targetLine;
    public GameObject targetLineGameObject;
    public LineRenderer playerLine;
    public GameObject playerLineGameObject;
    public GameObject stampset;

    [Header("Settings")]
    public float controllerSpeed = 4f;
    public float pointSpacing = 0.05f;
    public float accuracyThreshold = 0.2f;
    public float deadzone = 0.05f;
    public float maxMovePerFrame = 0.5f;
    [SerializeField]
    private float fixedWorldY;
    private Vector3 previousCursorPosition;
    private Vector2 cursorMove;
    private bool isDrawing;
    public float smoothSpeed = 5f; 
    private Vector3 smoothedMove;
    public float runeRadius = 5f;

    private void Start()
    {
        previousCursorPosition = transform.position;
        playerLine.positionCount = 0;
        Cursor.visible = false;

        foreach (LineRenderer stamp in stampset.GetComponentsInChildren<LineRenderer>())
        {
            if (stamp == enabled) 
            {
                targetLine = stamp;
                targetLineGameObject = stamp.gameObject;
            }
        }


    }

    private void Update()
    {


        HandlePoint();



        if (isDrawing)
        {
            DrawingStart();
        }

    }



    public void OnPoint(InputAction.CallbackContext context)
    {
        cursorMove = context.ReadValue<Vector2>();
    }

    // adapted from pix and dev
    public void HandlePoint()
    {
        Vector2 filteredInput = cursorMove;
        if (filteredInput.magnitude < deadzone)
            filteredInput = Vector2.zero;

        Vector3 move = new Vector3(filteredInput.x, 0f, filteredInput.y) * controllerSpeed * Time.deltaTime;

        //Smooth the movement vector
        smoothedMove = Vector3.Lerp(smoothedMove, move, Time.deltaTime * smoothSpeed);
        smoothedMove = Vector3.ClampMagnitude(smoothedMove, maxMovePerFrame);

        cursor.transform.position += smoothedMove;

        // Calculate the direction vector from the center to the object
        Vector3 direction = cursor.transform.position - runeCenter.position;
        float distance = direction.magnitude;

        // Check if the object is outside the circle
        if (distance > runeRadius)
        {
            // Normalize the direction vector
            Vector3 normalizedDirection = direction.normalized;

            // Calculate the new position on the circle's edge
            Vector3 newPosition = runeCenter.position + normalizedDirection * runeRadius;

            // Set the object's position to the new position
            cursor.transform.position = newPosition;

        }



        // Lock Y position
        Vector3 pos = cursor.transform.position;
        pos.y = fixedWorldY;
        cursor.transform.position = pos;
    }


    public void OnDrawRune(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HandlePoint();
            previousCursorPosition = cursor.transform.transform.position;
            isDrawing = true;
            DrawingStart();
        }
        if (context.canceled)
        {
            DrawingStop();
        }
    }

    public void DrawingStart()
    {
        Vector3 currentCursorPosition = cursor.transform.transform.position;
        if (Vector3.Distance(previousCursorPosition, cursor.transform.position) > pointSpacing)
        {
            playerLine.positionCount++;
            playerLine.SetPosition(playerLine.positionCount - 1, currentCursorPosition);
            previousCursorPosition = currentCursorPosition;

        }
    }

    public void DrawingStop()
    {
       
        isDrawing = false;
        float playerAccuracy = CalculateAccuracy(targetLine, playerLine, pointSpacing, accuracyThreshold);
        Debug.Log(playerAccuracy);
        //playerLine.positionCount = 0;
        cursor.SetActive(false);
        targetLineGameObject.SetActive(false);
        playerLineGameObject.SetActive(false); // the object this script is on

    }

    List<Vector3> GetDensifiedPath(LineRenderer line, float spacing)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < line.positionCount - 1; i++)
        {
            Vector3 start = line.GetPosition(i);
            Vector3 end = line.GetPosition(i + 1);
            float dist = Vector3.Distance(start, end);

            int steps = Mathf.CeilToInt(dist / spacing);

            for (int s = 0; s <= steps; s++)
            {
                float t = (float)s / steps;
                points.Add(Vector3.Lerp(start, end, t));
            }
        }
        return points;
    }

    public float CalculateAccuracy(LineRenderer target, LineRenderer player, float spacing, float threshold)
    {
        List<Vector3> denseTarget = GetDensifiedPath(target, spacing);
        int coveredPoints = 0;

        for (int i = 0; i < denseTarget.Count; i++)
        {
            Vector3 targetPoint = denseTarget[i];
            targetPoint.y = fixedWorldY; // ensure same plane

            bool covered = false;
            for (int j = 0; j < player.positionCount; j++)
            {
                Vector3 playerPoint = player.GetPosition(j);
                playerPoint.y = fixedWorldY; // ensure same plane

                if (Vector3.Distance(targetPoint, playerPoint) <= threshold)
                {
                    covered = true;
                    break;
                }
            }

            if (covered) coveredPoints++;
        }

        return (float)coveredPoints / denseTarget.Count;
    }

}
