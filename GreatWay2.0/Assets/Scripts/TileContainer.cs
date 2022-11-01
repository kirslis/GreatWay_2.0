using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    private List<Antity> EntitiesOnTile = new List<Antity>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Antity ant))
        {
            Debug.Log(ant.name + "_ENTER");
            EntitiesOnTile.Add(ant);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Antity ant))
        {
            Debug.Log(ant.name + "_EXIT");
            EntitiesOnTile.Remove(ant);
        }
    }
}
