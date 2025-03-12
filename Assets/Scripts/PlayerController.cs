using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer playerSprite;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float fallMultiplyer;
    Vector2 vectorGravity;
    public bool isOnGround = false;
    bool isJumping;
    bool secondJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
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

        RunAnimation(horizontalInput);

        if(horizontalInput < 0)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }

        if(isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
                secondJump = true;
                isJumping = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && secondJump)
            {
                jumpForce = 5f;
                Jump();
                secondJump = false;
                isJumping = false;
                DoubleJumpAnimation(true);
            }
        }
        if (rb.velocity.y < 0)
        {
            isJumping = false;
            DoubleJumpAnimation(false);
            rb.velocity -= vectorGravity * fallMultiplyer * Time.deltaTime;
        }
        JumpAnimation();
        FallAnimation();
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpForce = 10f;
            isOnGround = true;
        }
    }
    void RunAnimation(float input)
    {
        animator.SetFloat("speed", Mathf.Abs(input));
    }
    void JumpAnimation()
    {
        animator.SetBool("isJumping", isJumping);
    }

    void DoubleJumpAnimation(bool doubleJump)
    {
        animator.SetBool("doubleJump", doubleJump);
    }

    void FallAnimation()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
}
