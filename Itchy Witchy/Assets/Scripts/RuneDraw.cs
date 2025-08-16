using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class RuneDraw : MonoBehaviour
{
    public GameObject cursor;


    private Vector2 moveInput;
    public Camera cameraMain;

    public LineRenderer targetLine;
    public LineRenderer playerLine;

    [Header("Settings")]
    public float controllerSpeed = 4f;
    public float pointSpacing = 0.05f;
    public float accuracyThreshold = 0.2f;
    public float deadzone = 0.1f;
    float maxMovePerFrame = 0.1f;
    [SerializeField]
    private float fixedWorldY;

    private Vector2 screenBoundaries;
    private Vector3 previousCursorPosition;
    private Vector2 cursorMove;
    private bool isDrawing;
    public float smoothSpeed = 5f; // Higher = snappier, lower = smoother
    private Vector3 smoothedMove;

    private void Awake()
    {
        previousCursorPosition = transform.position;
        playerLine.positionCount = 0;
        Cursor.visible = false;
        screenBoundaries = cameraMain.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f - fixedWorldY));

    }

    private void Update()
    {
        HandlePoint();

        if (isDrawing) 
        {
            DrawingStart();
        }
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBoundaries.x, screenBoundaries.x * -1);
        viewPos.z = Mathf.Clamp(viewPos.y, screenBoundaries.y, screenBoundaries.y * -1);
        transform.position = viewPos;
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

        // Smooth the movement vector
        smoothedMove = Vector3.Lerp(smoothedMove, move, Time.deltaTime * smoothSpeed);
        smoothedMove = Vector3.ClampMagnitude(smoothedMove, maxMovePerFrame);

        cursor.transform.position += smoothedMove;

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
        playerLine.positionCount = 0;
        isDrawing = false;
    }
}
