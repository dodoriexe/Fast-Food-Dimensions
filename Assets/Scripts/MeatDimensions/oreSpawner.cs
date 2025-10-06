using UnityEngine;
using System.Collections;

public class OreSpawner : MonoBehaviour
{
    [Header("Ore Settings")]
    public GameObject[] orePrefabs;

    [Header("Spawn Settings")]
    public Transform[] oreSpawnLocations;
    public float minRespawnTime = 3f;
    public float maxRespawnTime = 8f;

    private GameObject[] currentOres;

    void Start()
    {
        currentOres = new GameObject[oreSpawnLocations.Length];

        for (int i = 0; i < oreSpawnLocations.Length; i++)
        {
            SpawnOreAt(i);
        }

        StartCoroutine(CheckForRespawn());
    }

    void SpawnOreAt(int index)
    {
        if (orePrefabs.Length == 0) return;
        if (oreSpawnLocations[index] == null) return;

        int randomOre = Random.Range(0, orePrefabs.Length);
        GameObject orePrefab = orePrefabs[randomOre];

        GameObject ore = Instantiate(orePrefab, oreSpawnLocations[index].position, oreSpawnLocations[index].rotation);

        ore.transform.SetParent(oreSpawnLocations[index].parent);

        currentOres[index] = ore;
    }

    IEnumerator CheckForRespawn()
    {
        while (true)
        {
            for (int i = 0; i < currentOres.Length; i++)
            {
                if (currentOres[i] == null)
                {
                    float waitTime = Random.Range(minRespawnTime, maxRespawnTime);
                    yield return new WaitForSeconds(waitTime);

                    if (currentOres[i] == null)
                        SpawnOreAt(i);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
