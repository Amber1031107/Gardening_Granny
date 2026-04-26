using UnityEngine;

public class HouseDoors : MonoBehaviour
{
    [Header("Front Doors")]
    public Transform frontDoorLeft;
    public Transform frontDoorRight;

    [Header("Garage Door")]
    public Transform garageDoor;

    [Header("Open Rotations (game start)")]
    public float frontDoorOpenY = 90f;
    public float garageDoorOpenZ = 90f;

    [Header("Closed Rotations (when kid spawns)")]
    public float frontDoorClosedY = 0f;
    public float garageDoorClosedZ = 0f;

    void Start()
    {
        ResetDoors();
    }

    /// <summary>Call this when the round button is pressed (from Points.InteractLeftClick)</summary>
    public void CloseDoors()
    {
        Debug.Log("[HouseDoors] CloseDoors called");

        if (frontDoorLeft != null)
            frontDoorLeft.localEulerAngles = new Vector3(
                frontDoorLeft.localEulerAngles.x, frontDoorClosedY, frontDoorLeft.localEulerAngles.z);
        else
            Debug.LogWarning("[HouseDoors] frontDoorLeft is not assigned!");

        if (frontDoorRight != null)
            frontDoorRight.localEulerAngles = new Vector3(
                frontDoorRight.localEulerAngles.x, frontDoorClosedY, frontDoorRight.localEulerAngles.z);
        else
            Debug.LogWarning("[HouseDoors] frontDoorRight is not assigned!");

        if (garageDoor != null)
            garageDoor.localEulerAngles = new Vector3(
                garageDoor.localEulerAngles.x, garageDoor.localEulerAngles.y, garageDoorClosedZ); // fixed
        else
            Debug.LogWarning("[HouseDoors] garageDoor is not assigned!");
    }

    /// <summary>Call this on game start or when resetting the round</summary>
    public void ResetDoors()
    {
        if (frontDoorLeft != null)
            frontDoorLeft.localEulerAngles = new Vector3(
                frontDoorLeft.localEulerAngles.x, frontDoorOpenY, frontDoorLeft.localEulerAngles.z);

        if (frontDoorRight != null)
            frontDoorRight.localEulerAngles = new Vector3(
                frontDoorRight.localEulerAngles.x, frontDoorOpenY, frontDoorRight.localEulerAngles.z);

        if (garageDoor != null)
            garageDoor.localEulerAngles = new Vector3(
                garageDoor.localEulerAngles.x, garageDoor.localEulerAngles.y,garageDoorOpenZ);
    }
}
