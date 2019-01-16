using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelMB: MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject hazardPrefab;
    [SerializeField] private NodeSpritePair[] nodeSprites;

    private NodeMB[,] nodes;

    private Dictionary<NodeType, Sprite> spriteDict = new Dictionary<NodeType, Sprite>();

    private void Awake()
    {
        GenerateLevel();

        // Populate sprite dictionary
        foreach (NodeSpritePair pair in nodeSprites)
        {
            spriteDict.Add(pair.type, pair.sprite);
        }
    }

    #region Generation

    private void GenerateLevel()
    {
        Debug.Log("Generate Level");

        // Create an array of nodes
        GenerateNodes();

        // Set the edges of each node
        GenerateEdges();

        // Add the player to the level
        GeneratePlayer();

        // Add hazards to the level
        GenerateHazards();
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
                NodeMB node = CreateNode(NodeType.Floor);

                // Set node position
                node.transform.parent = transform;
                node.transform.position = x * Vector3.right + y * Vector3.up;

                // Add node to array
                node.name = "Node X " + x + " Y " + y;
                nodes[x, y] = node;
            }
        }
    }

    private NodeMB CreateNode(NodeType type)
    {
        // Instantiate node prefab
        NodeMB node = Instantiate(nodePrefab).GetComponent<NodeMB>();
        Assert.IsTrue(node != null);

        // Set the node variables
        node.SetType(type);
        node.SetSprite(GetNodeSprite(type));

        return node;
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
        // Add the player to the level
        PlayerMB player = Instantiate(playerPrefab).GetComponent<PlayerMB>();
        Assert.IsTrue(player != null);

        // Initialize the player
        NodeMB startingNode = nodes[0, 0];
        player.Initialize(startingNode);
    }

    private void GenerateHazards()
    {
        //// Add hazards to the level
        //HazardMB hazard = Instantiate(hazardPrefab).GetComponent<HazardMB>();
        //Assert.IsTrue(hazard != null);

        //// Initialize the hazards
        //NodeMB startingNode = nodes[Random.Range(1, width - 1), Random.Range(1, height - 1)];
        //hazard.Initialize(startingNode);
    }

    #endregion

    public NodeMB GetNode(int x, int y)
    {
        // Return a node by index
        Assert.IsTrue(x >= 0 && x < width);
        Assert.IsTrue(y >= 0 && y < height);
        return nodes[x, y];
    }

    private Sprite GetNodeSprite(NodeType type)
    {
        Assert.IsTrue(spriteDict.ContainsKey(type));
        return spriteDict[type];
    }
}