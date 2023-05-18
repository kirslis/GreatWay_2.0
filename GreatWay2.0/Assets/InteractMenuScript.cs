using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractMenuScript : MonoBehaviour
{
    [SerializeField] InteractButtonScript _interactButton;

    private Camera Cam;
    private delegate void Option();
    private InteractMenuActions Actions;
    private float Distance = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        Actions = new InteractMenuActions();
        Actions.MainActions.RightClick.performed += context => { TryToInteract(); };
        Actions.MainActions.AnyKey.performed += context => { Close(); };
        Actions.MainActions.LeftClick.performed += context => { ClickCheck(); };
        Actions.Enable();
        GetComponent<Image>().enabled = false;
    }

    public void ClickCheck()
    {
        RaycastHit hit;
        Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit);
        if (hit.collider != null)
            Debug.Log(hit.collider.gameObject.name);
        if (hit.collider == null || hit.collider.gameObject != gameObject)
            Close();
    }


    private void OnEnable()
    {
        Actions.Enable();
    }

    private void OnDisable()
    {
        Actions.Disable();
    }

    private void TryToInteract()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector3.forward, out hit, Distance, LayerMask.GetMask("Grid"));


        foreach (InteractButtonScript child in GetComponentsInChildren<InteractButtonScript>())
            Destroy(child.gameObject);

        if (hit.collider != null)
        {
            GetComponent<Image>().enabled = true;
            Vector2 mousePos = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = new Vector3(mousePos.x + GetComponent<RectTransform>().sizeDelta.x / 2, mousePos.y - GetComponent<RectTransform>().sizeDelta.y / 2, transform.position.z);
            GameObject TargetTile = hit.collider.transform.parent.gameObject;
            Entity targetEntity = TargetTile.GetComponent<TileContainer>().entityOnTile;
            Enviroment targetEnviroment = TargetTile.GetComponent<TileContainer>().objectOnTile;
            InteractButtonScript button = Instantiate(_interactButton, transform);
            button.SetTarget(TargetTile);
            if (targetEntity != null)
            {
                button = Instantiate(_interactButton, transform);
                button.SetTarget(targetEntity.gameObject);
            }

            if (targetEnviroment != null)
            {
                button = Instantiate(_interactButton, transform);
                button.SetTarget(targetEnviroment.gameObject);
            }
        }
        else
            GetComponent<Image>().enabled = false;
    }

    public void ShowInteract(GameObject target)
    {
        var actions = target.GetComponent<InteractListinger>().options;

        foreach (InteractButtonScript child in GetComponentsInChildren<InteractButtonScript>())
            Destroy(child.gameObject);

        foreach (var action in actions)
        {
            InteractButtonScript button = Instantiate(_interactButton, transform);
            button.SetAction(action.Value, action.Key);
        }

    }

    public void Close()
    {
        foreach (InteractButtonScript child in GetComponentsInChildren<InteractButtonScript>())
            Destroy(child.gameObject);
        transform.position = new Vector2(-1000, -1000);

        GetComponent<Image>().enabled = false;
    }
}
