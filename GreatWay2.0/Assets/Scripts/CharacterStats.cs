using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] TurnIcon _icon;
    [SerializeField] PseudoThrowViewer _pseudoThrowViewer;
    [SerializeField] int[] _addictionalStats = new int[6];
    [SerializeField] Sprite _image;
    [SerializeField] int _countOfHPDices;
    [SerializeField] int _typeOfHPDice;

    private List<int> Stats = new List<int>();
    private List<int> StatMods = new List<int>();
    private Dictionary<string, int> StatsDictionry = new Dictionary<string, int>();
    private int CountOfStats = 6;
    private int Init = -1;
    private TurnIcon Icon;
    private int MaxHP;
    private int CurrentHp;
    private UiController UI;

    public Sprite image { get { return _image; } }
    public TurnIcon icon { get { return Icon; } }
    public int init { get { return Init; } }

    private void Awake()
    {
        UI = GetComponent<UiController>();

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

        int CurentStatIndex;
        StatsDictionry.TryGetValue("fort", out CurentStatIndex);

        for (int i = 0; i < _countOfHPDices; i++)
        {
            int Add = (Random.Range(1, _typeOfHPDice + 1) + StatMods[CurentStatIndex]);
            MaxHP += Add > 0 ? Add : 1;
        }

        CurrentHp = MaxHP;

        UI.playerInteface.maxHp = MaxHP;
        UI.playerInteface.currentHp = CurrentHp;

        UI.playerInteface.UpdateStatsView();
    }

    public void DealDamage(int damge, string DamageType)
    {
        CurrentHp -= damge;
        UI.playerInteface.UpdateStatsView();
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
        Debug.Log(name);

        int mod;
        StatsDictionry.TryGetValue("dex", out mod);
        int Value = Random.Range(1, 21);

        _pseudoThrowViewer.gameObject.SetActive(true);
        _pseudoThrowViewer.View(Value, StatMods[mod]);
        Init = (Value + StatMods[mod]) > 0 ? Value + StatMods[mod] : 1;
    }
}
