using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WindowEnviroment : Enviroment, IPointerClickHandler
{
    [SerializeField] private bool _isOpen = false;
    [SerializeField] Sprite _secondSprite;

    private void Awake()
    {
        Actions.Add(OpenClose);
    }

    public override void OpenClose()
    {
        Sprite t = _secondSprite;
        _secondSprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = t;

        _isAttackBlocking = !_isAttackBlocking;
        _visionBlocking = !_visionBlocking;

        base.OpenClose();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("WHAT?!");
    }

    private bool IsClickOnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == gameObject.GetComponent<Antity>().collider3d;
    }
}
