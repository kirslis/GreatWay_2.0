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

    int IStart = 0;
    int VisibleCount = 0;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            Poses.Add(new Vector2());
        }

        Rect = GetComponent<RectTransform>();

        Rect.sizeDelta = new Vector2(0, Rect.sizeDelta.y);
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
                    IStart = IStart == j - 1 ? j : IStart;
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

    //public void AddIcon(TurnIcon Icon)
    //{
    //    TurnIcon icon = Instantiate(Icon, transform);
    //    Icons.Add(icon);

    //    int i = Icons.Count - 1;
    //    while (i > 0 && Icons[i - 1].init < Icons[i].init)
    //    {
    //        TurnIcon t = Icons[i - 1];
    //        Icons[i - 1] = Icons[i];
    //        Icons[i] = t;
    //        i--;
    //    }

    //    Resize();
    //}

    private void UpdateVisual()
    {
        Debug.Log(ImagesCount);
        int IVisible = 0;
        for (int i = 0; i < ImagesCount; i++)
        {
            if (i >= IStart && i < IStart + VisibleCount || IStart + VisibleCount > Icons.Count && i < VisibleCount - (Icons.Count - VisibleCount))
            {
                if (!Icons[i].isVisible)
                    AddAnim(Icons[i], Poses[IVisible]);

                else
                    MoveToPosAnim(Icons[i], Poses[IVisible]);

                IVisible++;
            }

            else if ((i == ImagesCount - 1 || i == IStart + VisibleCount) && Icons[i].isVisible)
                DeleteAnim(Icons[i]);
        }
    }

    public void NextTurn()
    {
        //TurnIcon T = Icons[0];
        //Icons.Remove(T);
        //Icons.Add(T);
        IStart += 1;
        if (IStart >= Icons.Count)
            IStart = 0;
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

        StopCoroutine(ResizeCourutine());
        StartCoroutine(ResizeCourutine());
    }

    private void changePoses()
    {
        Debug.Log("VISIBLE_COUNT - " + VisibleCount);
        Debug.Log(Rect.sizeDelta.x);
        for (int i = 0; i < VisibleCount; i++)
        {
            Poses[i] = new Vector2(-Rect.sizeDelta.x / 2 + IconsSize / 2 + Space + i * (IconsSize + Space), 0);
            Debug.Log(i + " " + (-Rect.sizeDelta.x / 2 + IconsSize / 2 + Space + i * (IconsSize + Space)));
        }
    }

    IEnumerator ResizeCourutine()
    {
        float resizeSpeed = 1f;
        float newSizeX = VisibleCount * (Icons[0].GetComponent<RectTransform>().sizeDelta.x + Space) + Space;
        while (Rect.sizeDelta.x != newSizeX)
        {
            Rect.sizeDelta = Vector2.MoveTowards(Rect.sizeDelta, new Vector2(newSizeX, Rect.sizeDelta.y), resizeSpeed);
            yield return null;
        }

        changePoses();
        UpdateVisual();
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
