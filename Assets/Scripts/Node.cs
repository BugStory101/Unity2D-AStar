using UnityEngine;

public class Node
{
    public int G;
    public int H;
    public int F
    {
        get
        {
            return G + H;
        }
    }

    public Vector3Int Position;
    public Node ParentNode;

    public Node(Vector3Int position)
    {
        Position = position;
    }
}
