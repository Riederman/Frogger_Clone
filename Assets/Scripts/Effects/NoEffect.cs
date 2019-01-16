public class NoEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        message.target.NumTurnsToMove = 1;
    }
}