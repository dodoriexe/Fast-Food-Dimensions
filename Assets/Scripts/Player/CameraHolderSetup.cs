using UnityEngine;

public class CameraHolderSetup : MonoBehaviour
{

    public Transform cameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;
    }
}