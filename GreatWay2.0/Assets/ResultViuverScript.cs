using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultViuverScript : MonoBehaviour
{
    private TextMeshProUGUI Text;
    private int Result = 0;
    private ParticleSystem System;
    private int MaxStrickValue = 100;
    private Vector3 StartScale;
    private Vector3 CurrentScale;

    private void Awake()
    {
        System = GetComponent<ParticleSystem>();
        Text = GetComponent<TextMeshProUGUI>();
        StartScale = transform.localScale;
        CurrentScale = transform.localScale;
    }

    private void Update()
    {
        if (!transform.localScale.Equals(StartScale)){
            transform.localScale = Vector3.MoveTowards(transform.localScale, StartScale, Time.deltaTime);
        }   
    }

    public void AddValue(int value)
    {
        Result += value;
        if (Result <= 0)
            Result = 1;

        transform.localScale += Vector3.one * 0.1f;
        if (transform.localScale.x > 3)
            transform.localScale = Vector3.one * 3f;

        Text.text = Result.ToString();
        Text.color = Color.Lerp(Color.white, Color.red, (float)Result / MaxStrickValue > 1 ? 1 : (float)Result / MaxStrickValue);
        System.Play();
    }
}
