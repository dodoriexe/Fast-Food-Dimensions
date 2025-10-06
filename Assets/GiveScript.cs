using UnityEngine;

public class GiveScript : MonoBehaviour
{
    float brownamount;
    float redamount;
    float greenamount;

    public GameObject tomatoPrefab;
    public GameObject lettucePrefab;
    public GameObject picklePrefab;

    public TMPro.TextMeshPro redBugCountText;
    public TMPro.TextMeshPro greenBugCountText;
    public TMPro.TextMeshPro brownBugCountText;

    public Transform outputPoint;

    public BugSpawingScript bugSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brownamount = 0;
        redamount = 0;
        greenamount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeText()
    {
        redBugCountText.text = (3 - redamount).ToString();
        greenBugCountText.text = (3 - greenamount).ToString();
        brownBugCountText.text = (3 - brownamount).ToString();
    }

    void output()
    {
        if (brownamount >= 3)
        {
            Instantiate(picklePrefab, outputPoint.position, Quaternion.identity);
            brownamount = 0;
        }
        if (redamount >= 3)
        {
            Instantiate(tomatoPrefab, outputPoint.position, Quaternion.identity);
            redamount = 0;
        }
        if (greenamount >= 3)
        {
            Instantiate(lettucePrefab, outputPoint.position, Quaternion.identity);
            greenamount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>())
        {
            if (other.CompareTag("Brown Bug"))
            {
                Destroy(other.gameObject);
                brownamount++;
                bugSpawner.spawnBug(bugSpawner.brownBugPrefab);
            }
            if (other.CompareTag("Green Bug"))
            {
                Destroy(other.gameObject);
                greenamount++;
                bugSpawner.spawnBug(bugSpawner.greenBugPrefab);
            }
            if (other.CompareTag("Red Bug"))
            {
                Destroy(other.gameObject);
                redamount++;
                bugSpawner.spawnBug(bugSpawner.redBugPrefab);
            }
        }
        changeText();
        output();
    }
}
