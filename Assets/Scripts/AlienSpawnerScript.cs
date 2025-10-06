using System.Collections.Generic;
using UnityEngine;

public class AlienSpawnerScript : MonoBehaviour
{
    public GameObject alienPrefab;
    public float detectionDistance;
    public int maxSpawns;
    [Range(5f, 30f)]
    public float minSpawnTime;
    [Range(5f, 30f)]
    public float maxSpawnTime;
    
    private GameManager _gameManager;
    private GameObject _player;
    
    private List<GameObject> _spawns = new List<GameObject>();
    private bool _canSpawn;
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = _gameManager.player;

        StartCoroutine(StartTimer());
    }
    
    void FixedUpdate()
    {
        var isSpawnable = Vector3.Distance(_player.transform.position, transform.position) < detectionDistance &&
                    _spawns.Count < maxSpawns;
        
        _spawns.RemoveAll(go => go == null);

        if (!_canSpawn || !isSpawnable) return;
        
        SpawnAlien();
        _canSpawn = false;
        StartCoroutine(StartTimer());
    }

    private System.Collections.IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        _canSpawn = true;
    }
    
    void SpawnAlien()
    {
        _spawns.Add(Instantiate(alienPrefab, transform.position, Quaternion.identity));
    }
}
