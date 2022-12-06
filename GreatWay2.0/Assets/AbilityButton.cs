using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AbilityButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Image _blankImage;

    private ActionButtonActions Actions;
    protected Camera Cam;
    protected Image SmallButton;
    private BasicAbilityScript Ability;
    private AbilityController Player;
    protected bool IsAiming;
    private BasicTile LastTargetTile = null;

    public BasicAbilityScript ability { set { Ability = value; GetComponent<Image>().sprite = value.skillSprite; SmallButton.sprite = value.skillSprite; Debug.Log(name); } get { return Ability; } }
    public AbilityController player { set { Player = value;  } }

    private void Awake()
    {
        Actions = new ActionButtonActions();
        Actions.Chosen.mouseRight.performed += context => StopAiming();
        Actions.Disable();

        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        SmallButton = Instantiate(_blankImage, transform);
        SmallButton.sprite = GetComponent<Image>().sprite;
        Color baseColor = SmallButton.color;
        SmallButton.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0.5f);
        SmallButton.enabled = false;
        SmallButton.rectTransform.sizeDelta = Vector2.one * 50;
        SmallButton.maskable = false;
    }

    private void Update()
    {
        if (IsAiming)
        {
            ////Debug.Log(Mouse.current.position.ReadValue());
            //Debug.Log("Raycast" + Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit1) /*+*/
            //        + " hit " + hit1.transform.parent.TryGetComponent(out BasicTile targetTile1) 
            //    " targetTile " + targetTile1 +
            //    " Equals " + !targetTile1.Equals(LastTargetTile) +
            //"is targetble " + Ability.IsTileTargetble(targetTile1)
            //);
            ////Debug.Log(Cam.ScreenToViewportPoint(Mouse.current.position.ReadValue()));
            ///

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, 1000, LayerMask.GetMask("Grid"))
                && hit.transform.parent.TryGetComponent(out BasicTile targetTile) && !targetTile.Equals(LastTargetTile)
                && Ability.IsTileTargetble(targetTile))
            {
                LastTargetTile = targetTile;
                Debug.Log("Reaiming");
                Ability.Reaim(targetTile);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 Pos = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Pos.z = 0;
        SmallButton.rectTransform.position = Pos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Button>().enabled = false;
        SmallButton.enabled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SmallButton.enabled = false;
        if (Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit) && hit.transform.TryGetComponent(out SpellPlace place))
        {
            place.ChangeSpell(this, Player);
        }
        else
            Debug.Log(Physics.Raycast(Cam.ScreenPointToRay(Mouse.current.position.ReadValue())));

        GetComponent<Button>().enabled = true;
    }

    virtual public void OnClick()
    {
        Debug.Log("CLICK");
        IsAiming = true;
        Debug.Log("PLAYER = " + Player);
        Ability.OnAbilityClick(Player);
        Actions.Enable();
    }

    virtual protected void StopAiming()
    {
        IsAiming = false;
        Ability.Abort();
        Actions.Disable();
    }
}
