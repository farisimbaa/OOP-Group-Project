public class MultOnePointFivePickup : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystemLevel6.Instance.ApplyScoreMultiplier(1.5f);
    }
}