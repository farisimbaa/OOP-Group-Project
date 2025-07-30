public class MultPointFive : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystem.Instance.ApplyScoreMultiplier(0.5f);
    }
}