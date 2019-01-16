using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class NodeMB : MonoBehaviour
{
    [SerializeField]
    private NodeSpritePair[] nodeSprites;

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

    private Dictionary<NodeType, Sprite> spriteDict = new Dictionary<NodeType, Sprite>();

    public IEffect Effect { get; private set; }
    public NodeType Type { get; private set; }

    private void Awake()
    {
        // Get references
        spriteRend = GetComponent<SpriteRenderer>();
        Assert.IsTrue(spriteRend != null);

        // Populate sprite dictionary
        foreach (NodeSpritePair pair in nodeSprites)
        {
            spriteDict.Add(pair.type, pair.sprite);
        }
    }

    public void OnEnterNode(ActorMB actor)
    {
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

        // Set view sprite
        spriteRend.sprite = GetSprite(type);
    }

    private IEffect GetEffect(NodeType type)
    {
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

    private Sprite GetSprite(NodeType type)
    {
        Assert.IsTrue(spriteDict.ContainsKey(type));
        return spriteDict[type];
    }
}