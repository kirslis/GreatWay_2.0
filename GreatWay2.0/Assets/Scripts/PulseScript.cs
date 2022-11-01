using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour
{
    [SerializeField] Vector2 _baseSize;
    [SerializeField] Vector2 _maxSize;

    float _speed = 0.01f;

    private bool IsGrowing = true;

    void FixedUpdate()
    {
        if (IsGrowing && !transform.localScale.Equals(_maxSize))
            transform.localScale = Vector2.MoveTowards(transform.localScale, _maxSize, _speed);

        else if (!IsGrowing && !transform.localScale.Equals(_baseSize))
            transform.localScale = Vector2.MoveTowards(transform.localScale, _baseSize, _speed);

        else IsGrowing = !IsGrowing;
    }


}
