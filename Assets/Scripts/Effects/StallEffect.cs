﻿public class StallEffect : IEffect
{
    public void ApplyEffect(EffectMessage message)
    {
        message.target.NumTurnsToMove = 2;
    }
}