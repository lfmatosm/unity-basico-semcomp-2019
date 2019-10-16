using UnityEngine;
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

    [HideInInspector]
    private bool pathIsEnded = false;

    public float nextWaypointDistance = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
