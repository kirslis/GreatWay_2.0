using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New BasicAbility", menuName = "Basic Ability", order = 51)]
public class BasicAbilityScript : ScriptableObject
{
    [SerializeField] public string _areaTypeTag = "single";

    [SerializeField] Sprite _baseAbilityImage;
    [SerializeField] AbilityButton _baseAbilityButton;
    [SerializeField] string _targetTag;
    [SerializeField] int _range;

    protected BasicUsibleTargetAreaViewer Area;

    private List<ButtonPin> CoreButtons = new List<ButtonPin>();
    private List<ButtonPin> SubButtons = new List<ButtonPin>();
    private AbilityInputs Input;

    public Sprite skillSprite { get { return _baseAbilityImage; } }
    public BasicUsibleTargetAreaViewer area { set { Area = value; } }

    //ссылка на кнопку и ее обладателя
    private struct ButtonPin
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
        Input.Enable();
        Area.LightUpTargetArea(player.transform.position, _range, _targetTag);
    }

    public virtual void Use()
    {

    }

    public void Abort()
    {
        Area.AbortAiming();
        Input.Disable();
    }

    private bool TryGetSubButton(AbilityController player, out ButtonPin OutButton)
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

    private void DeleteSubButton(ButtonPin button)
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

    public AbilityButton AddNewSubButton(AbilityController Player)
    {
        if (SubButtons.Count > 0 && TryGetSubButton(Player, out ButtonPin button))
            DeleteSubButton(button);

        AbilityButton NewButton = Instantiate(_baseAbilityButton);
        NewButton.ability = this;
        NewButton.player = Player;
        SubButtons.Add(new ButtonPin(NewButton, Player));

        return NewButton;
    }

    public AbilityButton AddNewCoreButton(AbilityController Player)
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
        Debug.Log("REAIM");
        Area.Reaim(NewTargetTile, _targetTag);
    }
}
