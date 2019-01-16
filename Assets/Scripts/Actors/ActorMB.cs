using UnityEngine;
using UnityEngine.Assertions;

public abstract class ActorMB : MonoBehaviour
{
    protected NodeMB currentNode;

    public DirectionType CurrentDirection { get; private set; }

    public void Initialize(NodeMB currentNode)
    {
        // Initialize references
        this.currentNode = currentNode;
        Assert.IsTrue(currentNode != null);

        // Set the starting position
        transform.position = currentNode.transform.position;

        Debug.Log("Initialize " + name);
    }

    public virtual void Move(DirectionType direction)
    {
        // Get the node to move to
        NodeMB targetNode = currentNode.GetNextNode(direction);

        // Set the new position
        if (targetNode != null)
        {
            currentNode = targetNode;
            currentNode.OnEnterNode(this);
            transform.position = targetNode.transform.position;
        }

        // Set facing direction
        CurrentDirection = direction;
    }
}