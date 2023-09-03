using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDatas : MonoBehaviour
{
    public Dictionary<TileBase, TileData> DataFromTiles;

    [SerializeField]
    private List<TileData> m_TileDatas;
    private Tilemap Tilemap;

    void Awake()
    {
        DataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in m_TileDatas)
        {
            foreach (TileBase tileBase in tileData.Tiles)
            {
                DataFromTiles.Add(tileBase, tileData);
            }
        }

        Tilemap = GetComponent<Mapmanager>().Tilemap;
    }

    public bool IsWall(TileBase tileBase)
    {
        return DataFromTiles[tileBase].IsWall;
    }

    public bool IsWall(Vector3Int tilePosition)
    {
        TileBase tileBase = Tilemap.GetTile(tilePosition);
        return DataFromTiles[tileBase].IsWall;
    }
}
