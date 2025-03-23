public class IdleState : BartenderState
{
    public override void Handle(Bartender bartender)
    {
        bartender.SetAnimation("Idle");
    }
}