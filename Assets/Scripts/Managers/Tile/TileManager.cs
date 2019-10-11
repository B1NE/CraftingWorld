using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region JSON_DATA 저장용.
[Serializable]
public class TilePositions
{
    [SerializeField] public List<Vector2> pos = new List<Vector2>();
}

[Serializable]
public class TileMapJsonData : ISerializationCallbackReceiver
{
    [SerializeField] public List<int> keys = new List<int>();
    [SerializeField] public List<int> subKeys = new List<int>();
    [SerializeField] public List<TilePositions> posDatas = new List<TilePositions>();

    public MultiKeyDictionary<int, int, List<Vector2>> m_TileData = new MultiKeyDictionary<int, int, List<Vector2>>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        subKeys.Clear();
        posDatas.Clear();

        foreach (var data in m_TileData)
        {
            foreach(var subData in data.Value)
            {
                keys.Add(data.Key);
                subKeys.Add(subData.Key);

                TilePositions positions = new TilePositions();
                positions.pos.AddRange(subData.Value);
                posDatas.Add(positions);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        m_TileData.Clear();

        for (int i = 0; i < keys.Count; ++i)
        {
            if(m_TileData.ContainsKey(keys[i], subKeys[i]))
            {
                m_TileData[keys[i]][subKeys[i]] = new List<Vector2>(posDatas[i].pos);
            }
            else
            {
                m_TileData.Add(keys[i], subKeys[i], new List<Vector2>(posDatas[i].pos));
            }
        }
    }
}

#endregion

public class TileData
{
    public int X;
    public int Y;
    public int Zone_ID;
    public int Tile_ID;

    public TileData(int tile_id, int zone_id, int x, int y)
    {
        Tile_ID = tile_id;
        Zone_ID = zone_id;
        X = x;
        Y = y;
    }
}

public class TileManager : BaseManager
{
    [SerializeField] private Transform m_TileParent = null;
    [SerializeField] private Transform m_WallParent = null;

    private MultiKeyDictionary<int, int, List<TileData>> m_TilePosData = new MultiKeyDictionary<int, int, List<TileData>>();
    private Dictionary<int, List<TileData>> m_TileZoneData = new Dictionary<int, List<TileData>>();
    private Dictionary<int, List<TileData>> m_TileIdData = new Dictionary<int, List<TileData>>();

    private MultiKeyDictionary<int, int, bool> m_WallTile = new MultiKeyDictionary<int, int, bool>();
    private ObjectPool m_WallObjectPool = new ObjectPool();

    private Dictionary<int, GameObject> m_TilePrefabs = new Dictionary<int, GameObject>();
    private string m_TileSaveDataDirectoryPath = "";
    private string m_TileSaveDataFileName = "";

    private int m_ZoneCount = 0;

    public int GetZoneID(int x, int y)
    {
        if(m_TilePosData.TryGetValue(x, y, out var tileDatas))
        {
            return tileDatas[0].Zone_ID;
        }

        return -1;
    }

    public int GetZoneLevel(int zoneID)
    {
        if(m_TileZoneData.TryGetValue(zoneID, out var tileDatas))
        {
            return tileDatas.Count;
        }

        return 0;
    }

    public void Init()
    {
        m_TileSaveDataDirectoryPath = Application.persistentDataPath + "/data";
        m_TileSaveDataFileName = "tile.data";

        m_TilePosData.Clear();
        m_TileZoneData.Clear();
        m_TileIdData.Clear();
        m_WallTile.Clear();

        this.LoadTilePrefab(10000);
        this.LoadTilePrefab(10100);
        this.LoadTilePrefab(99999);

        m_WallObjectPool.Init(20, GetTilePrefab(99999), m_WallParent);

        LoadTileMap();
        CreateInitMap();
        InitCrafts();
    }

    private GameObject GetTilePrefab(int id)
    {
        if (m_TilePrefabs.TryGetValue(id, out var obj))
        {
            return obj;
        }

        Debug.LogError($"## GetTile Prefab Load Fail / {id}");

        return null;
    }

    private void LoadTilePrefab(int id)
    {
        m_TilePrefabs.Add(id, Resources.Load<GameObject>($"Tiles/{id}"));
    }

    private void SaveTileMap()
    {
        var filePath = Path.Combine(m_TileSaveDataDirectoryPath, m_TileSaveDataFileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else if (!Directory.Exists(m_TileSaveDataDirectoryPath))
        {
            Directory.CreateDirectory(m_TileSaveDataDirectoryPath);
        }

        TileMapJsonData jsonData = new TileMapJsonData();
        foreach (var data in m_TileIdData)
        {
            foreach(var tileData in data.Value)
            {
                if(!jsonData.m_TileData.ContainsKey(tileData.Tile_ID, tileData.Zone_ID))
                {
                    jsonData.m_TileData.Add(tileData.Tile_ID, tileData.Zone_ID, new List<Vector2>() { Vector2.zero.sXY(tileData.X, tileData.Y) });
                }
                else
                {
                    jsonData.m_TileData[tileData.Tile_ID, tileData.Zone_ID].Add(Vector2.zero.sXY(tileData.X, tileData.Y));
                }
            }
        }

        string jsonString = JsonUtility.ToJson(jsonData);

        try
        {
            File.WriteAllText(filePath, jsonString);
        }
        catch (IOException exception)
        {
            Debug.LogError(exception.Message);
        }
    }

    private void LoadTileMap()
    {
        var filePath = Path.Combine(m_TileSaveDataDirectoryPath, m_TileSaveDataFileName);

        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);

            var mapData = JsonUtility.FromJson<TileMapJsonData>(jsonString);

            for (int i = 0; i < mapData.keys.Count; ++i)
            {
                var tile_id = mapData.keys[i];
                var zone_id = mapData.subKeys[i];
                var pos_data = mapData.posDatas[i].pos;

                foreach (var pos in pos_data)
                {
                    TileData tile_data = new TileData(tile_id, zone_id, (int)pos.x, (int)pos.y);
                    m_ZoneCount = Mathf.Max(m_ZoneCount, zone_id + 1);
                    AddTileData(tile_data);
                }
            }
        }
        else
        {
            InitSaveTileMap();
            LoadTileMap();
        }
    }

    private void AddTileData(TileData tile_data)
    {
        var x = tile_data.X;
        var y = tile_data.Y;
        var tile_id = tile_data.Tile_ID;
        var zone_id = tile_data.Zone_ID;

        if (m_TilePosData.ContainsKey(x, y))
        {
            m_TilePosData[x, y].Add(tile_data);
        }
        else
        {
            m_TilePosData.Add(x, y, new List<TileData>() { tile_data });
        }

        if (m_TileZoneData.ContainsKey(zone_id))
        {
            m_TileZoneData[zone_id].Add(tile_data);
        }
        else
        {
            m_TileZoneData.Add(zone_id, new List<TileData>() { tile_data });
        }

        if (m_TileIdData.ContainsKey(tile_id))
        {
            m_TileIdData[tile_id].Add(tile_data);
        }
        else
        {
            m_TileIdData.Add(tile_id, new List<TileData>() { tile_data });
        }
    }

    private void CreateInitMap()
    {
        foreach (var data in m_TileIdData)
        {
            foreach (var tileData in data.Value)
            {
                MakeTile(tileData.X, tileData.Y, tileData.Tile_ID);
            }
        }

        CreateWall();
    }

    private void CreateWall()
    {
        m_WallObjectPool.AllClear();

        foreach(var data in m_TileIdData.Values)
        {
            foreach(var tileData in data)
            {
                if (m_WallTile.ContainsKey(tileData.X, tileData.Y))
                {
                    m_WallTile[tileData.X].Remove(tileData.Y);
                    if(m_WallTile[tileData.X].Count == 0)
                    {
                        m_WallTile.Remove(tileData.X);
                    }
                }

                if (!m_TilePosData.ContainsKey(tileData.X + 1, tileData.Y)) m_WallTile.Add(tileData.X + 1, tileData.Y, true);
                if (!m_TilePosData.ContainsKey(tileData.X - 1, tileData.Y)) m_WallTile.Add(tileData.X - 1, tileData.Y, true);
                if (!m_TilePosData.ContainsKey(tileData.X, tileData.Y + 1)) m_WallTile.Add(tileData.X, tileData.Y + 1, true);
                if (!m_TilePosData.ContainsKey(tileData.X, tileData.Y - 1)) m_WallTile.Add(tileData.X, tileData.Y - 1, true);
            }
        }

        foreach(var wall in m_WallTile)
        {
            var x = wall.Key;
            foreach(var wall_z in wall.Value)
            {
                var y = wall_z.Key;
                MakeWall(x, y);
            }
        }
    }

    private void InitSaveTileMap()
    {
        AddTileData(new TileData(10000, 0, 0, 0));
        AddTileData(new TileData(10000, 0, 0, 1));
        AddTileData(new TileData(10000, 0, 0, 2));
        AddTileData(new TileData(10000, 0, 1, 0));
        AddTileData(new TileData(10000, 0, 1, 1));
        AddTileData(new TileData(10000, 0, 1, 2));
        AddTileData(new TileData(10000, 0, 2, 0));
        AddTileData(new TileData(10000, 0, 2, 1));
        AddTileData(new TileData(10000, 0, 2, 2));

        AddTileData(new TileData(10100, 1, 3, 0));
        AddTileData(new TileData(10100, 1, 3, 1));
        AddTileData(new TileData(10100, 1, 3, 2));
        AddTileData(new TileData(10100, 1, 4, 0));
        AddTileData(new TileData(10100, 1, 4, 1));
        AddTileData(new TileData(10100, 1, 4, 2));
        AddTileData(new TileData(10100, 1, 5, 0));
        AddTileData(new TileData(10100, 1, 5, 1));
        AddTileData(new TileData(10100, 1, 5, 2));

        this.SaveTileMap();
    }

    private void InitCrafts()
    {
        foreach(var data in m_TileZoneData)
        {
            var zoneID = data.Key;
            var craftID = (zoneID == 0)? 500000 : 500100;
            var tiles = data.Value;
            int rnd = UnityEngine.Random.Range(0, tiles.Count);

            m_GameManager.CraftManager.AddCraft(tiles[rnd].X, tiles[rnd].Y, craftID, tiles[rnd].Zone_ID, 0.0f);
        }
    }

    private void InitMonsters()
    {
        foreach(var data in m_TileZoneData)
        {
            var zoneID = data.Key;
            if(zoneID == 1)
            {
                var monsterID = 900100;
                var tiles = data.Value;
                int rnd = UnityEngine.Random.Range(0, tiles.Count);

                //m_GameManager.MonsterManager.AddCraft(tiles[rnd].X, tiles)
            }
        }
    }

    public void MakeCrafts(int craftID, int zoneID)
    {
        if(m_TileZoneData.TryGetValue(zoneID, out var tileDatas))
        {
            int rnd = UnityEngine.Random.Range(0, tileDatas.Count);
            m_GameManager.CraftManager.AddCraft(tileDatas[rnd].X, tileDatas[rnd].Y, craftID, tileDatas[rnd].Zone_ID, UnityEngine.Random.Range(3.0f, 7.0f));
        }
    }

    private void MakeTile(int x, int y, int tile_id)
    {
        var prefab = GetTilePrefab(tile_id);
        if (prefab != null)
        {
            var tile = Instantiate(prefab, m_TileParent);
            tile.transform.position = Vector3.zero.sXZ(x, y);
            tile.name = $"[{x},{y}]";
        }
    }

    private void MakeWall(int x, int y)
    {
        var tile = m_WallObjectPool.GetNext<GameObject>();
        tile.SetActive(true);

        tile.transform.position = Vector3.zero.sXZ(x, y);
        tile.name = $"[{x},{y}]";
    }
}
