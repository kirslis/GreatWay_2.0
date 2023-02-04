using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractListinger : MonoBehaviour
{
    protected List<KeyValuePair<string, InteractOption>> Options = new List<KeyValuePair<string, InteractOption>>();

    public delegate void InteractOption();
    public List<KeyValuePair<string, InteractOption>> options { get { return Options; } }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Options.Add(new KeyValuePair<string, InteractOption>("delete", Delete));
    }

    private void Delete()
    {
        FindObjectOfType<EntityContainer>().DeleteCreature(GetComponent<Entity>());
    }
}
