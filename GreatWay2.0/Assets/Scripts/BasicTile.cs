using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    [SerializeField] int _pathCost;
    [SerializeField] private bool IsPasseble = true;
    [SerializeField] bool _isSeeThrought = true;

    private int BasePathCost;
    private Color BaseColor;
    private Color VisibleColor;
    private SpriteRenderer Sprite;
    private bool IsVisible;
    private bool IsSeen;
    private bool IsSeeThrought;

    public int SpentMoveSpeed = -1;
    private int CurrentPathCost;

    public bool isSeen { get { return IsSeen; } set { IsSeen = false; GetComponent<TileVisualController>().isSeen = false; } }
    public bool isSeeThrought { get { return IsSeeThrought; } set { } }
    public bool isVisible { set { IsVisible = value; if (value) IsSeen = true; GetComponent<TileVisualController>().isVisible = value; } get { return IsVisible; } }
    public int basePathCost { get { return BasePathCost; } }
    public bool isPasseble { get { return IsPasseble; } set { IsPasseble = value; } }
    public Color visibleColor { get { return VisibleColor; } set { VisibleColor = value; ChangeColorAnim(value); } }
    public int currentPathCost { get { return CurrentPathCost; } set { CurrentPathCost = value; } }

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();

        BasePathCost = _pathCost;
        CurrentPathCost = _pathCost;

        IsSeeThrought = _isSeeThrought;
        BaseColor = Sprite.color;
        visibleColor = BaseColor;
    }

    public void SetBaseColor(Color newColor)
    {
        visibleColor = newColor;
        BaseColor = newColor;
    }

    public void ChangeColor(Color newColor)
    {
        visibleColor = newColor;
    }

    public void RefreshTile()
    {
        visibleColor = BaseColor;
        SpentMoveSpeed = -1;
    }

    private void ChangeColorAnim(Color newColor)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColorCoroutine(newColor));
    }

    IEnumerator ChangeColorCoroutine(Color newColor)
    {
        float speed = 2f;
        while (!Sprite.color.Equals(newColor))
        {
            Sprite.color = Vector4.MoveTowards(Sprite.color, newColor, speed * Time.deltaTime);
            yield return null;
        }
    }
}
