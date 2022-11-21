using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    private Antity EntityOnTile;

    public Antity entityOnTile { get { return EntityOnTile; } } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out CharacterStats ant))
        {
            EntityOnTile = ant.GetComponent<Antity>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out CharacterStats ant))
        {
            EntityOnTile = null;
        }
    }

    public void MarkTargets(Color color, string tag)
    {
        if (EntityOnTile != null && EntityOnTile.tag == tag)
                EntityOnTile.GetComponent<AntityVisualController>().color = color;
    }

    public void RefreshTargets()
    {
        if (EntityOnTile != null)
            EntityOnTile.GetComponent<AntityVisualController>().ResetColor();
    }

    public bool IsContainEntity()
    {
        return EntityOnTile != null;
    }
}
