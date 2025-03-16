using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer playerSprite;
    GameManager gameManager;

    [Header("Variables")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float fallMultiplyer;


    Vector2 vectorGravity;
    public bool isOnGround = false;
    bool isJumping;
    bool secondJump = false;
    bool isCheckPoint = false;
    public bool isHit = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
        vectorGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        PlayerRun();
        PlayerJump();

        FallAnimation();

    }

    void PlayerRun()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);

        RunAnimation(horizontalInput);

        if (horizontalInput < 0)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }
    }

    void PlayerJump()
    {
        if (isOnGround)
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
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround = false;
    }

    public void Hit()
    {
        isHit = true;
        rb.AddForce(Vector2.left);
        HealthManager.health--;
        if (HealthManager.health <= 0 )
        {
            Debug.Log("Game Over");
        }
        StartCoroutine(GetHit());
        
    }
    IEnumerator GetHit()
    {
        HitAnimation();
        yield return new WaitForSeconds(3);
        isHit = false;
        HitAnimation();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpForce = 10f;
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            isCheckPoint = true;
            GameObject collisionObj = collision.gameObject;
            Debug.Log("Level Won");
            FlagAnimation(collisionObj);
            StartCoroutine(FlagCoroutine());
            FlagAnimation(collisionObj);
        }
        if(collision.gameObject.CompareTag("Collectible"))
        {
            gameManager.AddScore(collision.gameObject.name.ToString());
            Destroy(collision.gameObject);
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

    void HitAnimation()
    {
        Debug.Log(isHit);
        animator.SetBool("isHit", isHit);
    }
    IEnumerator FlagCoroutine()
    {
        yield return new WaitForSeconds(2.4f);
        isCheckPoint = false;
    }

    void FlagAnimation(GameObject flag)
    {
        flag.GetComponent<Animator>().SetBool("IsReachedEnd", isCheckPoint);
    }
}
