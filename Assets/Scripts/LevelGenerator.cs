using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS.MapGeneration;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public int seed = 0;
    public float wallProbability = 0.45f;
    public int iterations = 5;

    private CelluarAutomata automata;
    private bool[,] map;

    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private Tilemap tilemap;


    private void InitializeMap()
    {
        map = new bool[width, height];
        Random.InitState(System.DateTime.Now.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = Random.value < wallProbability;
            }
        }
    }

    private void IterateAutomata()
    {
        automata = new CelluarAutomata();
        for (int i = 0; i < iterations; i++)
        {
            automata.Iterate(map);
        }
    }

    private void DrawMap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase tile = map[x, y] ? wallTile : floorTile;
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, tile);
            }
        }
    }


    void Start()
    {
        InitializeMap();
        IterateAutomata();
        DrawMap();
    }

}
