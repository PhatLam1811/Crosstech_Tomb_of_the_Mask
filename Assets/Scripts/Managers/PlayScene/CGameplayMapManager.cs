using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CGameplayMapManager : MonoSingleton<CGameplayMapManager>
{
    public Grid _grid;

    public List<Tilemap> _playMaps;
    private Dictionary<int, Tilemap> _dictionaryPlayMaps;

    public GameObject prefab_player;
    
    public GameObject prefab_gate;
    public GameObject prefab_exit;

    public GameObject prefab_dotGame;
    public GameObject prefab_star;
    public GameObject prefab_coin;

    public GameObject prefab_spike;

    private int mapTotalDots = 0;
    private int mapTotalCoins = 0;

    private void Start()
    {
        this.SetUpDictionary();
        this.LoadMap(CGameplayManager.Instance.GetOnPlayingMapId());
    }

    private void SetUpDictionary()
    {
        this._dictionaryPlayMaps = new Dictionary<int, Tilemap>();

        if (this._playMaps != null)
        {
            for (int i = 0; i < this._playMaps.Count; i++)
            {
                this._dictionaryPlayMaps.Add(i + 1, this._playMaps[i]);
            }
        }
        else
        {
            Debug.LogError("No play map found!");
        }
    }

    private void LoadMap(int mapId)
    {
        if (!this._dictionaryPlayMaps.ContainsKey(mapId))
        {
            Debug.LogError("Not found map id " + mapId);
            return;
        }

        this._dictionaryPlayMaps[mapId].gameObject.SetActive(true);

        CMapConfig mapConfig = CMapConfigs.Instance.GetMapConfig(mapId);

        this.LoadCollectableObjects(mapConfig);
        this.LoadTrapObjects(mapConfig);
        this.LoadGate(mapConfig);
        this.LoadExit(mapConfig);

        CPlayer player = this.LoadPlayer(mapConfig);

        CGameplayManager.Instance.StartGame(player);
    }

    private void LoadGate(CMapConfig mapConfig)
    {
        Vector3 gateWorldPos = this._grid.CellToWorld(mapConfig._playerStartPosition);
        Instantiate(this.prefab_gate, gateWorldPos, Quaternion.identity);
    }

    private void LoadExit(CMapConfig mapConfig)
    {
        Vector3 exitWorldPos = this._grid.CellToWorld(mapConfig._exitPosition);
        Instantiate(this.prefab_exit, exitWorldPos, Quaternion.identity);
    }

    private CPlayer LoadPlayer(CMapConfig mapConfig)
    {
        Vector3 playerStartWorldPos = this._grid.CellToWorld(mapConfig._playerStartPosition);
        return Instantiate(this.prefab_player, playerStartWorldPos, Quaternion.identity).GetComponent<CPlayer>();
    }

    private void LoadCollectableObjects(CMapConfig mapConfig)
    {
        foreach (CCollectableObjectPositionConfig config in mapConfig._collectableObjectConfigs)
        {
            Vector3Int cellPos = config.position;

            Vector3 worldPos = _grid.CellToWorld(cellPos);

            switch (config._id)
            {
                case MapCollectableType.GAME_DOTS:
                    int min = 1;
                    int max = 10;
                    if (this.mapTotalCoins * 10 >= mapConfig._collectableObjectConfigs.Count || Random.Range(min, max) != 1)
                    {
                        Instantiate(this.prefab_dotGame, worldPos, Quaternion.identity);
                        this.mapTotalDots++;
                    }
                    else
                    {
                        Instantiate(this.prefab_coin, worldPos, Quaternion.identity);
                        this.mapTotalCoins++;
                    }
                    break;
                case MapCollectableType.STAR:
                    Instantiate(this.prefab_star, worldPos, Quaternion.identity); break;
            }
        }
    }

    private void LoadTrapObjects(CMapConfig mapConfig)
    {

    }

    public int GetMapTotalDots()
    {
        return this.mapTotalDots;
    }
}
