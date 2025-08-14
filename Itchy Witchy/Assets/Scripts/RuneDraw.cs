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
    public float controllerSpeed = 5f;
    public float pointSpacing = 0.05f;
    public float accuracyThreshold = 0.2f;
    [SerializeField]
    private float fixedWorldY;

    private Vector3 previousCursorPosition;
    private Vector2 cursorMove;
    private bool isDrawing;

    private void Awake()
    {
        previousCursorPosition = transform.position;
        playerLine.positionCount = 0;

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
        Vector3 screenPoint = cursorMove;
        Vector3 worldPoint = cameraMain.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 10f));
        worldPoint.y = fixedWorldY;
        cursor.transform.position = worldPoint;
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
