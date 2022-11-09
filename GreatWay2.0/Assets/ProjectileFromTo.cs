using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileFromTo : MonoBehaviour
{
    protected GameObject Target;
    protected int Value;
    private Rigidbody Rb;

    public GameObject target { set { Target = value; } }
    public int value { set { Value = value; } }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null && !transform.position.Equals(Target.transform.position))
            Rb.AddForce(Target.transform.position - transform.position);
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target)
            Destroy(gameObject);
    }
}
