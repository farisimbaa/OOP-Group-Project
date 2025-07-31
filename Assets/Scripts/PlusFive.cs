public class PlusFive : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystemLevel6.Instance.IncreaseMultiplier(5);
    }
}