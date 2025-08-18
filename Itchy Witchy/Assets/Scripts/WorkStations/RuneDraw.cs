using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

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
    public InputManager inputManager;
    public RuneWorkstation workstation;
    public Belladona cat;
    public GameObject firstButton;
    



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
    public bool canDraw;

    private void Start()
    {
        previousCursorPosition = transform.position;
        playerLine.positionCount = 0;
        fixedWorldY = runeCenter.position.y;
        Cursor.visible = false;
        playerLineGameObject.transform.position = new Vector3(
                playerLineGameObject.transform.position.x,
                fixedWorldY,
                playerLineGameObject.transform.position.z);

    }

    public void ChooseTargetPath(int index)
    {
        targetLine = stampset.transform.GetChild(index).GetComponent<LineRenderer>();
        targetLineGameObject = targetLine.gameObject;
        targetLineGameObject.SetActive(true);
        
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
        // based on yt video
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
       
        if (!canDraw) 
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
            return;
        } 
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
        workstation.playerRune.skillAcurracy = playerAccuracy;
        playerLine.positionCount = 0;
        cursor.SetActive(false);
        targetLineGameObject.SetActive(false);
        inputManager.SwitchToGameplay();
        workstation.playerRune.finishedProduct = true;
        Outcome();
        playerLineGameObject.SetActive(false); // the object this script is on
        

    }

    public void Outcome()
    {
        if (workstation.playerRune.material != RuneInteractables.RuneMaterial.Stone || 
            workstation.playerRune.stamp != RuneInteractables.Stamp.Star)
        {
            cat.SetOutcome(RuneOutcome.WrongMaterials);
        }
        else if (workstation.playerRune.skillAcurracy < 0.60f)
        {
            cat.SetOutcome(RuneOutcome.LowAccuracy);
        }
        else 
        {
            cat.SetOutcome(RuneOutcome.Success);
        }
    }

    // based on that commentSS

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
            // Convert target point from local to world space
            Vector3 targetPointWorld = target.transform.TransformPoint(denseTarget[i]);
            targetPointWorld.y = fixedWorldY;

            bool covered = false;
            for (int j = 0; j < player.positionCount; j++)
            {
                Vector3 playerPoint = player.GetPosition(j);
                playerPoint.y = fixedWorldY;

                if (Vector3.Distance(targetPointWorld, playerPoint) <= threshold)
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
