using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnQeue : MonoBehaviour
{
    private List<TurnIcon> Icons = new List<TurnIcon>();
    private List<Vector2> Poses = new List<Vector2>();
    private List<TurnIcon> VisibleIcons = new List<TurnIcon>();
    private RectTransform Rect;
    private float Space = 10;
    private float IconsSize = 50;
    private int ImagesCount;
    private bool IsFightStarted = false;
    private bool IsNeedToResize;
    private int IStart = 0;
    private int IEnd = 0;
    private int VisibleCount = 0;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            Poses.Add(new Vector2());
        }

        Rect = GetComponent<RectTransform>();

        Rect.sizeDelta = new Vector2(0, Rect.sizeDelta.y);
    }

    private void MoveIStartleft()
    {
        IStart++;
        if (IStart >= Icons.Count)
            IStart = 0;
        IEnd++;
        if(IEnd >= Icons.Count)
            IEnd = 0;

        Debug.Log(IStart + " " + IEnd);
    }

    public void SetQeue(List<Antity> Antities)
    {
        while (Antities.Count > Icons.Count)
            Icons.Add(new TurnIcon());

        foreach (TurnIcon icon in Icons)
        {
            if (icon != null && !Antities.Contains(icon.parrent))
                DeleteAnim(icon);
        }

        ImagesCount = Antities.Count;

        for (int i = 0; i < Antities.Count; i++)
        {
            if (!ContainCheck(Antities[i]))
            {
                for (int j = ImagesCount - 1; j > i; j--)
                {
                    Icons[j] = Icons[j - 1];
                    if (IStart == j - 1)
                        MoveIStartleft();
                }
                Icons[i] = Instantiate(Antities[i].GetComponent<CharacterStats>().icon, transform);
                Icons[i].transform.position = new Vector3(0, 100, 0);
                Icons[i].gameObject.SetActive(true);
                Icons[i].parrent = Antities[i].GetComponent<CharacterStats>().icon.parrent;
                Icons[i].GetComponent<Image>().color = Antities[i].GetComponent<SpriteRenderer>().color;
            }
        }

        if (!IsFightStarted)
        {
            IsFightStarted = true;
            IStart = 0;
        }

        Resize();
    }

    private bool ContainCheck(Antity ant)
    {
        foreach (TurnIcon icon in Icons)
            if (icon.parrent == ant)
                return true;

        return false;
    }

    private int GetIVisible(int index)
    {
        if (index >= IStart && (index <= IEnd || IStart > IEnd))
            return index - IStart;

        if (index <= IEnd && IEnd < IStart)
            return VisibleCount - IEnd + index - 1;

        return -1;
    }

    private void UpdateVisual()
    {
        Debug.Log(Icons.Count + " " + VisibleCount + " " + IStart + " " + IEnd);

        for (int i = 0; i < Icons.Count; i++)
        {
            int index = GetIVisible(i);

            if (index != -1)
            {
                if (Icons[i].isVisible == true)
                    MoveToPosAnim(Icons[i], Poses[index]);
                else
                    AddAnim(Icons[i], Poses[index]);
            }
            else if (Icons[i].isVisible == true)
                DeleteAnim(Icons[i]);
        }
    }

    public void NextTurn()
    {
        MoveIStartleft();

        UpdateVisual();
    }

    private void AddAnim(TurnIcon icon, Vector2 Pos)
    {
        icon.transform.localPosition = new Vector2(Pos.x, 70);
        icon.isVisible = true;

        StartCoroutine(MoveToPoseCourutine(icon, Pos));
    }

    private void DeleteAnim(TurnIcon icon)
    {
        Vector2 EndPos = new Vector2(icon.transform.localPosition.x, icon.transform.localPosition.y + 70);
        icon.isVisible = false;
        StartCoroutine(MoveToPoseCourutine(icon, EndPos));
    }

    private void MoveToPosAnim(TurnIcon icon, Vector2 Pos)
    {
        StartCoroutine(MoveToPoseCourutine(icon, Pos));
    }

    IEnumerator MoveToPoseCourutine(TurnIcon icon, Vector2 Pos)
    {
        while (!icon.transform.localPosition.Equals(Pos))
        {
            icon.transform.localPosition = Vector2.MoveTowards(icon.transform.localPosition, Pos, 1f);
            yield return null;
        }
    }

    private void Resize()
    {
        VisibleCount = ImagesCount < 10 ? ImagesCount : 10;
        IEnd = IStart + VisibleCount - 1;
        if(IEnd >= Icons.Count)
            IEnd -= Icons.Count;
        IsNeedToResize = true;
        ResizeAnim();
    }

    private void OnEnable()
    {
        if (IsNeedToResize)
            ResizeAnim();
    }

    private void ResizeAnim()
    {
        if (gameObject.activeInHierarchy)
        {
            StopCoroutine(ResizeCourutine());
            StartCoroutine(ResizeCourutine());
        }
    }

    private void changePoses()
    {
        for (int i = 0; i < VisibleCount; i++)
        {
            Poses[i] = new Vector2(-Rect.sizeDelta.x / 2 + IconsSize / 2 + Space + i * (IconsSize + Space), 0);
        }
    }

    IEnumerator ResizeCourutine()
    {
        float resizeSpeed = 500f;
        float newSizeX = VisibleCount * (Icons[0].GetComponent<RectTransform>().sizeDelta.x + Space) + Space;
        while (Rect.sizeDelta.x != newSizeX)
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, new Vector2(newSizeX, Rect.sizeDelta.y), resizeSpeed * Time.deltaTime);
            yield return null;
        }

        changePoses();
        UpdateVisual();
        IsNeedToResize = false;
    }

    public void DeleteCreatures()
    {
        foreach (TurnIcon icont in Icons)
            Destroy(icont.gameObject);

        Icons.Clear();

        VisibleCount = 0;
        VisibleIcons.Clear();

        IStart = 0;
        Rect.sizeDelta = new Vector2(0, Rect.sizeDelta.y);
    }
}
