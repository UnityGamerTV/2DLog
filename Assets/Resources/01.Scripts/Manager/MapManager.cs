using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public struct PLAYER_POS
{
    public int tileY { get; set; }
    public int tileX { get; set; }
}

/// <summary>
/// 맵 생성 전용
/// </summary>
/// 
public partial class MapManager : Singleton<MapManager>, IManager
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(CameraManager))] private CameraManager cameraManager;

    public Grid currentGrid { get; private set; }
    private GameObject map;
    private Tilemap baseTilemap;
    private MapData mapData;

    // 
    private bool[,] isCollision;
    private PLAYER_POS playerPos;

    // 아이템
    bool[,] isItem;
    bool[,] isMonster;
    public void Init()
    {
        InjectUtil.InjectSingleton(this);
    }

    public void GenerateMap(string mapName, string mapNum)
    {
        map = CreateMap(mapName, mapNum);
        InitMap(map);
        mapData = ParsingJsonMap(mapName, mapNum);
        ParsingTextMap(mapName, mapNum, mapData);
        cameraManager.StartCamV1();
    }

    private GameObject CreateMap(string mapName, string mapNum)
    {
        //    //string userInput = "Map/Base2/Map_001";
        // 프리팹 이하 경로
        string path = $"Map/{mapName}/{mapNum}";
        return resourceManager.Instantiate(path);
    }

    private void InitMap(GameObject map)
    {
        // TODO
        currentGrid = map.GetComponent<Grid>();
        GameObject collisionMap = Util.FindChild(map, "CollisionMap", true);
        GameObject itemTile = Util.FindChild(map, "Item", true);
        GameObject monsterTile = Util.FindChild(map, "Monster", true);
        baseTilemap = Util.FindChild<Tilemap>(map, "BaseMap", true);

        if (collisionMap)
            collisionMap.SetActive(false);

        if (itemTile)
            itemTile.SetActive(false);

        if (monsterTile)
            monsterTile.SetActive(false);
    }

    private MapData ParsingJsonMap(string mapName, string mapNum)
    {
        // JSON 경로
        string jsonFilePath = $"Assets/Resources/Map/{mapName}/{mapNum}.json";

        // JSON Read
        string jsonContent = File.ReadAllText(jsonFilePath);

        // JSON 디시리얼라이즈
        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonContent);

        // 로그
        Debug.Log($"Map Name: {mapData.name}");
        Debug.Log($"Bounds: xMin={mapData.bounds.xMin}, xMax={mapData.bounds.xMax}, yMin={mapData.bounds.yMin}, yMax={mapData.bounds.yMax}");
        Debug.Log("Monster List:");
        foreach (var monster in mapData.monsterList)
        {
            LogUtil.Log(monster);
        }

        return mapData;
    }

    public void ParsingTextMap(string mapName, string mapNum, MapData mapData)
    {
        // Txt 파일 경로
        string textFilePath = $"Assets/Resources/Map/{mapName}/{mapNum}.txt";

        // Read Txt
        string[] lines = File.ReadAllLines(textFilePath);

        // xMin, xMax, yMin, yMax 
        string[] boundsLine = lines[0].Split(':');
        int xMin = int.Parse(boundsLine[1].Trim());
        int xMax = int.Parse(lines[1].Split(':')[1].Trim());
        int yMin = int.Parse(lines[2].Split(':')[1].Trim());
        int yMax = int.Parse(lines[3].Split(':')[1].Trim());

        // CollisionTiles
        List<string> collisionTiles = new List<string>();
        int row = 5;
        while (row < lines.Length && lines[row] != "itemTiles")
        {
            collisionTiles.Add(lines[row]);
            row++;
        }

        // itemTiles 
        List<string> itemTiles = new List<string>();
        row++;
        while (row < lines.Length && lines[row] != "monsterTiles")
        {
            itemTiles.Add(lines[row]);
            row++;
        }

        // monsterTiles
        List<string> monsterTiles = new List<string>();
        row++;
        while (row < lines.Length)
        {
            monsterTiles.Add(lines[row]);
            row++;
        }

        // 
        Debug.Log($"Bounds: xMin={xMin}, xMax={xMax}, yMin={yMin}, yMax={yMax}");
        Debug.Log("Collision Tiles:");

        // 보정
        int xCount = xMax - xMin + 1;
        int yCount = yMax - yMin + 1;

        isCollision = new bool[yCount, xCount];
        // Collision 
        // 플레이어 생성
        for (int y = 0; y < yCount; y++)
        {
            string line = collisionTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                if (line[x] == '1')
                    isCollision[flippedY, x] = true;
                else if (line[x] == '2')
                {
                    // 좌표 읽는 순서 방향 보정
                    int cellX = x + xMin;
                    int cellY = flippedY + yMin;

                    playerPos.tileX = cellX;
                    playerPos.tileY = cellY;

                    Vector3 worldPos = CellToWorld(cellX, cellY);
                    fieldManager.CreatePlayer(worldPos, map);

                    isCollision[flippedY, x] = false;
                }
                else
                    isCollision[flippedY, x] = false;
            }
        }

        isItem = new bool[itemTiles.Count, xCount];
        int itemIndex = 0;
        // 아이템 생성
        for (int y = 0; y < itemTiles.Count; y++)
        {
            string line = itemTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                // 좌표 읽는 순서 방향 보정
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                if (line[x] == '0')
                {
                    Vector3 worldPos = CellToWorld(cellX, cellY);
                    fieldManager.CreateItem(worldPos, map, mapData, itemIndex);
                    itemIndex++;
                }
            }
        }

        isMonster = new bool[monsterTiles.Count, xCount];
        int monsterIndex = 0;
        // 몬스터 생성
        for (int y = 0; y < monsterTiles.Count; y++)
        {
            string line = monsterTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                // 좌표 보정
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                if (line[x] == '0')
                {
                    // 월드 좌표로 변경
                    Vector3 worldPos = CellToWorld(cellX, cellY);
                    fieldManager.CreateMonster(worldPos, map, mapData, monsterIndex);
                    monsterIndex++;
                }
            }
        }
    }

    // 셀 좌표 → 월드 좌표 중심으로 보정해서 반환
    public Vector3 CellToWorld(int cellX, int cellY)
    {
        // 비트맵 셀 월드 변환
        return baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0)) + baseTilemap.cellSize / 2f;
    }

    // 월드 좌표 → 셀 좌표
    public Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return baseTilemap.WorldToCell(worldPosition);
    }

    public void Release()
    {
        
    }
}

