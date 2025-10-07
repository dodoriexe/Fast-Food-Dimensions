using UnityEngine;

public class BugSpawingScript : MonoBehaviour
{
    public Transform minRandSpawn;
    public Transform maxRandSpawn;

    public GameObject greenBugPrefab;
    public GameObject redBugPrefab;
    public GameObject brownBugPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBug(GameObject bugtype)
    {
        Vector3 randomPosition = new Vector3(Random.Range(minRandSpawn.position.x, maxRandSpawn.position.x), maxRandSpawn.position.y, Random.Range(minRandSpawn.position.z, maxRandSpawn.position.z));
        GameObject bug = Instantiate(bugtype, randomPosition, Quaternion.identity);

        if (bugtype == brownBugPrefab)
            bug.tag = "Brown Bug";
        else if (bugtype == greenBugPrefab)
            bug.tag = "Green Bug";
        else if (bugtype == redBugPrefab)
            bug.tag = "Red Bug";
    }
}
