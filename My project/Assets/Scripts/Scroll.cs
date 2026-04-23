using UnityEngine;

public class Scroll : MonoBehaviour
{
    [Header("Sway Settings")]
    public float amplitude = 1f;    // How far it moves left/right
    public float speed = 0.5f;      // How fast it oscillates

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(
            startPosition.x + offsetX,
            startPosition.y,
            startPosition.z
        );
    }
}
