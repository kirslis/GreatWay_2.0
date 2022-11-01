using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurentHeroArrowScript : MonoBehaviour
{
    [SerializeField] float _speed;

    private GameObject Target;
    private float MaxDist;
    private int Direction = 1;

    private void Update()
    {
        if (Target != null)
        {
            Vector3 TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.5f + MaxDist * Direction, transform.position.z);
            if (!transform.position.Equals(TargetPos))
                transform.position = Vector3.MoveTowards(transform.position, TargetPos, _speed * Time.deltaTime);
            else
                Direction = -Direction;
        }
    }

    public void SetTerget(GameObject target)
    {
        Target = target;
        MaxDist = Target.GetComponent<SpriteRenderer>().size.y / 6;
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.5f + MaxDist * Direction, transform.position.z);
        transform.parent = target.transform;
    }
}
