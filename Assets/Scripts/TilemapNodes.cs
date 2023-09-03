using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNodes : MonoBehaviour
{
    public Dictionary<Vector3Int, Node> PositionFromNode;

    private Tilemap Tilemap;

    void Awake()
    {
        Tilemap = GetComponent<Mapmanager>().Tilemap;

        PositionFromNode = new Dictionary<Vector3Int, Node>();

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

    public Node GetNodeOrNull(Vector3Int position)
    {
        Node returnNode;
        PositionFromNode.TryGetValue(position, out returnNode);

        return returnNode;
    }
}
