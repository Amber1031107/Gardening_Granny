using UnityEngine;

public class IndoorAmbience : MonoBehaviour
{
    [Header("Player")]
    public string playerTag = "Player";

    [Header("Depth Ambience")]
    public bool useDepthAmbience = true;
    public Transform entrancePoint;
    public float maxDepthMeters = 8f;
    public string depthParameter = "GarageDepth";

    [Header("Location State")]
    public bool setLocationStateOnEnter = false;
    public AK.Wwise.State locationStateOnEnter;

    public bool setLocationStateOnExit = false;
    public AK.Wwise.State locationStateOnExit;

    private Transform playerTransform;
    private bool playerInside = false;

    private void Start()
    {
        if (useDepthAmbience)
            AkUnitySoundEngine.SetRTPCValue(depthParameter, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        playerTransform = other.transform;
        playerInside = true;

        if (setLocationStateOnEnter)
        {
            locationStateOnEnter.SetValue();
            Debug.Log("[IndoorAmbience] State on enter set.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        if (other.transform == playerTransform)
        {
            playerInside = false;
            playerTransform = null;

            if (useDepthAmbience)
                AkUnitySoundEngine.SetRTPCValue(depthParameter, 0f);

            if (setLocationStateOnExit)
            {
                locationStateOnExit.SetValue();
                Debug.Log("[IndoorAmbience] State on exit set.");
            }
        }
    }

    private void Update()
    {
        if (useDepthAmbience)
            UpdateDepthAmbience();
    }

    private void UpdateDepthAmbience()
    {
        if (!playerInside || playerTransform == null || entrancePoint == null)
            return;

        Vector3 toPlayer = playerTransform.position - entrancePoint.position;

        float depthMeters = Vector3.Dot(toPlayer, entrancePoint.forward);
        depthMeters = Mathf.Clamp(depthMeters, 0f, maxDepthMeters);

        float depthValue = (depthMeters / maxDepthMeters) * 100f;

        AkUnitySoundEngine.SetRTPCValue(depthParameter, depthValue);
    }

    private void OnDrawGizmosSelected()
    {
        if (entrancePoint == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            entrancePoint.position,
            entrancePoint.position + entrancePoint.forward * maxDepthMeters
        );
    }
}