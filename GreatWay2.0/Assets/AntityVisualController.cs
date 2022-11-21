using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntityVisualController : MonoBehaviour
{
    private Color BaseColor;

    public Color baseColor { set { BaseColor = value; color = value; } }
    public Color color { set { GetComponent<SpriteRenderer>().color = value; } }

    public void ResetColor()
    {
        color = BaseColor;
    }
}
