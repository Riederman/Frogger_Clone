using UnityEngine;

public class NodeMB : MonoBehaviour
{
    public Edge edgeLeft;
    public Edge edgeRight;
    public Edge edgeDown;
    public Edge edgeUp;
    public IEffect effect;

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