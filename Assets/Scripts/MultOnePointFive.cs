public class MultOnePointFivePickup : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystem.Instance.ApplyScoreMultiplier(1.5f);
    }
}