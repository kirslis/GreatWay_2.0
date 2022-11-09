using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultViuverScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    private int Result = 0;

    public void AddValue(int value)
    {
        Result += value;
        if (Result <= 0)
            Result = 1;

        _text.text = Result.ToString();
    }
}
