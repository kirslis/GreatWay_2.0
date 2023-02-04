using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroleTrailRedactor : MonoBehaviour
{
    [SerializeField] private ErrorText _errorMassage;
    [SerializeField] private GameObject _trailMark;
    [SerializeField] private GameObject _trailMarker;

    private Canvas Canvas;
    private GridContainer Grid;
    private MapRedactorMenu MapRedactorMenu;
    private AIMoveActions Input;
    private bool IsTrailCreating = false;
    private List<BasicTile> PatrouleTrail = new List<BasicTile>();
    private List<GameObject> TrailMarks = new List<GameObject>();
    private List<GameObject> TrailMarkers = new List<GameObject>();

    private void Awake()
    {
        Canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        MapRedactorMenu = GameObject.Find("MapRedactorFolder").GetComponent<MapRedactorMenu>();

        Grid = FindObjectOfType<GridContainer>();
        Input = new AIMoveActions();

        Input.actions.LMB.performed += context =>
        {
            if (IsTrailCreating)
            {
                BasicTile newTile = Grid.GetChosenTile();
                if (!newTile.isPasseble && newTile.GetComponent<TileContainer>().entityOnTile.gameObject != gameObject)
                    ThrowError("this point is not passeble");
                else
                {
                    PatrouleTrail.Add(newTile);
                    UpdateVisionOfMarks();
                }
            }
        };

        Input.actions.RMB.performed += context =>
        {
            BasicTile tileToDelete = Grid.GetChosenTile();

            if (PatrouleTrail.Contains(tileToDelete))
            {
                PatrouleTrail.Remove(tileToDelete);
                UpdateVisionOfMarks();
            }
        };

        Input.actions.Esc.performed += context =>
        {

            if (IsTrailCreating)
            {
                StopCreatingPatrouleTrial();
            }
        };

        Input.Enable();
    }

    private void ThrowError(string S)
    {
        ErrorText error = Instantiate(_errorMassage, Canvas.transform);
        error.StartFly(S, transform.position);
    }

    public void CreatePatrouleTrail()
    {
        UpdateVisionOfMarks();

        MapRedactorMenu.Hide();
        IsTrailCreating = true;
        Grid.StartTrailing();
        Input.Enable();
    }

    private void StopCreatingPatrouleTrial()
    {
        ClearTrilMarks();

        GetComponent<AIMove>().patroleTrail = PatrouleTrail;

        IsTrailCreating = false;
        Grid.AbortTrailing();
        Input.Disable();
        MapRedactorMenu.Show();
    }

    private void UpdateVisionOfMarks()
    {
        ClearTrilMarks();

        foreach (var tile in PatrouleTrail)
        {
            GameObject newMark = Instantiate(_trailMark);
            newMark.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, -1);
            TrailMarks.Add(newMark);
        }

        if (TrailMarks.Count > 0)
            for (int i = 0; i < TrailMarks.Count; i++)
            {
                GameObject marker = Instantiate(_trailMarker);
                marker.transform.position = TrailMarks[i].transform.position;

                int nextMarkindex = i != TrailMarks.Count - 1 ? i + 1 : 0;

                Vector2 Direction = TrailMarks[i].transform.position - TrailMarks[nextMarkindex].transform.position;
                marker.transform.right = -Direction;

                marker.GetComponent<SpriteRenderer>().size = new Vector2(Direction.magnitude / marker.transform.localScale.x, marker.GetComponent<SpriteRenderer>().size.y);

                TrailMarkers.Add(marker);
            }
    }

    private void ClearTrilMarks()
    {
        foreach (GameObject trailMark in TrailMarks)
            Destroy(trailMark);

        TrailMarks.Clear();

        foreach (GameObject trailMarker in TrailMarkers)
            Destroy(trailMarker);

        TrailMarkers.Clear();
    }
}
