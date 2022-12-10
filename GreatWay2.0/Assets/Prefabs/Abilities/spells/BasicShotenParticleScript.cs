using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.U2D.Path;
using UnityEngine;

public class BasicShotenParticleScript : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] GameObject _hit;

    private Antity Target;
    private BasicDamageAbilityScript Ability;
    private float YDelta;
    private Vector3 EndPos;
    private bool IsHitting;

    public Antity target { set { Target = value; YDelta = Target.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2; EndPos = new Vector3(Target.transform.position.x, Target.transform.position.y + YDelta, Target.transform.position.z - 1); } }
    public BasicDamageAbilityScript ability { set { Ability = value; } }
    public bool isHitting { set { IsHitting = value; if (!IsHitting) EndPos = new Vector3(EndPos.x + Random.Range(-30, 30)/20f, EndPos.y + Random.Range(-30, 30)/20f, EndPos.z); } }

    // Update is called once per frame
    void Update()
    {
        if (Target != null && !transform.position.Equals(EndPos))
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPos, _speed * Time.deltaTime);
            if (Target.transform.position.x >= transform.position.x)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else hit();
    }

    private void hit()
    {
        if (IsHitting)
            Ability.DealDamage(Target);
        Instantiate(_hit, EndPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
