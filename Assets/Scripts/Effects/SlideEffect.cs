public class SlideEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        message.target.Move(message.target.CurrentDirection);
    }
}