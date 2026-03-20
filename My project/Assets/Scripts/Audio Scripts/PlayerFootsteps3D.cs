using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AkGameObj))]
public class PlayerFootsteps3D : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public float stepDistanceWalk = 2.0f;
    public float stepDistanceRun = 1.2f;
    public float runSpeedThreshold = 4.5f;

    [Header("Surface Detection")]
    public float raycastDistance = 10.0f;
    public LayerMask surfaceLayers = ~0;

    [Header("Wwise")]
    public string switchGroupName = "Surfaces";
    public string eventName = "Play_Footsteps";

    private Vector3 lastPosition;
    private float distanceSinceLastStep;

    private void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 delta = transform.position - lastPosition;
        delta.y = 0f;

        float movedDistance = delta.magnitude;
        lastPosition = transform.position;

        float speed = movedDistance / Mathf.Max(Time.deltaTime, 0.0001f);
        bool isMoving = speed > 1.0f;
        bool isGrounded = controller != null && controller.isGrounded;

        if (!isMoving || !isGrounded)
            return;

        distanceSinceLastStep += movedDistance;

        float requiredStepDistance = speed >= runSpeedThreshold ? stepDistanceRun : stepDistanceWalk;

        if (distanceSinceLastStep >= requiredStepDistance)
        {
            distanceSinceLastStep = 0f;
            PlayFootstep();
        }
    }

    private void PlayFootstep()
    {
        FootstepSurface surface = DetectSurface();
        AkSoundEngine.SetSwitch(switchGroupName, surface.ToString(), gameObject);
        AkSoundEngine.PostEvent(eventName, gameObject);

        Debug.Log("Footstep surface: " + surface);
    }

    private FootstepSurface DetectSurface()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.2f;

        Debug.DrawRay(origin, Vector3.down * raycastDistance, Color.red, 0.1f);

        if (Physics.Raycast(origin, Vector3.down, out hit, raycastDistance, surfaceLayers))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            FootstepSurfaceTag tag = hit.collider.GetComponent<FootstepSurfaceTag>();

            if (tag == null)
                tag = hit.collider.GetComponentInParent<FootstepSurfaceTag>();

            if (tag != null)
            {
                Debug.Log("Detected tagged surface: " + tag.surfaceType + " on " + hit.collider.name);
                return tag.surfaceType;
            }

            Debug.Log("Hit collider had no FootstepSurfaceTag: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Raycast hit nothing");
        }

        return FootstepSurface.Grass;
    }
}