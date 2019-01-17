using UnityEngine;
using UnityEngine.Assertions;

public abstract class ActorMB : MonoBehaviour
{
    protected NodeMB currentNode;

    private int numTurnsCounter;

    public int NumTurnsToMove { get; set; }
    public DirectionType CurrentDirection { get; private set; }

    public void Initialize(NodeMB currentNode)
    {
        // Initialize references
        this.currentNode = currentNode;
        Assert.IsTrue(currentNode != null);

        // Initialize variables
        NumTurnsToMove = 1;
        transform.position = currentNode.transform.position;

        Debug.Log("Initialize " + name);
    }

    public virtual void Move(DirectionType direction)
    {
        // Get the node to move to
        NodeMB targetNode = currentNode.GetNextNode(direction);

        if (targetNode != null)
        {
            // Tick up counter
            numTurnsCounter++;

            if (numTurnsCounter == NumTurnsToMove)
            {
                numTurnsCounter = 0;
                currentNode = targetNode;
                currentNode.OnEnterNode(this);
                transform.position = targetNode.transform.position;
            }
        }

        // Set facing direction
        CurrentDirection = direction;
    }
}