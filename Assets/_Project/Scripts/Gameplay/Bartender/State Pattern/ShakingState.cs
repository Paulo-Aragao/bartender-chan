public class ShakingState : BartenderState
{
    public override void Handle(Bartender bartender)
    {
        bartender.SetAnimation("Shaking");
    }
}