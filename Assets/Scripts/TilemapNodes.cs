using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNodes : MonoBehaviour
{
    public Dictionary<Vector3Int, Node> PositionFromNode = new Dictionary<Vector3Int, Node>();

    private Tilemap Tilemap;

    void Awake()
    {
        Tilemap = GetComponent<Mapmanager>().Tilemap;

        int minX = (int)Tilemap.localBounds.min.x;
        int minY = (int)Tilemap.localBounds.min.y;

        int maxX = (int)Tilemap.localBounds.max.x - 1;
        int maxY = (int)Tilemap.localBounds.max.y - 1;

        for (int x = minX; maxX >= x; ++x)
        {
            for (int y = minY; maxY >= y; ++y)
            {
                Node node = new Node(new Vector3Int(x, y));
                PositionFromNode.Add(node.Position, node);
            }
        }
    }

    public void ClearNodeData()
    {
        foreach(Node node in PositionFromNode.Values)
        {
            node.G = 0;
            node.H = 0;
            node.ParentNode = null;
        }

        /*
        foreach(KeyValuePair<Vector3Int, Node> keyValue in PositionFromNode)
        {
            keyValue.Value.G = 0;
            keyValue.Value.H = 0;
            keyValue.Value.ParentNode = null;
        }
        */
    }

    public Node GetNodeOrNull(Vector3Int position)
    {
        Node returnNode;
        PositionFromNode.TryGetValue(position, out returnNode);

        return returnNode;
    }
}
