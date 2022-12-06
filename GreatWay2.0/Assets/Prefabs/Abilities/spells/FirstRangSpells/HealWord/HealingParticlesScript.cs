using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingParticlesScript : BasicBuffParticle
{
    private Antity Target;
    private BasicHealAbility Ability;
    private ParticleSystem Particles;

    public Antity target { set { Target = value; } }
    public BasicHealAbility ability { set { Ability = value; } }

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        Particles = GetComponent<ParticleSystem>();
        StartCoroutine(BuffCourutine());
    }

    IEnumerator BuffCourutine()
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
