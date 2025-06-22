using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    Vector3 playerSize = new Vector3(0.35f, 0.35f, 0.35f);
    [SerializeField] Transform cam;
    Rigidbody rb;
    [SerializeField] UIMenu uiMenu;
    InputSystemActions inputActions;

    Vector2 inputVector = Vector2.zero;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystemActions();
        inputActions.Player.Enable();
    }

    void HandleInput()
    {
        inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        if (inputActions.Player.EscMenu.WasPressedThisFrame())
        {
            uiMenu.ToggleMenu();
        }
        inputVector.Normalize();
    }
    
    void Move()
    {
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        Vector3 moveDirAngle = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        Vector3 desiredMove = moveDirAngle * PlayerStats.Instance.MoveSpeed * Time.fixedDeltaTime;
            
        if (!Physics.BoxCast(transform.position, playerSize, moveDirAngle, out RaycastHit hit, transform.rotation, playerSize.x))
        {
            rb.MovePosition(rb.position + desiredMove);
        }
        else
        {
            Vector3 slideDirection = Vector3.ProjectOnPlane(desiredMove, hit.normal);
            if (!Physics.BoxCast(transform.position, playerSize, slideDirection.normalized, out _, transform.rotation, playerSize.x))
            {
                rb.MovePosition(rb.position + slideDirection);
            }
        }
    }

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * PlayerStats.Instance.RotateSpeed));
            }
        }
    }
    void Update()
    {
        HandleInput();
    }
    void FixedUpdate()
    {
        if (inputVector.magnitude > 0f)
        {
            Move();
        }
        FollowMouse();
    }
    void OnDestroy()
    {
        inputActions.Player.Disable();
    }
}