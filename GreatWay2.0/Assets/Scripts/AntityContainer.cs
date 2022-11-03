using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntityContainer : MonoBehaviour
{
    [SerializeField] GlobalVisionController _globalController;
    [SerializeField] TurnQeue _qeue;
    [SerializeField] GameObject _alliesContainer;
    [SerializeField] List<Antity> _players;
    [SerializeField] CurentHeroArrowScript _arrow;

    private List<Antity> Players = new List<Antity>();
    private List<Antity> TurnOrders = new List<Antity>();
    private CurentHeroArrowScript Arrow;
    private int CurentActivePlayerIndex;

    public List<Antity> antityes { get { return Players; } }

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
            newPlayer.GetComponent<CharacterStats>().RollInit();
            Players.Add(newPlayer);
            i++;
        }

        SortByInit();
        CurentActivePlayerIndex = 0;
        Arrow = Instantiate(_arrow);
        Arrow.SetTerget(Players[CurentActivePlayerIndex].gameObject);
        StartCoroutine(MakeGamePlayble());

        _globalController.AllLookOut();
    }

    IEnumerator MakeGamePlayble()
    {
        Debug.Log("??");
        yield return new WaitForSeconds(7f);
        Debug.Log("START");
        _qeue.SetQeue(Players);
        yield return new WaitForSeconds(1f);

        Players[0].isActive = true;

        FindObjectOfType<MapController>().isInterfaceActive = true;
    }

    private void SortByInit()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            int j = i;
            Antity t = Players[i];
            while (j > 0 && Players[j - 1].GetComponent<CharacterStats>().init < t.GetComponent<CharacterStats>().init)
            {
                Players[j] = Players[j - 1];
                if (CurentActivePlayerIndex == j - 1)
                    CurentActivePlayerIndex++;
                j--;
            }

            Players[j] = t;
        }

        foreach (Antity player in Players)
            Debug.Log(player.GetComponent<CharacterStats>().init);
    }

    public void DeleteCreatures()
    {
        foreach (Antity player in Players)
            Destroy(player.gameObject);

        Players.Clear();

        _qeue.DeleteCreatures();
    }

    public void AddCreature(Antity Creature, Vector2 Pos)
    {
        Antity creature = Instantiate(Creature, _alliesContainer.transform);
        creature.transform.position = Pos;
        creature.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 100) / 100f, Random.Range(0, 100) / 100f, Random.Range(0, 100) / 100f, 1);
        creature.isActive = false;
        creature.name = creature.name + "_" + (Players.Count - 1).ToString();
        creature.GetComponent<PlayerMove>().yStep = FindObjectOfType<GridContainer>().yStep;
        creature.GetComponent<CharacterStats>().RollInit();
        Players.Add(creature);

        SortByInit();

        _qeue.SetQeue(Players);
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
