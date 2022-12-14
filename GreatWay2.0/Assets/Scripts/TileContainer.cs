using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    private Antity EntityOnTile;
    public Enviroment ObjectOnTile;

    public Antity entityOnTile { get { return EntityOnTile; } }
    public Enviroment objectOnTile { get { return ObjectOnTile; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.parent.TryGetComponent(out CharacterStats ant))
        {
            EntityOnTile = ant.GetComponent<Antity>();
        }


        else if (collision.transform.TryGetComponent(out Enviroment obj))
        {
            ObjectOnTile = obj.GetComponent<Enviroment>();
            Debug.Log(ObjectOnTile);
        }

        Debug.Log("??");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out CharacterStats ant))
        {
            GetComponent<BasicTile>().isPasseble = true;
            EntityOnTile = null;
        }

        else if (collision.transform.TryGetComponent(out Enviroment obj))
        {
            Debug.Log("EXIT");
            GetComponent<BasicTile>().isPasseble = true;
            GetComponent<BasicTile>().isSeeThrought = true;
            GetComponent<BasicTile>().isAttackThrought = true;
            ObjectOnTile = null;
        }
    }

    public void DeleteCreatureFromTile()
    {
        GetComponent<BasicTile>().isPasseble = true;
        EntityOnTile = null;
    }

    public void DeleteObjectFromTile()
    {
        GetComponent<BasicTile>().isPasseble = true;
        ObjectOnTile = null;
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

    public void makeInvisible()
    {
        if (EntityOnTile != null)
            EntityOnTile.GetComponent<SpriteRenderer>().enabled = false;

        if (ObjectOnTile != null)
            if (ObjectOnTile.isStatic)
                ObjectOnTile.GetComponent<SpriteRenderer>().color = Color.gray;
            else
                ObjectOnTile.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void MakeUnSeen()
    {
        makeInvisible();

        if (ObjectOnTile != null && ObjectOnTile.isStatic)
            ObjectOnTile.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void makeSeen()
    {
        if (EntityOnTile != null)
            EntityOnTile.GetComponent<SpriteRenderer>().enabled = true;
        if (ObjectOnTile != null)
        {
            ObjectOnTile.GetComponent<SpriteRenderer>().enabled = true;
            ObjectOnTile.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
