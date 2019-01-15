using UnityEngine;
using UnityEngine.Assertions;

public class LevelMB: MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject playerPrefab;

    private NodeMB[,] nodes;

    private void Awake()
    {
        GenerateLevel();
    }

    #region Generation

    private void GenerateLevel()
    {
        // Create an array of nodes
        GenerateNodes();
        // Set the edges of each node
        GenerateEdges();
        // Adds the player to the level
        GeneratePlayer();
    }

    private void GenerateNodes()
    {
        Assert.IsTrue(width > 0);
        Assert.IsTrue(height > 0);

        // Create an array of nodes
        nodes = new NodeMB[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Instantiate node prefab
                NodeMB node = Instantiate(nodePrefab).GetComponent<NodeMB>();
                Assert.IsTrue(node != null);

                // Set node position
                node.transform.parent = transform;
                node.transform.position = x * Vector3.right + y * Vector3.up;

                // Add node to array
                node.name = "Node X " + x + " Y " + y;
                nodes[x, y] = node;
            }
        }
    }

    private void GenerateEdges()
    {
        // Set the edges of each node
        for (int x = 0; x < width; x++)
        {
            nodes[x, 0].edgeUp = new Edge(null, false);
            nodes[x, height - 1].edgeDown = new Edge(null, false);
        }

        for (int y = 0; y < height; y++)
        {
            nodes[0, y].edgeLeft = new Edge(null, false);
            nodes[width - 1, y].edgeRight = new Edge(null, false);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x > 0)
                {
                    nodes[x, y].edgeLeft = new Edge(nodes[x - 1, y], true);
                }
                if (x < width - 1)
                {
                    nodes[x, y].edgeRight = new Edge(nodes[x + 1, y], true);
                }
                if (y > 0)
                {
                    nodes[x, y].edgeDown = new Edge(nodes[x, y - 1], true);
                }
                if (y < height - 1)
                {
                    nodes[x, y].edgeUp = new Edge(nodes[x, y + 1], true);
                }
            }
        }
    }

    private void GeneratePlayer()
    {
        // Adds the player to the level
        PlayerMB player = Instantiate(playerPrefab).GetComponent<PlayerMB>();
        Assert.IsTrue(player != null);

        // Initializes the player
        NodeMB startingNode = nodes[0, 0];
        player.Initialize(startingNode);
    }

    #endregion

    public NodeMB GetNode(int x, int y)
    {
        // Returns a node by index
        Assert.IsTrue(x >= 0 && x < width);
        Assert.IsTrue(y >= 0 && y < height);
        return nodes[x, y];
    }
}