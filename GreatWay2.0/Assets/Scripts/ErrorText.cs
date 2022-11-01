using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public void StartFly(string text, Vector2 Pos)
    {
        GetComponent<Text>().text = text;
        transform.position = Pos;
        StartCoroutine(ErrorTextFlyCoroutine());
    }

    IEnumerator ErrorTextFlyCoroutine()
    {
        yield return new WaitForSeconds(WaitTime);

        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y + 300);
        while (GetComponent<Text>().color.a > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            Color currColor  = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(currColor.r, currColor.g, currColor.b, currColor.a - 0.2f * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
