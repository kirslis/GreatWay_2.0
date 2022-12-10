using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float ScrollSpeed;

    bool isMoving = false;
    private CameraActions InputActions;
    Vector2 moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InputActions = new CameraActions();
        InputActions.Move.Moving.performed += context => isMoving = true; ;
        InputActions.Move.Moving.canceled += context => { moveSpeed = Vector2.zero; isMoving = false; };
        InputActions.Move.Zoom.performed += Scroll;
        InputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            moveSpeed = InputActions.Move.Moving.ReadValue<Vector2>();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + moveSpeed.x, transform.position.y + moveSpeed.y, transform.position.z), Time.deltaTime * Speed * 10);
        }
    }
    private void Scroll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0 && Camera.main.orthographicSize < 9)
            Camera.main.orthographicSize += ScrollSpeed;
        else if (context.ReadValue<float>() > 0 && Camera.main.orthographicSize > 2)
            Camera.main.orthographicSize -= ScrollSpeed;
    }
}
