public class EnterEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        LevelManagerMB.Instance.ReturnToPrevLevel();
    }
}