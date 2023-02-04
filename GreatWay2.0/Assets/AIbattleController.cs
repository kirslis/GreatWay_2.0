using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIbattleController : MonoBehaviour
{
    private AIVisionConroller VisionController;
    private List<Entity> VisibleEntities = new List<Entity>();
    private List<Entity> VisibleEnemies = new List<Entity>();
    //private List

    private struct RegistredEnemy
    {
        private Entity Enemy;
        private BasicTile LastSeenTile;
        private int LeftTurnsForSearch;
    }

    private void Awake()
    {
        VisionController = GetComponent<AIVisionConroller>();
    }

    private void SeachForEntities()
    {
        List<BasicTile> VisibleTiles = VisionController.visibleTiles;

        foreach (BasicTile visibleTile in VisibleTiles)
            if (visibleTile.GetComponent<TileContainer>().entityOnTile != null)
            {
                Entity entity = visibleTile.GetComponent<TileContainer>().entityOnTile;
                VisibleEntities.Add(entity);
                bool haveSeenNewEnemy = false;
                if (entity.tag != tag && !VisibleEnemies.Contains(entity))
                {
                    VisibleEnemies.Add(entity);
                    haveSeenNewEnemy = true;
                }

                if (haveSeenNewEnemy)
                    GetComponent<EmotionViwerScript>().Confuse();
            }
    }
}
