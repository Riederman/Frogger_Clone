using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpriteManagerMB : Singleton<SpriteManagerMB>
{
    [SerializeField]
    private NodeSpritePair[] nodeSprites;

    private static Dictionary<NodeType, Sprite> nodeDict = new Dictionary<NodeType, Sprite>();


    private void Awake()
    {
        // Populate sprite dictionary
        foreach (NodeSpritePair pair in nodeSprites)
        {
            nodeDict.Add(pair.type, pair.sprite);
        }
    }

    public static Sprite GetNodeSprite(NodeType type)
    {
        // Return a sprite by type
        Assert.IsTrue(nodeDict.ContainsKey(type));
        return nodeDict[type];
    }
}