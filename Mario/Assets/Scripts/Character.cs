﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public float moveForce = 365f;
    public float jumpForce = 2f;
    public float maxSpeed = 5f;
    public float maxVerticalSpeed = 3f;
    private Rigidbody2D rb2d;
    private bool lookingToTheRight = true;
    private bool grounded = true;
    Vector3 spawnPosition = new Vector3(-1.7f, -1.8f, 0);

    private bool isDead = false;
    public BoxCollider2D body;
    public CircleCollider2D feet;
    Transform target;
    public Sprite deathSprite;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("return"))
            Spawn();

        if (isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.1f * Time.deltaTime);
        }
        else
        {
            float h = Input.GetAxis("Horizontal");

            animator.SetFloat("Speed", Mathf.Abs(h));

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
                Jump();

            if ((lookingToTheRight && h < 0) || (!lookingToTheRight && h > 0))
                FlipSprite();

            if (h * rb2d.velocity.x < maxSpeed)
                rb2d.AddForce(Vector2.right * h * moveForce);

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        if (other.gameObject.CompareTag("Void"))
        {
            Death();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }

    void Jump()
    {
        rb2d.AddForce(new Vector2(0, jumpForce));
        rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * maxVerticalSpeed);
    }

    void FlipSprite()
    {
        lookingToTheRight = !lookingToTheRight;
        transform.Rotate(0, 180, 0);
        transform.Translate(-transform.localScale.x / 2, 0, 0);
    }

    public void Death()
    {
        isDead = true;
        target = transform;
        target.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        animator.enabled = false;
        rb2d.velocity = new Vector3(0, rb2d.velocity.y);
        spriteRenderer.sprite = deathSprite;
        body.isTrigger = true;
        feet.isTrigger = true;
    }

    void Spawn()
    {
        isDead = false;
        animator.enabled = true;
        body.isTrigger = false;
        feet.isTrigger = false;
        transform.position = spawnPosition;
    }
}
