using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float blastForce = 10f;
    public float disappearTime = 2f;

    public int playerPoints = 0;
    public bool hasHitTrap = false;

    private Rigidbody rb;
    private Transform target;

    // Reference to ScoreUI
    private PointsUI scoreUI;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Find the ScoreUI in the scene
        scoreUI = FindObjectOfType<PointsUI>();
    }

    void FixedUpdate()
    {
        FindClosestPlant();

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPos = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
            transform.LookAt(target);
        }
    }

    void FindClosestPlant()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Flower");
        if (plants.Length == 0)
        {
            target = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestPlant = null;

        foreach (GameObject plant in plants)
        {
            float distance = Vector3.Distance(transform.position, plant.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlant = plant;
            }
        }

        target = closestPlant != null ? closestPlant.transform : null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flower"))
        {
            Destroy(collision.gameObject);
            playerPoints += 2;
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            hasHitTrap = true;
            rb.useGravity = true;
            rb.AddForce(Vector3.up * blastForce, ForceMode.Impulse);
            Destroy(gameObject, disappearTime);

            playerPoints -= 1;

            // Notify the ScoreUI
            if (scoreUI != null)
            {
                scoreUI.ShowScore(playerPoints);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
