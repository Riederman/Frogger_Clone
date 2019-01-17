public class DeathEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        LevelManagerMB.Instance.RepeatCurrentLevel();
    }
}