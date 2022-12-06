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
    [SerializeField] protected DataTypeHolderScript.AbiltyType _type;

    protected BasicUsibleTargetAreaViewer Area;
    protected AbilityController Caster;

    protected List<ButtonPin> CoreButtons = new List<ButtonPin>();
    protected List<ButtonPin> SubButtons = new List<ButtonPin>();
    private AbilityInputs Input;

    public AbilityController caster { set { Caster = value; } }
    public Sprite skillSprite { get { return _baseAbilityImage; } }
    public BasicUsibleTargetAreaViewer area { set { Area = value; } }
    public DataTypeHolderScript.AbiltyType type { get { return _type; } }

    //ссылка на кнопку и ее обладателя
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
        Input = new AbilityInputs();
        Input.Mouse.Use.performed += context => Use();
        Input.Mouse.AbortAiming.performed += context => Abort();
        Input.Disable();
    }

    public void OnAbilityClick(AbilityController player)
    {
        Debug.Log(player + " - " + _range + " - " + GetTargetTag() + " - " + Area);
        Input.Enable();
        Caster = player;
        Area.LightUpTargetArea(player.transform.position, _range, GetTargetTag());
    }

    public virtual void Use()
    {
        Abort();
    }

    public void Abort()
    {
        Area.AbortAiming();
        Input.Disable();
    }

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

    protected void DeleteSubButton(ButtonPin button)
    {
        Destroy(button.button.gameObject);
        SubButtons.Remove(button);
    }

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

    virtual public AbilityButton AddNewCoreButton(AbilityController Player)
    {
        AbilityButton NewButton = Instantiate(_baseAbilityButton);
        NewButton.ability = this;
        NewButton.player = Player;
        CoreButtons.Add(new ButtonPin(NewButton, Player));

        return NewButton;
    }

    public bool IsTileTargetble(BasicTile Tile)
    {
        return Area.IsTileInTaget(Tile);
    }

    public void Reaim(BasicTile NewTargetTile)
    {
        Area.Reaim(NewTargetTile, GetTargetTag());
    }

    private string GetTargetTag()
    {
        string TargetTag;
        if (_targetTag == "Ally" && Caster.tag == "Ally" || _targetTag == "Enemy" && Caster.tag == "Enemy")
            TargetTag = "Ally";
        else
            TargetTag = "Enemy";

        return TargetTag;
    }
}
