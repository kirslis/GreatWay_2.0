using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    [SerializeField] int _pathCost;
    [SerializeField] bool _isPasseble;

    private bool IsPasseble;

    private int BasePathCost;
    private Color BaseColor;
    private Color VisibleColor;
    private SpriteRenderer Sprite;

    public int SpentMoveSpeed = -1;
    private int CurrentPathCost;

    public int basePathCost { get { return BasePathCost; }}
    public bool isPasseble { get { return IsPasseble; } set { IsPasseble = value; } }
    public Color visibleColor { get { return VisibleColor; } set { VisibleColor = value; Sprite.color = value; } }
    public int currentPathCost { get { return CurrentPathCost; } set { CurrentPathCost = value; } }

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();

        BasePathCost = _pathCost;
        CurrentPathCost = _pathCost;
        IsPasseble = _isPasseble;

        BaseColor = Sprite.color;
        visibleColor = BaseColor;
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
}
