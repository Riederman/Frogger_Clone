public class DeathEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        message.target.gameObject.SetActive(false);
    }
}