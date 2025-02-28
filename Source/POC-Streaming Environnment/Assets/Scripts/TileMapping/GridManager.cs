using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData
{
    public int width;
    public int height;
    public List<int> tiles = new();
}

public class GridManager : MonoBehaviour
{
    public GameObject[] tilePrefabs; // Array of different tile types
    public string mapFile = ""; // Path to the JSON file

    private MapData mapData;

    internal void Start()
    {
        Debug.Log($"🔵 Scene loaded: {SceneManager.GetActiveScene().name} [GridManager]");
        string filePath;
        if (mapFile == "")
        {
            filePath = Path.Combine(Application.streamingAssetsPath, $"Maps/Generated{SceneManager.GetActiveScene().name}.json");
        }
        else
        {
            filePath = Path.Combine(Application.streamingAssetsPath, mapFile);
        }

        LoadMap(filePath);

        if (mapData == null)
        {
            Debug.LogError("❌ mapData is NULL ! [GridManager]");
            return;
        }

        Debug.Log("✅ Map loaded successfully ! [GridManager]");
        GenerateGrid();
    }

    private void LoadMap(string filePath)
    {
        Debug.Log($"📂 File path: {filePath} [GridManager]");

        if (!File.Exists(filePath))
        {
            Debug.LogError($"❌ File not found: {filePath} [GridManager]");
            return;
        }

        string json = File.ReadAllText(filePath);
        Debug.Log($"📄 JSON loaded: {json} [GridManager]");

        mapData = JsonUtility.FromJson<MapData>(json);

        if (mapData == null)
        {
            Debug.LogError("❌ mapData is NULL after deserialization ! [GridManager]");
            return;
        }

        Debug.Log($"✅ mapData loaded: width={mapData.width}, height={mapData.height} [GridManager]");

        if (mapData.tiles == null || mapData.tiles.Count == 0)
        {
            Debug.LogError("❌ mapData.tiles is NULL or empty ! [GridManager]");
        }
        else
        {
            Debug.Log($"✅ mapData.tiles contains {mapData.tiles.Count} rows [GridManager]");
        }
    }

    private void GenerateGrid(string sceneName = "")
    {
        if (this == null)
        {
            Debug.LogError("❌ GridManager instance is NULL ! [GridManager]");
            return;
        }

        if (mapData == null)
        {
            Debug.LogError("❌ mapData is NULL ! [GridManager]");
            return;
        }

        if (tilePrefabs == null || tilePrefabs.Length == 0)
        {
            Debug.LogError("❌ tilePrefabs is NULL or empty ! [GridManager]");
            return;
        }

        if (mapData.tiles == null || mapData.tiles.Count == 0)
        {
            Debug.LogError("❌ mapData.tiles is NULL or empty ! [GridManager]");
            return;
        }

        for (int z = 0; z < mapData.height; z++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                int index = z * mapData.width + x;
                if (index < 0 || index >= mapData.tiles.Count)
                {
                    Debug.LogError($"❌ Invalid index {index} at position ({x}, {z}) ! [GridManager]");
                    continue;
                }

                int packedValue = mapData.tiles[index];
                int tileType = packedValue & 0b111; // Extract the tile type (lower 3 bits)
                int height = packedValue >> 3; // Extract the height (upper bits)

                if (tileType < 0 || tileType >= tilePrefabs.Length)
                {
                    Debug.LogError($"❌ Invalid tileType {tileType} at position ({x}, {z}) ! [GridManager]");
                    continue;
                }
                Vector3 position = new(x, height, z);

#if PRE_SCENE_TEST
                GameObject tile = Instantiate(tilePrefabs[tileType], position, Quaternion.identity, SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].transform);
#else
                GameObject tile = Instantiate(tilePrefabs[tileType], position, Quaternion.identity);
#endif

                tile.transform.parent = transform;
            }
        }
    }
}
