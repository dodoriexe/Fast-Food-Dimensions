using UnityEngine;

public class ClosingScript : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public Vector3 ClosedPositionLeft;
    public Vector3 ClosedPositionRight;

    public Vector3 OpenPositionLeft;
    public Vector3 OpenPositionRight;

    public float slideDuration = 1.0f;

    bool opened = true;
    Coroutine slidingCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClosedPositionLeft = new Vector3(0f, 3.8f, 0f);
        ClosedPositionRight = new Vector3(0f, 3.8f, 0f);

        OpenPositionLeft = new Vector3(0f, 3.8f, -1.5f);
        OpenPositionRight = new Vector3(0f, 3.8f, 1.5f);
    }

    public void Open()
    {
        if (slidingCoroutine != null) StopCoroutine(slidingCoroutine);
        slidingCoroutine = StartCoroutine(SlideDoors(leftDoor, rightDoor, leftDoor.localPosition, rightDoor.localPosition, OpenPositionLeft, OpenPositionRight));
        opened = true;
    }

    public void Close()
    {
        if (slidingCoroutine != null) StopCoroutine(slidingCoroutine);
        slidingCoroutine = StartCoroutine(SlideDoors(leftDoor, rightDoor, leftDoor.localPosition, rightDoor.localPosition, ClosedPositionLeft, ClosedPositionRight));
        opened = false;
    }

    System.Collections.IEnumerator SlideDoors(Transform left, Transform right, Vector3 startLeft, Vector3 startRight, Vector3 endLeft, Vector3 endRight)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            left.localPosition = Vector3.Lerp(startLeft, endLeft, t);
            right.localPosition = Vector3.Lerp(startRight, endRight, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        left.localPosition = endLeft;
        right.localPosition = endRight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
