using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class NodeMB : MonoBehaviour
{
    public Edge edgeLeft;
    public Edge edgeRight;
    public Edge edgeDown;
    public Edge edgeUp;
    public IEffect effect;
    public NodeType type;

    private SpriteRenderer spriteRend;

    public Sprite NodeSprite
    {
        get { return spriteRend.sprite; }
        set { spriteRend.sprite = value; }
    }

    private EffectMessage message = new EffectMessage();

    private void Awake()
    {
        // Get references
        spriteRend = GetComponent<SpriteRenderer>();
        Assert.IsTrue(spriteRend != null);
    }

    public void OnEnterNode(ActorMB actor)
    {
        // Called when an actor enters
        Assert.IsTrue(effect != null);
        message.target = actor;
        effect.ApplyEffect(message);
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
}