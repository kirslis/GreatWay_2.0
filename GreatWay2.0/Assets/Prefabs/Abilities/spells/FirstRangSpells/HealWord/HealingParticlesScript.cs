using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingParticlesScript : BasicBuffParticle
{
    private Entity Target;
    private BasicHealAbility Ability;
    private ParticleSystem Particles;

    public Entity target { set { Target = value; } }
    public BasicHealAbility ability { set { Ability = value; } }

    private void Awake()
    {

    }

    public IEnumerator Play()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        Particles = GetComponent<ParticleSystem>();
        yield return BuffCourutine();
    }

    private IEnumerator BuffCourutine()
    {
        for (int i = 0; i < 40; i++)
        {
            Particles.emissionRate = i;
            yield return new WaitForSeconds(0.1f);
        }
        Ability.Heal(Target);

        Particles.emissionRate = 0;
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
