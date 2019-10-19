using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rgb2d;
    public Path path;
    public float speed = 300f;
    public ForceMode2D fMode;
    public int currentWaypoint = 0;
    public float nextWaypointDistance = 3;

    [HideInInspector]
    private bool pathIsEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rgb2d = GetComponent<Rigidbody2D>();

        if (target == null) {
            Debug.LogError("Jogador não encontrado");
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath() {
        if (target = null) yield return false;
        
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f/updateRate);
        StartCoroutine(UpdatePath());
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate() {
        if (target == null || path == null) return;

        if (currentWaypoint >= path.vectorPath.Count) {
            if (pathIsEnded) return;

            Debug.Log("End of path reached");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        rgb2d.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance) currentWaypoint++;
    }
}
