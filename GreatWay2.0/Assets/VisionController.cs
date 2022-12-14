using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    [SerializeField] int VisionDistance;

    private List<BasicTile> VisibleTiles = new List<BasicTile>();
    private GlobalVisionController GlobalVision;
    private GridContainer Grid;
    private float YDeviation = 0;

    public int visionDistance { set { VisionDistance = value; } }

    private void Awake()
    {
        GlobalVision = FindObjectOfType<GlobalVisionController>();
        Grid = FindObjectOfType<GridContainer>();
    }

    public void LookOut()
    {
        Vector2 Pos = transform.position;

        VisibleTiles.Clear();

        int leftBorder = Pos.x - VisionDistance > 0 ? (int)Pos.x - VisionDistance : 0;
        int rightBorder = Pos.x + VisionDistance < Grid.sizeX ? (int)Pos.x + VisionDistance : Grid.sizeX - 1;

        int bottomBorder = Pos.y - VisionDistance > 0 ? (int)Pos.y - VisionDistance : 0;
        int topBorder = Pos.y + VisionDistance < Grid.sizeY ? (int)Pos.y + VisionDistance : Grid.sizeY - 1;

        for (int i = bottomBorder; i <= topBorder; i++)
        {
            for (int j = leftBorder; j <= rightBorder; j++)
            {
                if (Mathf.Sqrt(Mathf.Pow(j - Pos.x, 2) + Mathf.Pow(i - Pos.y, 2)) <= VisionDistance)
                    if (CheckTile(Grid.GetTile(new Vector2(j, i))))
                        VisibleTiles.Add(Grid.GetTile(new Vector2(j, i)));
            }
        }

        GlobalVision.AddVisibleTiles(VisibleTiles);
    }

    private bool CheckTile(BasicTile tile)
    {
        float Distance = Vector2.Distance(tile.transform.position, transform.position);
        Vector2 startPos = new Vector2(transform.position.x, transform.position.y + YDeviation);
        float halfOfTile = 0.5f;

        List<Vector2> directions = new List<Vector2>();
        //directions.Add(new Vector2(tile.transform.position.x - halfOfTile, tile.transform.position.y));
        //directions.Add(new Vector2(tile.transform.position.x + halfOfTile, tile.transform.position.y));
        //directions.Add(new Vector2(tile.transform.position.x, tile.transform.position.y - halfOfTile));
        //directions.Add(new Vector2(tile.transform.position.x, tile.transform.position.y + halfOfTile));
        directions.Add(new Vector2(tile.transform.position.x, tile.transform.position.y));


        foreach (Vector2 direction in directions)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(startPos, direction - startPos, Distance, LayerMask.GetMask("Grid"));
            int i = 0;
            bool isVisible = true;
            while (i < hits.Length && isVisible)
            {
                if (!hits[i].collider.GetComponent<BasicTile>().isSeeThrought && hits[i].collider.gameObject != tile.gameObject)
                    isVisible = false;

                i++;
            }

            if (isVisible)
            {
                Debug.DrawRay(startPos, (Vector2)tile.transform.position - startPos, Color.green, 3);
                directions.Clear();
                return true;
            }
        }

        Debug.DrawRay(startPos, (Vector2)tile.transform.position - startPos, Color.red, 3);
        
        directions.Clear();
        return false;
    }
}
