using System;

public class ShakingState : BartenderState
{
    public override void Handle(Bartender bartender,Action<string> onStateChange)
    {
        bartender.SetAnimation("Shaking");
        onStateChange?.Invoke("Shaking");
    }
}