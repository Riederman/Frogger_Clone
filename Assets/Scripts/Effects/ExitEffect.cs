public class ExitEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        LevelManagerMB.Instance.ReturnToPrevLevel();
    }
}