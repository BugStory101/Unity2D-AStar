using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDatas
{
    public Dictionary<TileBase, TileData> DataFromTiles;

    [SerializeField]
    private List<TileData> m_TileDatas;


    public TilemapDatas()
    {
        DataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in m_TileDatas)
        {
            foreach (TileBase tileBase in tileData.Tiles)
            {
                DataFromTiles.Add(tileBase, tileData);
            }
        }
    }

    public bool IsWall(TileBase tileBase)
    {
        return DataFromTiles[tileBase].IsWall;
    }




}
