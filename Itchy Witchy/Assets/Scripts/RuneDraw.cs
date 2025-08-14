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

    public void HandlePoint()
    {
        //Vector2 screenPoint = cursorMove;
        //Vector3 worldPoint = cameraMain.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 10f ));
        //worldPoint.y = fixedWorldY;
        //cursor.transform.position = worldPoint;

        //Vector2 screenPos = cursorMove;
        //Vector3 worldPos = cameraMain.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
        //worldPos.y = fixedWorldY;
        //cursor.transform.position = worldPos;

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

    //controller.Move(move * moveSpeed * Time.deltaTime);

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
