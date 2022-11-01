using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] TurnIcon _icon;
    [SerializeField] PseudoThrowViewer _pseudoThrowViewer;
    [SerializeField] int[] AddictionalStats = new int[6];

    private List<int> Stats = new List<int>();
    private List<int> StatMods = new List<int>();
    private Dictionary<string, int> StatsDictionry = new Dictionary<string, int>();
    private int CountOfStats = 6;
    private int Init = -1;
    private TurnIcon Icon;

    public TurnIcon icon { get { return Icon; } }
    public int init { get { return Init; } }

    private void Awake()
    {
        StatsDictionry = new Dictionary<string, int>()
        {
            {"str", 0 },
            {"dex", 1},
            {"fort", 2},
            {"int", 3},
            {"wis", 4},
            {"cha", 5},
        };

        for (int i = 0; i < CountOfStats; i++)
        {
            Stats.Add(new int());
            StatMods.Add(new int());
        }

        Icon = Instantiate(_icon, transform);
        Icon.parrent = GetComponent<Antity>();
        Icon.gameObject.SetActive(false);
        GenerateStats();
    }

    private void GenerateStats()
    {
        int DicesToStat = 4;
        for (int i = 0; i < CountOfStats; i++)
        {
            int minDiceValue = 7;
            for (int j = 0; j < DicesToStat; j++)
            {
                int diceValue = Random.Range(1, 6);
                Stats[i] += diceValue;
                minDiceValue = Mathf.Min(minDiceValue, diceValue);
            }

            Stats[i] -= minDiceValue;

            if (Stats[i] < 10)
                StatMods[i] = (Stats[i] - 11) / 2;
            else
                StatMods[i] = (Stats[i] - 10) / 2;
        }
    }

    public void RollInit()
    {
        int mod;
        StatsDictionry.TryGetValue("des", out mod);
        int Value = Random.Range(1, 20);

        _pseudoThrowViewer.gameObject.SetActive(true);
        _pseudoThrowViewer.View(Value, StatMods[mod]);
        Init = (Value + StatMods[mod]) > 0 ? Value + StatMods[mod] : 1;
    }
}
