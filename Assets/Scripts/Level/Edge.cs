public struct Edge
{
    public NodeMB nodeNext;
    public bool isNavigable;

    public Edge(NodeMB nodeNext, bool isNavigable)
    {
        this.nodeNext = nodeNext;
        this.isNavigable = isNavigable;
    }
}