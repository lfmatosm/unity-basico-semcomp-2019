using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public BoxCollider2D body;
    public CircleCollider2D head;
    public Animator animator;
    public float speed = 3f;
    private Rigidbody2D rb2d;
    private bool lookingToTheRight = true;
    private bool isAlive = true;
    private float deathTime;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            if (Time.time > deathTime) Destroy(gameObject);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(speed));
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TileMap"))
        {
            speed = -speed;
            if ((lookingToTheRight && speed < 0) || (!lookingToTheRight && speed > 0))
                FlipSprite();
        }
    }

    public void Kill()
    {
        animator.SetBool("IsAlive", false);
        deathTime = animator.GetCurrentAnimatorStateInfo(0).length + Time.time;
        isAlive = false;
    }

    void FlipSprite()
    {
        lookingToTheRight = !lookingToTheRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
