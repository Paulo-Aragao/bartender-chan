public class SuccessState : BartenderState
{
    public override void Handle(Bartender bartender)
    {
        bartender.SetAnimation("Success");
    }
}
