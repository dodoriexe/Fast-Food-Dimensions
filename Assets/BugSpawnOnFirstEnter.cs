using UnityEngine;

public class BugSpawnOnFirstEnter : MonoBehaviour
{
    bool isFirstTime = true;

    public BugSpawingScript bugSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFirstTime)
        {
            isFirstTime = false;

            for (int i = 0; i < 5; i++)
            {
                bugSpawner.spawnBug(bugSpawner.brownBugPrefab);
            }
            for (int i = 0; i < 5; i++)
            {
                bugSpawner.spawnBug(bugSpawner.greenBugPrefab);
            }
            for (int i = 0; i < 5; i++)
            {
                bugSpawner.spawnBug(bugSpawner.redBugPrefab);
            }
        }

    }
}
