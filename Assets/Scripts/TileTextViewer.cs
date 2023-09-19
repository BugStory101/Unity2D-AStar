using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using TMPro;

public class TileTextViewer : MonoBehaviour
{
    public Dictionary<Vector3Int, TextMeshProUGUI> Texts = new Dictionary<Vector3Int, TextMeshProUGUI>();

    private const string TILEMAP_TEXT_PREFAB_PATH = "Assets/Prefabs/TileText.prefab";
    private const string POSITION_GHF_TEXT = "{0} \r\n" + 
                                         "<color=red>G: {1}</color> \r\n" +
                                         "<color=yellow>H: {2}</color> \r\n" +
                                         "<color=green>F: {3}</color>";

    private Tilemap Tilemap;
    private Canvas Canvas;
    

    void Awake()
    {
        Tilemap = GetComponent<Mapmanager>().Tilemap;        
        Canvas = FindAnyObjectByType<Canvas>();

        LoadPrefabs();
    }

    public void SetText(Vector3Int position, string message)
    {
        Texts[position].text = message;
    }

    public TextMeshProUGUI GetTextMeshOrNull(Vector3Int position)
    {
        TextMeshProUGUI text;

        Texts.TryGetValue(position, out text);

        return text;
    }

    public void SetTileCost(List<Node> nodeList)
    {
        if (nodeList == null)
        {
            return;
        }

        TextMeshProUGUI text;

        for (int i = 0; i < nodeList.Count; ++i)
        {
            text = GetTextMeshOrNull(nodeList[i].Position);

            if (text == null)
            {
                text.text = "Null";
                continue;
            }

            text.text = string.Format(POSITION_GHF_TEXT,
                nodeList[i].Position,
                nodeList[i].G, nodeList[i].H, nodeList[i].F);
        }
    }

    public void ClearText()
    {
        foreach (KeyValuePair<Vector3Int, TextMeshProUGUI> keyValue in Texts)
        {
            keyValue.Value.text = keyValue.Key.ToString();
        }
    }

    private void LoadPrefabs()
    {
        int minX = (int)Tilemap.localBounds.min.x;
        int minY = (int)Tilemap.localBounds.min.y;

        int maxX = (int)Tilemap.localBounds.max.x - 1;
        int maxY = (int)Tilemap.localBounds.max.y - 1;

        for (int x = minX; maxX >= x; x++)
        {
            for (int y = minY; maxY >= y; y++)
            {
                Vector3Int position = new Vector3Int(x, y);
                
                // Load Prefab.
                TextMeshProUGUI textPrefab = (TextMeshProUGUI)AssetDatabase.LoadAssetAtPath(TILEMAP_TEXT_PREFAB_PATH, typeof(TextMeshProUGUI));
                Vector3 centerPosition = Tilemap.GetCellCenterLocal(position);
                TextMeshProUGUI text = Instantiate(textPrefab, centerPosition, Quaternion.identity, Canvas.transform);
                text.text = position.ToString();

                Texts.Add(position, text);
            }
        }
    }
}
