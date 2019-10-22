using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform bottomBounds;

    public float smoothDampTime = 0.15f;
    private float smoothDampVelocityX = 0.0f;
    private float smoothDampVelocityY = 10.0f;

    private float camWidth, camHeight, levelMinX, levelMaxX, levelMinY;


    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = Camera.main.aspect;

        float leftBoundWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float bottomBoundWidth = bottomBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;

        levelMinX = leftBounds.position.x + leftBoundWidth + (camWidth / 2);
        levelMaxX = rightBounds.position.x - leftBoundWidth - (camWidth / 2);
        levelMinY = bottomBounds.position.y + bottomBoundWidth + (camHeight / 2);
    }

    void Update()
    {
        if (target)
        {
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            float targetY = Mathf.Max(levelMinY, target.position.y);

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocityX, smoothDampTime);
            float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocityY, smoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
