using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionViewer : MonoBehaviour
{
    [SerializeField] Text BlackText;
    [SerializeField] Text GrayText;

    private float BaseWidh = 100;
    private bool IsOpen = false;

    public void SetDescription(BasicDescription ObjectsDescription)
    {
        BlackText.text = ObjectsDescription.blackDescription;
        GrayText.text = ObjectsDescription.grayDescription;
    }

    public void OpenDescription(Vector3 Pos)
    {
        if (!IsOpen)
        {
            Debug.Log(BlackText.rectTransform.sizeDelta.y);
            Debug.Log(GrayText.rectTransform.sizeDelta.y);
            StartCoroutine(OpenCoroutine(Pos));
            IsOpen = true;
        }
    }

    IEnumerator OpenCoroutine(Vector3 Pos)
    {
        yield return new WaitForEndOfFrame();

        RectTransform Rect = gameObject.GetComponent<RectTransform>();
        Rect.sizeDelta = new Vector2(BaseWidh, 0);
        transform.position = new Vector3(Pos.x, Pos.y + (BlackText.rectTransform.sizeDelta.y + GrayText.rectTransform.sizeDelta.y) * 4, Pos.z);
        Vector2 FinalSize = new Vector2(Rect.sizeDelta.x, BlackText.rectTransform.sizeDelta.y + GrayText.rectTransform.sizeDelta.y + 20);
        float sizeStep = (BlackText.rectTransform.sizeDelta.y + GrayText.rectTransform.sizeDelta.y) / 20;
        while (!Rect.sizeDelta.Equals(FinalSize))
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, FinalSize, sizeStep);
            yield return null;
        }

        Debug.Log("END");
    }

    public void CloseDescription()
    {
        if (IsOpen)
        {
            StartCoroutine(CloseCoroutine());
            IsOpen = false;
        }
    }

    IEnumerator CloseCoroutine()
    {
        RectTransform Rect = gameObject.GetComponent<RectTransform>();
        Vector2 FinalSize = new Vector2(Rect.sizeDelta.x, 0);
        float sizeStep = (BlackText.rectTransform.sizeDelta.y + GrayText.rectTransform.sizeDelta.y) / 20;
        while (!Rect.sizeDelta.Equals(FinalSize))
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, FinalSize, sizeStep);
            yield return null;
        }

        Rect.sizeDelta = new Vector2(0, 0);
        gameObject.SetActive(false);
    }
}
