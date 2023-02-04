using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityContainer : MonoBehaviour
{
    [SerializeField] GlobalVisionController _globalController;
    [SerializeField] TurnQeue _qeue;
    [SerializeField] GameObject _alliesContainer;
    [SerializeField] GameObject _enviromentsContainer;
    [SerializeField] List<Entity> _players;
    [SerializeField] CurentHeroArrowScript _arrow;
    [SerializeField] ActionsPanel _panel;
    [SerializeField] GridContainer _grid;

    private List<Entity> Players = new List<Entity>();
    private List<Entity> TurnOrders = new List<Entity>();
    private List<Enviroment> Enviroments = new List<Enviroment>();
    private CurentHeroArrowScript Arrow;
    private int CurentActivePlayerIndex = -1;
    private EnviromentChecking Input;
    private Camera Cam;

    private List<Entity> AllyesList = new List<Entity>();
    private List<Entity> EnemyesList = new List<Entity>();

    public List<Entity> antityes { get { return Players; } }
    public Entity currentPlayer { get { return Players[CurentActivePlayerIndex]; } }

    private void Awake()
    {
        Cam = GameObject.Find("UICanvas").GetComponent<Canvas>().worldCamera;
        Input = new EnviromentChecking();
        Input.CheckEnviroment.RightClick.performed += context =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.transform.parent.TryGetComponent(out Enviroment obj) && obj.isInteracteble)
            {
                if (Mathf.Pow(currentPlayer.transform.position.x - obj.transform.position.x, 2) + Mathf.Pow(currentPlayer.transform.position.y - obj.transform.position.y, 2) <= Mathf.Sqrt(2)
                && Mathf.Pow(currentPlayer.transform.position.x - obj.transform.position.x, 2) + Mathf.Pow(currentPlayer.transform.position.y - obj.transform.position.y, 2) != 0)
                    //_panel.gameObject.SetActive(true);
                    //_panel.transform.position = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    obj.OpenClose();
            }
        };

        Input.Enable();
    }

    bool IsPosFree(Vector2 pos)
    {
        foreach (Entity player in Players)
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
        foreach (Entity player in _players)
        {
            Entity newPlayer = Instantiate(player, _alliesContainer.transform);
            newPlayer.transform.position = GetFreePos();

            newPlayer.GetComponent<AntityVisualController>().baseColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            Debug.Log("GENERATED" + (i - 1));
            newPlayer.isActive = false;

            Debug.Log("GENERATED" + i);

            newPlayer.name = player.name + "_" + i.ToString();
            Debug.Log("GENERATED" + i + 1);

            newPlayer.GetComponent<PlayerMove>().yStep = FindObjectOfType<GridContainer>().yStep;
            newPlayer.GetComponent<CharacterStats>().RollInit();
            Players.Add(newPlayer);


            if (newPlayer.tag == "Ally")
                AllyesList.Add(newPlayer);
            else if (newPlayer.tag == "Enemy")
                EnemyesList.Add(newPlayer);


            i++;
        }
        Debug.Log("GENERATED");

        SortByInit();
        Arrow = Instantiate(_arrow);
        NextTurn();
        Arrow.SetTerget(Players[CurentActivePlayerIndex].gameObject);
        StartCoroutine(MakeGamePlayble());


        _globalController.AllLookOut();
    }

    IEnumerator MakeGamePlayble()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("START");
        _qeue.SetQeue(Players, CurentActivePlayerIndex);
        yield return new WaitForSeconds(1f);

        Players[0].isActive = true;

        FindObjectOfType<MapController>().isInterfaceActive = true;
    }

    private void SortByInit()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            int j = i;
            Entity t = Players[i];
            while (j > 0 && Players[j - 1].GetComponent<CharacterStats>().init < t.GetComponent<CharacterStats>().init)
            {
                Players[j] = Players[j - 1];
                if (CurentActivePlayerIndex == j - 1)
                    CurentActivePlayerIndex++;
                j--;
            }

            Players[j] = t;
        }

        foreach (Entity player in Players)
            Debug.Log(player.GetComponent<CharacterStats>().init);
    }

    public void DeleteCreatures()
    {
        foreach (Entity player in Players)
        {
            Destroy(player.gameObject);
            Destroy(player.GetComponent<UiController>().playerInteface.gameObject);
        }

        Players.Clear();
        AllyesList.Clear();
        EnemyesList.Clear();
        _qeue.DeleteCreatures();
    }

    public void DeleteChosenCreature()
    {
        Entity creature = _grid.GetChosenTile().GetComponent<TileContainer>().entityOnTile;
        if (creature != null)
            DeleteCreature(creature);

        Destroy(creature.gameObject);
    }

    public void DeleteEnviroment()
    {
        Enviroment obj = _grid.GetChosenTile().GetComponent<TileContainer>().objectOnTile;

        if (obj != null)
            Destroy(obj.gameObject);
    }

    public void DeleteCreature(Entity creature)
    {
        if (currentPlayer == creature)
            NextTurn();
        if (CurentActivePlayerIndex > Players.IndexOf(creature))
        {
            Debug.Log(CurentActivePlayerIndex + " " + Players.IndexOf(creature));
            CurentActivePlayerIndex--;
        }
        Players.Remove(creature);

        _qeue.SetQeue(Players, CurentActivePlayerIndex);

        _grid.GetTile(creature.transform.position).GetComponent<TileContainer>().DeleteCreatureFromTile();
        Destroy(creature.gameObject);
    }

    public void AddCreature(Entity Creature, Vector2 Pos)
    {
        Debug.Log(Creature + " " + _alliesContainer.transform);
        Entity creature = Instantiate(Creature, _alliesContainer.transform);
        Debug.Log(creature);
        creature.transform.position = Pos;
        creature.GetComponent<AntityVisualController>().baseColor = new Color(Random.Range(0, 100) / 100f, Random.Range(0, 100) / 100f, Random.Range(0, 100) / 100f, 1);
        creature.isActive = false;
        creature.name = creature.name + "_" + (Players.Count - 1).ToString();
        creature.GetComponent<Move>().yStep = FindObjectOfType<GridContainer>().yStep;
        creature.GetComponent<CharacterStats>().RollInit();
        Players.Add(creature);

        SortByInit();

        _qeue.SetQeue(Players, CurentActivePlayerIndex);
    }

    public void AddEnviroment(Enviroment obj, Vector2 Pos, float zAngle)
    {
        if (_grid.GetTile(Pos).GetComponent<TileContainer>().objectOnTile != null)
            Destroy(_grid.GetTile(Pos).GetComponent<TileContainer>().objectOnTile.gameObject);


        Enviroment newObject = Instantiate(obj, _enviromentsContainer.transform);
        newObject.transform.position = new Vector3(Pos.x, Pos.y, -1);
        newObject.transform.Rotate(0, 0, zAngle);
    }

    public void NextTurn()
    {
        if (CurentActivePlayerIndex >= 0)
            Players[CurentActivePlayerIndex].isActive = false;

        CurentActivePlayerIndex++;
        if (CurentActivePlayerIndex >= Players.Count)
            CurentActivePlayerIndex = 0;

        Players[CurentActivePlayerIndex].isActive = true;
        Players[CurentActivePlayerIndex].NextTurn();
        Arrow.SetTerget(Players[CurentActivePlayerIndex].gameObject);
        _qeue.NextTurn();
    }
}
