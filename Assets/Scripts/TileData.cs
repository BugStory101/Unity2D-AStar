using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] Tiles;

    [SerializeField]
    private bool m_IsWall;
    public bool IsWall
    {
        get
        {
            return m_IsWall;
        }
    }
}
