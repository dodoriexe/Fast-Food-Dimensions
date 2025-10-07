using UnityEngine;

public class PortalSetup : MonoBehaviour
{
    // Kitchen
    public Camera cameraA;
    public Material cameraMatA;

    // Pantry
    public Camera cameraB;
    public Material cameraMatB;

    // Meatropolis
    public Camera cameraC;
    public Material cameraMatC;

    // Drill
    public Camera cameraD;
    public Material cameraMatD;

    // Kin
    public Camera cameraE;
    public Material cameraMatE;

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

        if(cameraE.targetTexture != null)
        {
            cameraE.targetTexture.Release();
        }

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;

        cameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatC.mainTexture = cameraC.targetTexture;

        cameraD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatD.mainTexture = cameraD.targetTexture;

        cameraE.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatE.mainTexture = cameraE.targetTexture;
    }
}
