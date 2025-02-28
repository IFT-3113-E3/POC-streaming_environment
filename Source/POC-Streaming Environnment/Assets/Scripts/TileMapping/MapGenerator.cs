using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MapGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;  // Array of tile prefabs for different types
    public int width = 5;             // Width of the map
    public int height = 5;            // Height of the map
    public float scale = 0.1f;        // Scale of Perlin Noise (affects frequency)
    public int heightRange = 8;       // Max height value
    public string sceneName = "";    // Name of the map
    private MapData mapData;

    internal void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"🔵 Scene loaded: {scene.name} [MapGenerator]");

        string filePath = Path.Combine(Application.streamingAssetsPath, $"Maps/Generated{sceneName}.json");
        if (File.Exists(filePath))
        {
            Debug.Log($"[MapGenerator] Map file already exists at {filePath}, skipping generation.");
            return;
        }

        GenerateMap();

        if (mapData == null)
        {
            Debug.LogError("❌ mapData is NULL ! [MapGenerator]");
            return;
        }

        Debug.Log("✅ Map generated successfully ! [MapGenerator]");
        ExportMapToJSON(scene.name);
    }

    private void GenerateMap()
    {
        mapData = new MapData
        {
            width = width,
            height = height,
            tiles = new List<int>(new int[width * height])
        };

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                // Generate height using Perlin Noise
                float xCoord = x * scale;
                float zCoord = z * scale;
                float perlinValue = Mathf.PerlinNoise(xCoord, zCoord); // Generate height (0 to 1)

                // Convert Perlin Noise value to height range (0 to heightRange)
                int heightValue = Mathf.FloorToInt(perlinValue * heightRange);

                // Pack tile type (randomly choosing) and height
                int tileType = Random.Range(0, tilePrefabs.Length);  // Random tile type
                int packedValue = (heightValue << 3) | (tileType & 0b111); // Pack height and tile type

                // Set the packed value in the array
                mapData.tiles[z * width + x] = packedValue;

                // Visualize the tile at the generated position
                Vector3 position = new(x, heightValue, z);
                Instantiate(tilePrefabs[tileType], position, Quaternion.identity);
            }
        }
        Debug.Log("Map generated successfully! [MapGenerator]");
    }

    private void ExportMapToJSON(string sceneName)
    {
        string json = JsonUtility.ToJson(mapData, true);
        string filePath = Path.Combine(Application.streamingAssetsPath, $"Maps/Generated{sceneName}.json");
        File.WriteAllText(filePath, json);
        Debug.Log($"[MapGenerator] Map exported to {filePath}");
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
