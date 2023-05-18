using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillingState : PlayerState
{
    private AbilityButton AbilityButton;

    public AbilityButton abilityButton { set { AbilityButton = value; AbilityButton.isAiming = true; } }

    public PlayerSkillingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override IEnumerator Start()
    {
        return base.Start();
    }

    public override IEnumerator OnLeftMouseDown()
    {
        if (AbilityButton.isAiming)
        {
            return AbilityButton.TryToUseAbility();
        }
        else
            return base.OnLeftMouseDown();
    }

    public override IEnumerator OnRightMouseDown()
    {
        AbilityButton.AbortAbility();
        return base.OnRightMouseDown();
    }

}
