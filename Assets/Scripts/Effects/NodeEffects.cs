public static class NodeEffects
{
    private static NoEffect noEffect = new NoEffect();
    private static EnterEffect enterEffect = new EnterEffect();
    private static ExitEffect exitEffect = new ExitEffect();
    private static StallEffect stallEffect = new StallEffect();
    private static SlideEffect slideEffect = new SlideEffect();
    private static DeathEffect deathEffect = new DeathEffect();

    public static IEffect GetEffect(NodeType type)
    {
        // Return an effect by type
        switch (type)
        {
            case NodeType.Enter:
                return enterEffect;
            case NodeType.Exit:
                return exitEffect;
            case NodeType.Grass:
                return stallEffect;
            case NodeType.Ice:
                return slideEffect;
            case NodeType.Lava:
                return deathEffect;
        }

        return noEffect;
    }
}