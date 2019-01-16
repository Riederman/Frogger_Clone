using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class NodeMB : MonoBehaviour
{
    public Edge edgeLeft;
    public Edge edgeRight;
    public Edge edgeDown;
    public Edge edgeUp;

    private SpriteRenderer spriteRend;

    private EffectMessage message = new EffectMessage();
    private NoEffect noEffect = new NoEffect();
    private StallEffect stallEffect = new StallEffect();
    private SlideEffect slideEffect = new SlideEffect();
    private FallEffect fallEffect = new FallEffect();
    private DeathEffect deathEffect = new DeathEffect();

    public IEffect Effect { get; private set; }
    public NodeType Type { get; private set; }

    private void Awake()
    {
        // Get references
        spriteRend = GetComponent<SpriteRenderer>();
        Assert.IsTrue(spriteRend != null);
    }

    public void OnEnterNode(ActorMB actor)
    {
        // Called when an actor enters
        message.target = actor;
        Effect.ApplyEffect(message);
    }

    public NodeMB GetNextNode(DirectionType direction)
    {
        // Interpret the direction to return a navigable node
        if (direction == DirectionType.Left && edgeLeft.isNavigable)
        {
            return edgeLeft.nodeNext;
        }
        if (direction == DirectionType.Right && edgeRight.isNavigable)
        {
            return edgeRight.nodeNext;
        }
        if (direction == DirectionType.Down && edgeDown.isNavigable)
        {
            return edgeDown.nodeNext;
        }
        if (direction == DirectionType.Up && edgeUp.isNavigable)
        {
            return edgeUp.nodeNext;
        }

        return null;
    }

    public void SetType(NodeType type)
    {
        // Set variables
        Type = type;
        Effect = GetEffect(type);
    }

    public void SetSprite(Sprite sprite)
    {
        // Set node view
        spriteRend.sprite = sprite;
    }

    private IEffect GetEffect(NodeType type)
    {
        // Return an effect based on type
        switch (type)
        {
            case NodeType.Grass:
                return stallEffect;
            case NodeType.Ice:
                return slideEffect;
            case NodeType.Hole:
                return fallEffect;
            case NodeType.Lava:
                return deathEffect;
        }

        return noEffect;
    }
}