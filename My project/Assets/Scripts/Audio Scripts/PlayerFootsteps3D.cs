using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AkGameObj))]
public class PlayerFootsteps3D : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;

    [Header("Normal Footsteps")]
    public float stepDistanceWalk = 11f;
    public float stepDistanceRun = 7f;
    public float runSpeedThreshold = 6f;

    [Header("Micro Shuffle")]
    public float shuffleDistance = 0.0015f;
    public float shuffleCooldownMin = 0.08f;
    public float shuffleCooldownMax = 0.14f;

    [Header("Frame Movement Split")]
    public float tinyMovementMin = 0.00015f;
    public float largeMovementMin = 0.0035f;

    [Header("Surface Detection")]
    public float raycastDistance = 10f;
    public LayerMask surfaceLayers = ~0;

    [Header("Wwise")]
    public string switchGroupName = "Surfaces";
    public string eventName = "Play_Footsteps";
    public string speedRtpcName = "PlayerSpeed";
    public float rtpcSmoothing = 10f;

    private Vector3 lastPosition;
    private float distanceSinceLastStep;
    private float distanceSinceLastShuffle;
    private float shuffleCooldownTimer;
    private float nextShuffleCooldown;
    private float smoothedSpeed;

    private FootstepSurface? overrideSurface = null;
    private int triggerCount = 0;

    private void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        lastPosition = transform.position;
        nextShuffleCooldown = Random.Range(shuffleCooldownMin, shuffleCooldownMax);
    }

    private void Update()
    {
        // --- MOVEMENT DATA ---
        Vector3 velocity = controller.velocity;
        velocity.y = 0f;
        float rawSpeed = velocity.magnitude;

        Vector3 delta = transform.position - lastPosition;
        delta.y = 0f;
        float movedDistance = delta.magnitude;
        lastPosition = transform.position;

        // --- RTPC (LOUDNESS ONLY) ---
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, rawSpeed, Time.deltaTime * rtpcSmoothing);
        AkSoundEngine.SetRTPCValue(speedRtpcName, smoothedSpeed, gameObject);

        // --- GROUNDED CHECK ---
        bool isGrounded = controller != null && controller.isGrounded;
        if (!isGrounded)
            return;

        // --- TIMERS ---
        shuffleCooldownTimer += Time.deltaTime;

        // ---------- NORMAL FOOTSTEPS ----------
        bool largeMovementThisFrame = movedDistance > largeMovementMin;

        if (largeMovementThisFrame)
        {
            distanceSinceLastStep += movedDistance;

            float requiredStepDistance = rawSpeed >= runSpeedThreshold ? stepDistanceRun : stepDistanceWalk;

            if (distanceSinceLastStep >= requiredStepDistance)
            {
                distanceSinceLastStep = 0f;
                PlayFootstep();
            }

            // big movement should not also build shuffle
            distanceSinceLastShuffle = 0f;
        }
        else
        {
            distanceSinceLastStep = 0f;
        }

        // ---------- MICRO SHUFFLE ----------
        bool tinyMovementThisFrame = movedDistance > tinyMovementMin && movedDistance <= largeMovementMin;

        if (tinyMovementThisFrame)
        {
            distanceSinceLastShuffle += movedDistance;

            if (distanceSinceLastShuffle >= shuffleDistance && shuffleCooldownTimer >= nextShuffleCooldown)
            {
                distanceSinceLastShuffle = 0f;
                shuffleCooldownTimer = 0f;
                nextShuffleCooldown = Random.Range(shuffleCooldownMin, shuffleCooldownMax);

                PlayFootstep();
            }
        }
    }

    private void PlayFootstep()
    {
        FootstepSurface surface = DetectSurface();
        AkSoundEngine.SetSwitch(switchGroupName, surface.ToString(), gameObject);
        AkSoundEngine.PostEvent(eventName, gameObject);
    }

    private FootstepSurface DetectSurface()
    {
        if (overrideSurface.HasValue)
            return overrideSurface.Value;

        RaycastHit hit;
        Vector3 origin = transform.position + controller.center;

        if (Physics.Raycast(origin, Vector3.down, out hit, raycastDistance, surfaceLayers))
        {
            FootstepSurfaceTag tag = hit.collider.GetComponent<FootstepSurfaceTag>();

            if (tag == null)
                tag = hit.collider.GetComponentInParent<FootstepSurfaceTag>();

            if (tag != null)
                return tag.surfaceType;
        }

        return FootstepSurface.Grass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        FootstepSurfaceTag tag = other.GetComponent<FootstepSurfaceTag>();
        if (tag != null)
        {
            overrideSurface = tag.surfaceType;
            triggerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        FootstepSurfaceTag tag = other.GetComponent<FootstepSurfaceTag>();
        if (tag != null)
        {
            triggerCount--;

            if (triggerCount <= 0)
            {
                overrideSurface = null;
            }
        }
    }
}