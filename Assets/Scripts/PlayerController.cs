using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float fallMultiplyer;
    Vector2 vectorGravity;
    public bool isOnGround = false;
    bool secondJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vectorGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);

        if(isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isOnGround = false;
                secondJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && secondJump)
            {
                jumpForce = 5f;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isOnGround = false;
                secondJump = false;
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vectorGravity * fallMultiplyer * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpForce = 10f;
            isOnGround = true;
        }
    }
}
