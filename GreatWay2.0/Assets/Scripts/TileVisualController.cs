using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualController : MonoBehaviour
{
    [SerializeField] Sprite _fogStrite;

    private bool IsVisible;
    private bool IsSeen;
    private SpriteRenderer SpriteR;
    private Sprite BaseSprite;

    public bool isVisible { set { if (value && !IsSeen) MakeSeen(); ChageVisibleMode(value); } }

    private void Awake()
    {
        SpriteR = GetComponent<SpriteRenderer>();
        BaseSprite = SpriteR.sprite;
        SpriteR.sprite = _fogStrite;
    }

    private void MakeSeen()
    {
        IsSeen = true;
        SpriteR.sprite = BaseSprite;
        GetComponent<ParticleSystem>().Play();
    }

    private void ChageVisibleMode(bool value)
    {
        if (value)
            GetComponent<BasicTile>().SetBaseColor(Color.white);
        else
            GetComponent<BasicTile>().SetBaseColor(Color.gray);
        IsVisible = value;
    }
}
