using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New BasicAbility", menuName = "Basic Ability", order = 51)]
public class BasicAbilityScript : ScriptableObject
{
    [SerializeField] public string _areaTypeTag = "single";

    [SerializeField] protected Sprite _baseAbilityImage;
    [SerializeField] public AbilityButton _baseAbilityButton;
    [SerializeField] protected string _targetTag;
    [SerializeField] protected int _range;
    [SerializeField] public string _abilityName;
    [SerializeField] protected DataTypeHolderScript.AbiltyType _type;
    [SerializeField] public DataTypeHolderScript.ActiveType _activeType;

    protected BasicUsibleTargetAreaViewer Area;

    protected AbilityController C;
    protected AbilityController Caster { set { C = value; } get { return C; } }
    protected AbilityManager AbilityManager;

    protected List<ButtonPin> CoreButtons = new List<ButtonPin>();
    protected List<ButtonPin> SubButtons = new List<ButtonPin>();

    public AbilityManager abilityManager { set { AbilityManager = value; } }
    public AbilityController caster { set { Caster = value; } }
    public Sprite skillSprite { get { return _baseAbilityImage; } }
    public BasicUsibleTargetAreaViewer area { set { Area = value; } }
    public DataTypeHolderScript.AbiltyType type { get { return _type; } }

    //������ �� ������ � �� ����������
    protected struct ButtonPin
    {
        public AbilityButton button;
        public AbilityController player;

        public ButtonPin(AbilityButton button, AbilityController player)
        {
            this.button = button;
            this.player = player;
        }

    }

    public virtual void Awake()
    {

    }

    //����������� ��� ������� �� ������ �����������. ��������� � ����� ������������
    public void OnAbilityClick(AbilityController player)
    {
        Caster = player;
        AbilityManager.curentAbility = this;

        Debug.Log(player + " - " + _range + " - " + GetTargetTag() + " - " + Area + " - " + name);

        Area.LightUpTargetArea(player.transform.position, _range, GetTargetTag(), true);
    }

    //�������� �� ����������� ������������� �����������.
    //� ������ ������, ��������� �� �� ���� � �������� ��������������� ��������
    public virtual IEnumerator TryToUse(AbilityButton button)
    {
        if (ActivateCheck())
        {
            UseAction();
            button.AbortAbility();
        }
        else
            Debug.Log("NoTarget");
        yield break;
    }

    protected virtual IEnumerator AbilityUse()
    {
        
        yield break;
    }

    private void UseAction()
    {
        if (_activeType == DataTypeHolderScript.ActiveType.mainActive)
            Caster.GetComponent<CharacterStats>().mainActive = false;
        else if (_activeType == DataTypeHolderScript.ActiveType.subActive)
            Caster.GetComponent<CharacterStats>().subActive = false;
    }

    //���������� ������ �������, ������� ����� ���� ����� �����������
    public List<DataTypeHolderScript.TargetAntity> GetTargets(AbilityController Caster)
    {
        Area.LightUpTargetArea(Caster.transform.position, _range, GetTargetTag(), false);
        return Area.targets;
    }

   public virtual IEnumerator UseOnTarget(BasicTile targetTile, AbilityController player)
    {
        Caster = player;
        Area.LightUpTargetArea(Caster.transform.position, _range, GetTargetTag(), false);
        Area.Reaim(targetTile, GetTargetTag());
        UseAction();
        yield return AbilityUse();
        Abort();
    }

    //��������� ���������� �����������
    public void Abort()
    {
        Debug.Log("Caster - " + Caster + " Spell name = " + name);
        Area.AbortAiming();
        if (Caster.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine playerStateMachine))
            playerStateMachine.SetNewState(new PlayerIdlingState(playerStateMachine));
        AbilityManager.curentAbility = null;
    }

    //�������� �� ������� ���-������ � �������. ���� ��� ����, ���������� ��
    protected bool TryGetSubButton(AbilityController player, out ButtonPin OutButton)
    {
        foreach (ButtonPin button in SubButtons)
            if (button.player.Equals(player))
            {
                OutButton = button;
                return true;
            }

        OutButton = SubButtons[0];
        return false;
    }

    //������� ���-������ button
    protected void DeleteSubButton(ButtonPin button)
    {
        Destroy(button.button.gameObject);
        SubButtons.Remove(button);
    }

    //������� ������ button
    public void DeleteButton(AbilityButton Button)
    {
        foreach (ButtonPin button in SubButtons)
            if (button.button.Equals(Button))
            {
                Destroy(button.button.gameObject);
                SubButtons.Remove(button);
                return;
            }
    }

    //��������� ����� ���-������ ��� �����������, ������ ����������, ���� �� ����, � ������ � � ��������� �������.
    virtual public AbilityButton AddNewSubButton(AbilityController Player)
    {
        if (SubButtons.Count > 0 && TryGetSubButton(Player, out ButtonPin button))
            DeleteSubButton(button);

        AbilityButton NewButton = Instantiate(_baseAbilityButton);
        NewButton.ability = this;
        NewButton.player = Player;
        SubButtons.Add(new ButtonPin(NewButton, Player));

        return NewButton;
    }

    //�������� ����� �������� ������ ����������� � ������ � � ��������� �������
    virtual public AbilityButton AddNewCoreButton(AbilityController Player)
    {
        AbilityButton NewButton = Instantiate(_baseAbilityButton);
        NewButton.ability = this;
        NewButton.player = Player;
        CoreButtons.Add(new ButtonPin(NewButton, Player));

        return NewButton;
    }

    //�������� �� ��, �������� �� ���� ����� ��� �����������
    public bool IsTileTargetble(BasicTile Tile)
    {
        return Area.IsTileInTaget(Tile);
    }

    //����������� �����������
    public void Reaim(BasicTile NewTargetTile)
    {
        Area.Reaim(NewTargetTile, GetTargetTag());
    }

    //���������� ��� ���� ������������ �������
    private string GetTargetTag()
    {
        string TargetTag;
        if (_targetTag == "Ally" && Caster.tag == "Ally" || _targetTag == "Enemy" && Caster.tag == "Enemy")
            TargetTag = "Ally";
        else
            TargetTag = "Enemy";

        return TargetTag;
    }

    //�������� �� ����������� ������������� �����������.
    //���� ��� ��������, ��������� ���������� ���������� � ������� muted
    protected bool ActivateCheck()
    {
        if (Area.targets.Count > 0)
        {
            Area.AbortAiming();
            if (Caster.TryGetComponent(out PlayerStateMachine playerStateMachine))
                playerStateMachine.SetNewState(new PlayerMutedState(playerStateMachine));
        }

        return Area.targets.Count > 0;
    }
}
