using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShotenParticleScript : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] GameObject _hit;

    private Antity Target;
    private BasicDamageAbilityScript Ability;
    private float YDelta;
    private Vector3 EndPos;

    public Antity target { set { Target = value; YDelta = Target.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2; EndPos = new Vector3(Target.transform.position.x, Target.transform.position.y + YDelta, Target.transform.position.z - 1); } }
    public BasicDamageAbilityScript ability { set { Ability = value; } }

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
        Ability.DealDamage(Target);
        Instantiate(_hit, EndPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
