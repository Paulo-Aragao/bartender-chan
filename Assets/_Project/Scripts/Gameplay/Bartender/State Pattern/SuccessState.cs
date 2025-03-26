using System;

public class SuccessState : BartenderState
{
    public override void Handle(Bartender bartender, Action<string> onStateChange)
    {
        bartender.SetAnimation("Success");
        onStateChange?.Invoke("Success");
    }
}
