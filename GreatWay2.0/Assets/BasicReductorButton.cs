using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BasicReductorButton : MonoBehaviour
{
    [SerializeField] protected GameObject _resourse;
    [SerializeField] protected DescriptionViewer _descriptionViewer;
    [SerializeField] protected Image _baseSprite;
    [SerializeField] protected Text _TextViewer;
    [SerializeField] protected string _name;

    protected MapController Map;
    protected bool IsMouseDown;
    protected bool IsReducting { set { MiniTile.gameObject.SetActive(value); } get { return MiniTile.gameObject.activeSelf; } }

    private bool IsMouseIn;
    private float WaitTime;
    private float WaitTimeToOpenDescription = 1f;
    private RedactorButtonActions Input;
    private Vector3 MiniSize = new Vector3(1, 1, 1f);
    private Vector2 MiniDeviation = new Vector2(-20, 30);
    private Image MiniTile;

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsMouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsMouseIn = false;
        WaitTime = 0;
        _descriptionViewer.CloseDescription();
    }

    protected virtual void Awake()
    {
        if (_name != "")
            _TextViewer.text = _name;
        else
            _TextViewer.text = _resourse.name;


        Input = new RedactorButtonActions();
        Input.Reduct.AbortReduct.performed += context => { if (IsReducting) AbortReduct(); };
        Input.Reduct.ReductMap.performed += context => { if (IsReducting) IsMouseDown = !IsMouseDown; };

        Map = FindObjectOfType<MapController>();

        InstantiateSemiResourse(ref MiniTile, MiniSize);
        MiniTile.name = "mini_tile";
    }

    private void OnEnable()
    {
        Input.Enable();
    }

    private void InstantiateSemiResourse(ref Image resourse, Vector3 size)
    {
        resourse = Instantiate(_baseSprite, transform);
        resourse.sprite = _resourse.GetComponent<SpriteRenderer>().sprite;
        resourse.transform.localScale = size;
        resourse.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsMouseIn)
        {
            WaitTime += Time.deltaTime;
        }

        if (WaitTime > WaitTimeToOpenDescription)
        {
            _descriptionViewer.gameObject.SetActive(true);
            _descriptionViewer.SetDescription(_resourse.GetComponent<BasicDescription>());
            _descriptionViewer.OpenDescription(Mouse.current.position.ReadValue());
        }

        if (IsReducting)
        {
            MiniTile.transform.position = Mouse.current.position.ReadValue() + MiniDeviation;
        }
    }

    public void StartReduct()
    {
        IsReducting = true;
        Map.StartReduct();
    }

    private void AbortReduct()
    {
        IsReducting = false;
        IsMouseDown = false;
        Map.AbortReduct();
    }

}
