using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class KitchenCamera : MonoBehaviour
{
    public Transform PlayerCamera;
    public Transform Portal;

    public Transform otherPortal;

    private void Update()
    {
        otherPortal = GameManager.Instance.dimensionPortals[GameManager.Instance.currentDimensionIndex].transform;

        Vector3 playerOffsetFromPortal = PlayerCamera.position - otherPortal.position;
        transform.position = Portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(Portal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * PlayerCamera.forward;

        // Rotate 180 degrees on the Y axis
        newCameraDirection = Quaternion.AngleAxis(180f, Vector3.up) * newCameraDirection;

        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
