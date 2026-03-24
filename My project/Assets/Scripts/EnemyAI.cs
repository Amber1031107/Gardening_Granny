using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float blastForce = 10f;
    public float disappearTime = 2f;
    public int playerPoints = 0;
    public bool hasHitTrap = false;
    public AK.Wwise.Event trapFlingSound;
    public AK.Wwise.Event plantPullSound;
    public AK.Wwise.Event footstepLoopStart;
    public AK.Wwise.Event footstepLoopStop;

    private bool hasEscaped = false;

    private Points points;

    // ── Plant destruction limit ───────────────────────────────────────────────
    private int plantsDestroyedThisRound = 0;
    public int maxPlantsToDestroy = 2;

    [Header("Pathfinding")]
    public float pathUpdateInterval = 0.3f;
    public float waypointReachedDistance = 0.5f;
    public LayerMask obstacleLayer;
    public float obstacleAvoidanceRadius = 1f;
    public int maxPathfindingIterations = 200;

    [Header("Pathfinding Grid")]
    public float nodeSize = 1f;
    public int gridWidth = 30;
    public int gridHeight = 30;

    private bool isMoving = false;
    private Rigidbody rb;
    private Transform target;
    private PointsUI scoreUI;

    private List<Vector3> currentPath = new List<Vector3>();
    private int currentWaypointIndex = 0;
    private float pathUpdateTimer = 0f;

    // ─── A* Node ───────────────────────────────────────────────────────────────
    private class Node
    {
        public Vector2Int gridPos;
        public Vector3 worldPos;
        public bool walkable;
        public float gCost, hCost;
        public Node parent;
        public float fCost => gCost + hCost;

        public Node(Vector2Int gridPos, Vector3 worldPos, bool walkable)
        {
            this.gridPos = gridPos;
            this.worldPos = worldPos;
            this.walkable = walkable;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        scoreUI = FindObjectOfType<PointsUI>();

        // Auto-find the Points script in the scene
        points = FindObjectOfType<Points>();
        if (points == null)
            Debug.LogWarning("[EnemyAI] Could not find Points script in scene!");
    }

    void FixedUpdate()
    {
        // If the destruction limit is hit, stop moving entirely
        if (plantsDestroyedThisRound >= maxPlantsToDestroy)
        {
            StopFootsteps();
            return;
        }

        FindClosestPlant();

        if (target == null)
        {
            StopFootsteps();
            return;
        }

        pathUpdateTimer -= Time.fixedDeltaTime;
        if (pathUpdateTimer <= 0f)
        {
            pathUpdateTimer = pathUpdateInterval;
            currentPath = FindPath(transform.position, target.position);
            currentWaypointIndex = 0;
        }

        FollowPath();
    }

    // ─── Path Following ────────────────────────────────────────────────────────

    void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            MoveTowards(target.position);
            return;
        }

        if (currentWaypointIndex >= currentPath.Count)
        {
            StopFootsteps();
            return;
        }

        Vector3 waypoint = currentPath[currentWaypointIndex];
        waypoint.y = transform.position.y;

        MoveTowards(waypoint);

        if (Vector3.Distance(transform.position, waypoint) < waypointReachedDistance)
            currentWaypointIndex++;
    }

    void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
        transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
        StartFootsteps();
    }

    // ─── A* Pathfinding ────────────────────────────────────────────────────────

    List<Vector3> FindPath(Vector3 startWorld, Vector3 targetWorld)
    {
        Vector3 gridOrigin = transform.position - new Vector3(gridWidth * nodeSize / 2f, 0, gridHeight * nodeSize / 2f);
        Node[,] grid = BuildGrid(gridOrigin);
        Node startNode = WorldToNode(grid, gridOrigin, startWorld);
        Node targetNode = WorldToNode(grid, gridOrigin, targetWorld);

        if (startNode == null || targetNode == null || !targetNode.walkable) return null;

        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closed = new HashSet<Node>();
        int iterations = 0;

        while (openSet.Count > 0 && iterations++ < maxPathfindingIterations)
        {
            Node current = GetLowestFCost(openSet);

            if (current == targetNode)
                return RetracePath(startNode, targetNode);

            openSet.Remove(current);
            closed.Add(current);

            foreach (Node neighbour in GetNeighbours(grid, current))
            {
                if (!neighbour.walkable || closed.Contains(neighbour)) continue;

                float newG = current.gCost + Vector3.Distance(current.worldPos, neighbour.worldPos);
                if (newG < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newG;
                    neighbour.hCost = Vector3.Distance(neighbour.worldPos, targetNode.worldPos);
                    neighbour.parent = current;
                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    Node[,] BuildGrid(Vector3 origin)
    {
        Node[,] grid = new Node[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 worldPos = origin + new Vector3(x * nodeSize, 0, z * nodeSize);
                bool walkable = !Physics.CheckSphere(worldPos, obstacleAvoidanceRadius, obstacleLayer);
                grid[x, z] = new Node(new Vector2Int(x, z), worldPos, walkable);
            }
        return grid;
    }

    Node WorldToNode(Node[,] grid, Vector3 origin, Vector3 worldPos)
    {
        int x = Mathf.RoundToInt((worldPos.x - origin.x) / nodeSize);
        int z = Mathf.RoundToInt((worldPos.z - origin.z) / nodeSize);
        if (x < 0 || x >= gridWidth || z < 0 || z >= gridHeight) return null;
        return grid[x, z];
    }

    List<Node> GetNeighbours(Node[,] grid, Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int dx = -1; dx <= 1; dx++)
            for (int dz = -1; dz <= 1; dz++)
            {
                if (dx == 0 && dz == 0) continue;
                int nx = node.gridPos.x + dx;
                int nz = node.gridPos.y + dz;
                if (nx >= 0 && nx < gridWidth && nz >= 0 && nz < gridHeight)
                    neighbours.Add(grid[nx, nz]);
            }
        return neighbours;
    }

    Node GetLowestFCost(List<Node> nodes)
    {
        Node lowest = nodes[0];
        foreach (Node n in nodes)
            if (n.fCost < lowest.fCost || (n.fCost == lowest.fCost && n.hCost < lowest.hCost))
                lowest = n;
        return lowest;
    }

    List<Vector3> RetracePath(Node start, Node end)
    {
        List<Vector3> path = new List<Vector3>();
        Node current = end;
        while (current != start)
        {
            path.Add(current.worldPos);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    // ─── Helpers ───────────────────────────────────────────────────────────────

    void StartFootsteps()
    {
        if (!isMoving) { footstepLoopStart.Post(gameObject); isMoving = true; }
    }

    void StopFootsteps()
    {
        if (isMoving) { footstepLoopStop.Post(gameObject); isMoving = false; }
    }

    // ─── Plant Finding ─────────────────────────────────────────────────────────

    void FindClosestPlant()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Flower");
        if (plants.Length <= 1)
        {
            target = null;
            if (scoreUI != null)
            {
                scoreUI.ShowScore(playerPoints);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
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

    // ─── Collision ─────────────────────────────────────────────────────────────

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flower"))
        {
            if (plantsDestroyedThisRound < maxPlantsToDestroy)
            {
                plantsDestroyedThisRound++;
                Debug.Log($"[EnemyAI] Plant destroyed ({plantsDestroyedThisRound}/{maxPlantsToDestroy})");
                plantPullSound.Post(gameObject);
                Destroy(collision.gameObject);

                if (plantsDestroyedThisRound >= maxPlantsToDestroy)
                    StartCoroutine(RunAway());
            }
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            if (!hasEscaped)
                TriggerEscape(flung: true);
        }
    }

    private void TriggerEscape(bool flung = false)
    {
        if (hasEscaped) return;
        hasEscaped = true;

        StopAllCoroutines();
        footstepLoopStop.Post(gameObject);

        if (flung)
        {
            trapFlingSound.Post(gameObject);
            rb.useGravity = true;
            rb.AddForce(Vector3.up * blastForce, ForceMode.Impulse);
        }

        if (points != null)
        {
            points.Fances.SetActive(true);
            points.kidSpawned = false;
            points.AddPoints(0); // ← this now shows the panel too
        }
        else
        {
            Debug.LogWarning("[EnemyAI] points reference is not assigned!");
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Destroy(gameObject, disappearTime);
    }

    private IEnumerator RunAway()
    {
        currentPath = null;
        target = null;
        StopFootsteps();

        Vector3 runDirection = (transform.position - Vector3.zero).normalized;
        runDirection.y = 0f;

        footstepLoopStart.Post(gameObject);
        isMoving = true;

        float runTimer = 0f;
        while (runTimer < disappearTime)
        {
            runTimer += Time.fixedDeltaTime;
            Vector3 newPos = transform.position + runDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
            transform.LookAt(transform.position + runDirection);
            yield return new WaitForFixedUpdate();
        }

        TriggerEscape(flung: false);
    }
}