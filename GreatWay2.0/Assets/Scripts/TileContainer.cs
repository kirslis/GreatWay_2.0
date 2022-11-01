using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    private List<Antity> EntitiesOnTile = new List<Antity>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out CharacterStats ant))
        {
            EntitiesOnTile.Add(ant.GetComponent<Antity>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out CharacterStats ant))
        {
            EntitiesOnTile.Remove(ant.GetComponent<Antity>());
        }
    }

    public bool IsContainEntity()
    {
        return EntitiesOnTile.Count > 0;
    }
}
