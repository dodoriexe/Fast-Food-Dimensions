using UnityEngine;

public class PortalSetup : MonoBehaviour
{
    public Camera cameraA;
    public Material cameraMatA;

    public Camera cameraB;
    public Material cameraMatB;

    public Camera cameraC;
    public Material cameraMatC;

    public Camera cameraD;
    public Material cameraMatD;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }

        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        if(cameraC.targetTexture != null)
        {
            cameraC.targetTexture.Release();
        }

        if(cameraD.targetTexture != null)
        {
            cameraD.targetTexture.Release();
        }

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;

        cameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatC.mainTexture = cameraC.targetTexture;

        cameraD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatD.mainTexture = cameraD.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
