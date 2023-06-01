using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CGameplayMapManager : MonoSingleton<CGameplayMapManager>
{
    public Grid _grid;

    public List<Tilemap> _playMaps;
    private Dictionary<int, Tilemap> _dictionaryPlayMaps;

    public CCamera _camera;

    public GameObject prefab_player;
    
    public GameObject prefab_gate;
    public GameObject prefab_exit;

    public GameObject prefab_dotGame;
    public GameObject prefab_star;
    public GameObject prefab_coin;

    public GameObject prefab_rising_sea;
    public GameObject prefab_spike_m;

    private int mapTotalDots = 0;
    private int mapTotalCoins = 0;

    private float countdown = 0.0f;

    private bool _isMapLoaded = false;

    private const float DELAY = 0.35f;

    private void Start()
    {
        Camera.main.fieldOfView = 200f;
        this.SetUpDictionary();
    }

    private void Update()
    {
        if (!this._isMapLoaded && this.countdown >= DELAY)
        {
            this._isMapLoaded = true;
            this.LoadMap(CGameplayManager.Instance.GetOnPlayingMapId());
        }
        
        if (!this._isMapLoaded)
        {
            this.countdown += Time.deltaTime;
        }
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

        this._camera.GetPlayerPos();
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

            Vector3 worldPos = this._grid.CellToWorld(cellPos);

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
        foreach (CTrapObjectConfig config in mapConfig._trapObjectConfigs)
        {
            Vector3Int cellPos = config.position;
            Vector3 worldPos = this._grid.CellToWorld(cellPos);

            switch (config._id)
            {
                case MapTrapType.RISING_SEA:
                    Instantiate(this.prefab_rising_sea, worldPos, Quaternion.identity); break;
                case MapTrapType.SPIKE_M_U:
                case MapTrapType.SPIKE_M_L:
                case MapTrapType.SPIKE_M_D:
                case MapTrapType.SPIKE_M_R:
                    CSpikeM spikeM = Instantiate(this.prefab_spike_m, worldPos, Quaternion.identity).GetComponent<CSpikeM>();
                    spikeM.SetUpSpikeDirection(config._id);
                    break;
            }
        }
    }

    public int GetMapTotalDots()
    {
        return this.mapTotalDots;
    }
}
