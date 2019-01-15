using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMB : MonoBehaviour
{
    private NodeMB currentNode;

    public void Initialize(NodeMB currentNode)
    {
        // Initialize references
        this.currentNode = currentNode;
        Assert.IsTrue(currentNode != null);

        // Sets the starting position
        transform.position = currentNode.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(DirectionType.Left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(DirectionType.Right);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(DirectionType.Down);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(DirectionType.Up);
        }
    }

    public void Move(DirectionType direction)
    {
        // Get the node to move to
        NodeMB targetNode = currentNode.GetNextNode(direction);

        // Set the new position
        if (targetNode != null)
        {
            currentNode = targetNode;
            transform.position = targetNode.transform.position;
        }
    }
}