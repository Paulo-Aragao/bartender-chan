
using System;

public class FailedState : BartenderState
{
    public override void Handle(Bartender bartender,Action<string> onStateChange)
    {
        bartender.SetAnimation("Failed");
        onStateChange?.Invoke("Failed");
    }
}
