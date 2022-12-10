using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] TurnIcon _icon;
    [SerializeField] PseudoThrowViewer _pseudoThrowViewer;
    [SerializeField] int[] _addictionalStats = new int[6];
    [SerializeField] Sprite _image;
    [SerializeField] int _countOfHPDices;
    [SerializeField] int _typeOfHPDice;

    public delegate void DamageIncomingModificator(ref int Damage, DataTypeHolderScript.DamageType damageType, DataTypeHolderScript.AttackType attackType, CharacterStats Person, CharacterStats DamageDealer);
    public delegate void HurtListener(CharacterStats Person);

    private List<DamageIncomingModificator> DamageIncomingModificators = new List<DamageIncomingModificator>();
    private List<HurtListener> HurtListeners = new List<HurtListener>();

    private Animator Anim;
    private List<int> Stats = new List<int>();
    private List<int> StatMods = new List<int>();
    private Dictionary<string, int> StatsDictionry = new Dictionary<string, int>();
    private int CountOfStats = 6;
    private int Init = -1;
    private TurnIcon Icon;
    private int MaxHP;
    private int CurrentHP;
    private int TemporaryHP;
    private int AC;
    private UiController UI;
    private AbilityManager AbilityManager;

    public int temporaryHP { set { if (value > TemporaryHP) { TemporaryHP = value; UI.playerInteface.temporaryHP = value; UI.playerInteface.UpdateStatsView(); } } get { return TemporaryHP; } }
    public Sprite image { get { return _image; } }
    public TurnIcon icon { get { return Icon; } }
    public int init { get { return Init; } }

    public int ac { get { return AC; } }

    private void Awake()
    {
        AbilityManager = FindObjectOfType<AbilityManager>();
        Anim = GetComponent<Animator>();
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

        StatsDictionry.TryGetValue("dex", out CurentStatIndex);

        AC = 10 + StatMods[CurentStatIndex];

        CurrentHP = MaxHP;

        UI.playerInteface.aC = AC;
        UI.playerInteface.maxHp = MaxHP;
        UI.playerInteface.currentHp = CurrentHP;

        UI.playerInteface.UpdateStatsView();
    }

    public void AddDamageIncomingModificator(DamageIncomingModificator newMod)
    {
        DamageIncomingModificators.Add(newMod);
    }

    public void DeleteDamageIncomingModificator(DamageIncomingModificator mod)
    {
        StartCoroutine(DeleteModificatorCourutine(mod));
    }

    IEnumerator DeleteModificatorCourutine(DamageIncomingModificator mod)
    {
        yield return null;
        DamageIncomingModificators.Remove(mod);
    }

    public void AddHurtListener(HurtListener newListener)
    {
        HurtListeners.Add(newListener);
    }

    public void DeleteHurtListener(HurtListener listener)
    {
        StartCoroutine(DeleteHurtListenerCoriutine(listener));
    }

    IEnumerator DeleteHurtListenerCoriutine(HurtListener Listener)
    {
        yield return null;
        HurtListeners.Remove(Listener);
    }

    public void DealDamage(int damage, DataTypeHolderScript.DamageType DamageType, DataTypeHolderScript.AttackType attackType, CharacterStats DamageDealer)
    {
        foreach (DamageIncomingModificator damageMod in DamageIncomingModificators)
            damageMod(ref damage, DamageType, attackType, this, DamageDealer);
        if (TemporaryHP > 0)
        {
            if (TemporaryHP >= damage)
            {
                AbilityManager.RollMassage(transform.position, "", damage, Color.blue);
                TemporaryHP -= damage;
                damage = 0;
            }
            else
            {
                AbilityManager.RollMassage(transform.position, "", TemporaryHP, Color.blue);
                damage -= TemporaryHP;
                TemporaryHP = 0;
            }
        }

        UI.playerInteface.temporaryHP = TemporaryHP;

        if (damage > 0)
        {
            AbilityManager.RollMassage(transform.position, "", damage, Color.red);
            CurrentHP -= damage;
            UI.playerInteface.currentHp = CurrentHP;
            Anim.SetTrigger("Hurt");
        }
        else
            AbilityManager.RollMassage(transform.position, "blocked", -1, Color.gray);

        foreach (HurtListener listener in HurtListeners)
            listener(this);
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
        int mod;
        StatsDictionry.TryGetValue("dex", out mod);
        int Value = Random.Range(1, 21);

        _pseudoThrowViewer.gameObject.SetActive(true);
        _pseudoThrowViewer.View(Value, StatMods[mod]);
        Init = (Value + StatMods[mod]) > 0 ? Value + StatMods[mod] : 1;
    }

    public void Heal(int heal)
    {
        AbilityManager.RollMassage(transform.position, "", heal, Color.green);
        CurrentHP += heal;
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;

        UI.playerInteface.currentHp = CurrentHP;
        UI.playerInteface.UpdateStatsView();

        StartCoroutine(HealCoroutine());
    }

    IEnumerator HealCoroutine()
    {
        AntityVisualController VC = GetComponent<AntityVisualController>();
        for (int i = 0; i < 3; i++)
        {
            VC.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            VC.ResetColor();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
