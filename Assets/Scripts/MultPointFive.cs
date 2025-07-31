public class MultPointFive : ScoreMultiplierPickup
{
    protected override void ApplyEffect()
    {
        ScoreSystemLevel6.Instance.ApplyScoreMultiplier(0.5f);
    }
}