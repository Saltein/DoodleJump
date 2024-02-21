using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfCollisionScript : MonoBehaviour
{
    public float h = 0.6f;
    private GameObject player;

    private float yProjection;
    private bool isPlayerInside;

    private Vector2 velocity;
    private Vector2 yAxis = new Vector2(0, 1);

    BoxCollider2D platformCollider;
    Rigidbody2D playerRB;

    private void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocity = playerRB.velocity;
        float yProjection = Vector2.Dot(velocity, yAxis);

        if (yProjection < 0 && !isPlayerInside)
        {
            platformCollider.enabled = true;
        }
        if (yProjection >= 0)
        {
            platformCollider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            isPlayerInside = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            isPlayerInside = false;
        }
    }
}
