using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntityContainer : MonoBehaviour
{
    [SerializeField] TurnQeue _qeue;
    [SerializeField] GameObject _alliesContainer;
    [SerializeField] List<Antity> _players;
    [SerializeField] CurentHeroArrowScript _arrow;

    private List<Antity> Players = new List<Antity>();
    private List<Antity> TurnOrders = new List<Antity>();
    private CurentHeroArrowScript Arrow;
    private int CurentActivePlayerIndex;

    bool IsPosFree(Vector2 pos)
    {
        foreach (Antity player in Players)
            if (pos.Equals((Vector2)player.transform.position))
                return false;

        return true;
    }

    Vector2 GetFreePos()
    {
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(0, (int)Mathf.Sqrt(_players.Count) + 1), Random.Range(0, (int)Mathf.Sqrt(_players.Count) + 1));
        } while (!IsPosFree(pos));

        return pos;
    }

    public void GenerateCreatures()
    {
        int i = 0;
        foreach (Antity player in _players)
        {
            Antity newPlayer = Instantiate(player, _alliesContainer.transform);
            newPlayer.transform.position = GetFreePos();
            newPlayer.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            newPlayer.isActive = false;
            newPlayer.name = player.name + "_" + i.ToString();
            newPlayer.GetComponent<PlayerMove>().yStep = FindObjectOfType<GridContainer>().yStep;
            Players.Add(newPlayer);
            i++;
        }

        SortByInit();
        CurentActivePlayerIndex = 0;
        Arrow = Instantiate(_arrow);
        Arrow.SetTerget(Players[CurentActivePlayerIndex].gameObject);
        StartCoroutine(MakeGamePlayble());
    }

    IEnumerator MakeGamePlayble()
    {
        yield return new WaitForSeconds(7f);
        _qeue.SetQeue(Players);
        Players[0].isActive = true;
    }

    private void SortByInit()
    {
        foreach (Antity player in Players)
            player.GetComponent<CharacterStats>().RollInit();

        for (int i = 0; i < Players.Count; i++)
        {
            int j = i;
            Antity t = Players[i];
            while (j > 0 && Players[j - 1].GetComponent<CharacterStats>().init < t.GetComponent<CharacterStats>().init)
            {
                Players[j] = Players[j - 1];
                j--;
            }

            Players[j] = t;
        }
    }

    public void DeleteCreatures()
    {
        foreach (Antity player in Players)
            Destroy(player.gameObject);

        Players.Clear();

        _qeue.DeleteCreatures();
    }

    public void NextTurn()
    {
        Players[CurentActivePlayerIndex].isActive = false;

        CurentActivePlayerIndex++;
        if (CurentActivePlayerIndex >= Players.Count)
            CurentActivePlayerIndex = 0;

        Players[CurentActivePlayerIndex].isActive = true;
        Arrow.SetTerget(Players[CurentActivePlayerIndex].gameObject);
        _qeue.NextTurn();
    }
}
