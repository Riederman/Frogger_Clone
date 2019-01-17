public class WinEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        GameManagerMB.Instance.ProceedToWinScreen();
    }
}