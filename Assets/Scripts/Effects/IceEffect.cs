public class IceEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        message.target.Move(message.target.CurrentDirection);
    }
}