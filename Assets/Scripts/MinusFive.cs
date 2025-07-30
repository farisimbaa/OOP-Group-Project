public class MinusFive : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystem.Instance.AddFlatScore(-5);
    }
}