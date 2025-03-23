
public class FailedState : BartenderState
{
    public override void Handle(Bartender bartender)
    {
        bartender.SetAnimation("Failed");
    }
}
