using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionScript : MonoBehaviour
{
    private Text TextViewer;
    private delegate void Emotion();
    private Dictionary<EmotionType, Emotion> EmotionDictionary;

    public enum EmotionType
    {
        shake
    }

    private void Awake()
    {
        TextViewer = GetComponent<Text>();
        EmotionDictionary = new Dictionary<EmotionType, Emotion>()
        {
            { EmotionType.shake, ShakeAnim}
        };

        TextViewer.enabled = false;
    }

    public void SetText(string text, Color color, EmotionType EmotionType)
    {
        TextViewer.enabled = true;
        TextViewer.text = text;
        TextViewer.color = color;

        EmotionDictionary.TryGetValue(EmotionType, out Emotion emotion);
        emotion();
    }

    private void ShakeAnim()
    {
        StartCoroutine(ShakeCourutine());
    }

    IEnumerator ShakeCourutine()
    {
        TextViewer.fontSize = 0;
        TextViewer.transform.eulerAngles = new Vector3(0, 0, 345);

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("ROTATION " + TextViewer.transform.eulerAngles.z);

            while (TextViewer.transform.eulerAngles.z < 359)
            {
                TextViewer.transform.eulerAngles = new Vector3(0, 0, TextViewer.transform.eulerAngles.z + 75 * Time.deltaTime);
                if (TextViewer.fontSize <= 10)
                    TextViewer.fontSize += 1;
                yield return null;
            }

            while (TextViewer.transform.eulerAngles.z > 345)
            {
                TextViewer.transform.eulerAngles = new Vector3(0, 0, TextViewer.transform.eulerAngles.z - 75 * Time.deltaTime);

                if (TextViewer.fontSize <= 10)
                    TextViewer.fontSize += 1;
                yield return null;
            }

        }

        while (TextViewer.fontSize > 0)
        {
            TextViewer.fontSize -= 1;
            yield return null;
        }
        TextViewer.enabled = false;
    }
}
