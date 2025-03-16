using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer enemySprite;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    private Vector3 nextPosition;
    public float speed = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        nextPosition = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        if(transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player dead");
            collision.gameObject.GetComponent<PlayerController>().Hit();
            if (transform.position == nextPosition)
            {
                nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
            }
        }
    }
}
