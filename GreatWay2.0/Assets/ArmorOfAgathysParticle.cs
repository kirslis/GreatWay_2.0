using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorOfAgathysParticle : BasicBuffParticle
{
    private int RotateSpeed = 60;
    private float Scale = 3;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(ChangeSizeCoroutine());
    }

    private void Update()
    {
        transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
    }

    IEnumerator ChangeSizeCoroutine()
    {
        while(transform.localScale.y < Scale)
        {
            transform.localScale += Vector3.one * 0.03f;
            yield return null;
        }
    }
}
