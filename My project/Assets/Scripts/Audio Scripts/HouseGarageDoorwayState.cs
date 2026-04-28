using UnityEngine;

public class HouseGarageDoorwayState : MonoBehaviour
{
    [Header("Player")]
    public string playerTag = "Player";

    [Header("Doorway Direction")]
    public Transform doorwayDirection;
    // Blue arrow should point from GARAGE into HOUSE

    [Header("Wwise States")]
    public AK.Wwise.State houseState;
    public AK.Wwise.State garageState;

    private Vector3 playerEnterPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        playerEnterPosition = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        Vector3 movementThroughDoor = other.transform.position - playerEnterPosition;

        float direction = Vector3.Dot(movementThroughDoor, doorwayDirection.forward);

        if (direction > 0f)
        {
            houseState.SetValue();
            Debug.Log("[HouseGarageDoorway] Entered HOUSE");
        }
        else
        {
            garageState.SetValue();
            Debug.Log("[HouseGarageDoorway] Entered GARAGE");
        }
    }
}