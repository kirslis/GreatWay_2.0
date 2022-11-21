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

    public bool isSeen { set { IsSeen = value; if (!IsSeen) MakeUnSeen(); } }
    public bool isVisible { set { if (value && !IsSeen) MakeSeen(); ChangeVisibleMode(value); } }

    private void Awake()
    {
        SpriteR = GetComponent<SpriteRenderer>();
        BaseSprite = SpriteR.sprite;

        if (!FindObjectOfType<GlobalVisionController>().isInReductMode)
            SpriteR.sprite = _fogStrite;
    }

    private void MakeSeen()
    {
        IsSeen = true;
        SpriteR.sprite = BaseSprite;
        GetComponent<ParticleSystem>().Play();
    }

    private void MakeUnSeen()
    {
        IsSeen = false;
        SpriteR.sprite = _fogStrite;
    }

    private void ChangeVisibleMode(bool value)
    {
        if (value)
            GetComponent<BasicTile>().SetBaseColor(Color.white);
        else if(IsSeen)
            GetComponent<BasicTile>().SetBaseColor(Color.gray);
        IsVisible = value;
    }
}
