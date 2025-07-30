public class PlusFive : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystem.Instance.IncreaseMultiplier(5);
    }
}