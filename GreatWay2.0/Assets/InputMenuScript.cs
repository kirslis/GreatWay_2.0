using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteructMenu : MonoBehaviour
{
    private InteractMenuActions Actions;

    // Start is called before the first frame update
    void Start()
    {
        Actions = new InteractMenuActions();
        Actions.MainActions.RightClick.performed += context => { TryToInteruct(); };
        Actions.Enable();
    }

    private void OnEnable()
    {
        Actions.Enable();
    }

    private void OnDisable()
    {
        Actions.Disable();
    }

    private void TryToInteruct()
    {
        Debug.Log("Try");
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D[] hits = Physics2D.RaycastAll(Mouse.current.position.ReadValue(), Vector3.down);

        foreach (RaycastHit2D hit in hits)
            Debug.Log(hit.collider.gameObject);

    }

}
