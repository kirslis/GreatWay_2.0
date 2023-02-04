using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionViwerScript : MonoBehaviour
{
    private EmotionScript Emotion;

    private void Awake()
    {
        Emotion = GetComponentInChildren<Canvas>().GetComponentInChildren<EmotionScript>();
        if (Emotion == null)
            Debug.Log("No Emotion on Entity");
    }

    public void Confuse()
    {
        Emotion.SetText("?", Color.yellow, EmotionScript.EmotionType.shake);
    }
}
