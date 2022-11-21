using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPulse : MonoBehaviour
{
    [SerializeField] AbilityButton _coreButton;
    [SerializeField] Sprite _skillSprite;

    private AbilityButton CoreButton;
    private AbilityButton SubButton;
    private Sprite SkillSprite;

    public Sprite skillSprite { get { return SkillSprite; } }

    // Start is called before the first frame update
    private void Start()
    {
        SkillSprite = _skillSprite;
        CoreButton = _coreButton;
    }

    public void DeleteSubButton()
    {
        Destroy(SubButton.gameObject);
        SubButton = null;
    }

    public void AddNewSubButton(AbilityButton NewButton)
    {
        if (SubButton != null)
            Destroy(SubButton.gameObject);
        SubButton = NewButton;
    }

    public void OnClick()
    {
        Debug.Log("*CLICK*");
    }
}
