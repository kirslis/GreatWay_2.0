using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PseudoThrowViewer : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    private Text ValueViewer;

    private void Awake()
    {
        ValueViewer = GetComponent<Text>();
    }

    public void View(int Value, int Bonus)
    {
        StartCoroutine(ViewCourutine(Value, Bonus));
    }

    IEnumerator ViewCourutine(int Value, int Bonus)
    {
        for (int i = 0; i < 30; i++)
        {
            ValueViewer.text = Random.Range(0, 20).ToString();
            yield return new WaitForSeconds(0.1f);
        }

        ValueViewer.text = Value.ToString();
        Color color;
        if (Value == 1)
            color = Color.red;
        else if (Value == 20)
            color = Color.yellow;
        else
            color = Color.green;

        ValueViewer.color = color;
        _particleSystem.startColor = color;
        _particleSystem.Play();

        yield return new WaitForSeconds(3f);

        ValueViewer.text = ((Value + Bonus) > 0 ? Value + Bonus : 1).ToString();

        yield return new WaitForSeconds(3f);

        ValueViewer.color = Color.green;
        //gameObject.SetActive(false);
    }
}
