using System;

public class IdleState : BartenderState
{
    public override void Handle(Bartender bartender, Action<string> onStateChange)
    {
        bartender.SetAnimation("Idle");
        onStateChange?.Invoke("Idle");
    }
}