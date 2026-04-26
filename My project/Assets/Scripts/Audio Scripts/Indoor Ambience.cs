using UnityEngine;

public class IndoorAmbience : MonoBehaviour
{
    [Header("Player")]
    public string playerTag = "Player";

    [Header("Garage Ambience")]
    public bool useGarageAmbience = true;
    public Transform garageEntrancePoint;
    public float garageMaxDepthMeters = 8f;
    public string garageDepthParameter = "GarageDepth";

    [Header("Location State")]
    public bool setLocationStateOnEnter = false;
    public AK.Wwise.State locationState;

    private Transform playerTransform;
    private bool playerInsideGarage = false;

    private void Start()
    {
        AkSoundEngine.SetRTPCValue(garageDepthParameter, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[IndoorAmbience] Something entered trigger: " + other.name + " | Tag: " + other.tag);

        if (!other.CompareTag(playerTag))
            return;

        playerTransform = other.transform;
        playerInsideGarage = true;

        if (setLocationStateOnEnter)
        {
            locationState.SetValue();
            Debug.Log("[IndoorAmbience] Location state set.");
        }

        Debug.Log("[IndoorAmbience] PLAYER ENTERED ZONE");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("[IndoorAmbience] Something exited trigger: " + other.name + " | Tag: " + other.tag);

        if (!other.CompareTag(playerTag))
            return;

        if (other.transform == playerTransform)
        {
            playerInsideGarage = false;
            playerTransform = null;

            AkSoundEngine.SetRTPCValue(garageDepthParameter, 0f);
            Debug.Log("[IndoorAmbience] PLAYER EXITED ZONE. RTPC reset to 0.");
        }
    }

    private void Update()
    {
        if (useGarageAmbience)
            UpdateGarageAmbience();
    }

    private void UpdateGarageAmbience()
    {
        if (!playerInsideGarage || playerTransform == null || garageEntrancePoint == null)
            return;

        Vector3 toPlayer = playerTransform.position - garageEntrancePoint.position;

        float depthMeters = Vector3.Dot(toPlayer, garageEntrancePoint.forward);
        depthMeters = Mathf.Clamp(depthMeters, 0f, garageMaxDepthMeters);

        float garageDepthValue = (depthMeters / garageMaxDepthMeters) * 100f;

        AkSoundEngine.SetRTPCValue(garageDepthParameter, garageDepthValue);

        Debug.Log("[IndoorAmbience] " + garageDepthParameter + " = " + garageDepthValue.ToString("F1"));
    }

    private void OnDrawGizmosSelected()
    {
        if (garageEntrancePoint == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            garageEntrancePoint.position,
            garageEntrancePoint.position + garageEntrancePoint.forward * garageMaxDepthMeters
        );
    }
}