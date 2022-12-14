using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ErrorText : MonoBehaviour
{
    private float WaitTime = 1f;
    private float speed = 30f;

    private void Awake()
    {
        Debug.Log("Error masage");
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void StartFly(string text, Vector3 Pos)
    {
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = text;
        else
            GetComponent<TextMeshPro>().text = text;
        transform.position = Pos;
        StartCoroutine(ErrorTextFlyCoroutine());
    }

    IEnumerator ErrorTextFlyCoroutine()
    {

        if (GetComponent<Text>() != null)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 300, 0);
            while (GetComponent<Text>().color.a > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                Color currColor = GetComponent<Text>().color;
                GetComponent<Text>().color = new Color(currColor.r, currColor.g, currColor.b, currColor.a - 0.2f * Time.deltaTime);
                yield return null;
            }
        }

        else if (GetComponent<TextMeshPro>() != null)
        {
            yield return new WaitForSeconds(WaitTime);
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 2, 0);

            while (GetComponent<TextMeshPro>().color.a > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed / 100 * Time.deltaTime);
                Color currColor = GetComponent<TextMeshPro>().color;
                GetComponent<TextMeshPro>().color = new Color(currColor.r, currColor.g, currColor.b, currColor.a - 0.2f * Time.deltaTime);
                yield return null;
            }
        }
        Destroy(gameObject);
    }
}
