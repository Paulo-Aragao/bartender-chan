using System;

public abstract class BartenderState
{
    public abstract void Handle(Bartender bartender,Action<string> onStateChange);
}